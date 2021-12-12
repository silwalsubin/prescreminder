using contracts.Notifications;
using infrastructures.BackgroundJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using prescreminder.Utilities;
using services.UserPrescriptions.Domain;
using services.UserPrescriptions.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace services.UserPrescriptions.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPrescriptionController : ControllerBase
    {
        private readonly UserPrescriptionsRepository _userPrescriptionsRepository;
        private readonly PrescriptionTimesRepository _prescriptionTimesRepository;
        private readonly INotificationService _notificationService;
        private readonly PrescriptionsPdfGenerator _prescriptionsPdfGenerator;
        private readonly IBackgroundJobsInfrastructure _backgroundJobsInfrastructure;

        public UserPrescriptionController(
            UserPrescriptionsRepository userPrescriptionsRepository,
            PrescriptionTimesRepository prescriptionTimesRepository,
            INotificationService notificationService,
            PrescriptionsPdfGenerator prescriptionsPdfGenerator,
            IBackgroundJobsInfrastructure backgroundJobsInfrastructure)
        {
            _userPrescriptionsRepository = userPrescriptionsRepository;
            _prescriptionTimesRepository = prescriptionTimesRepository;
            _notificationService = notificationService;
            _prescriptionsPdfGenerator = prescriptionsPdfGenerator;
            _backgroundJobsInfrastructure = backgroundJobsInfrastructure;
        }

        [HttpGet]
        [Route("pdf")]
        public async Task<IActionResult> Pdf()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var memoryStream = new MemoryStream(await _prescriptionsPdfGenerator.GetFileStream(userId));
            const string fileName = "Prescriptions.pdf";
            Response.SetFileName(fileName);
            return File(memoryStream, "application/octet-stream", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecords = await _userPrescriptionsRepository.GetByUserIdAsync(userId);
            var result = new List<PrescriptionViewModel>();
            foreach (var x in prescriptionRecords)
            {
                var prescriptionTimes = (await _prescriptionTimesRepository.GetAsync(x.PrescriptionId)).Select(t => new TimeOfDay
                {
                    Hour = t.Hour,
                    Minute = t.Minute,
                }).OrderBy(t => new TimeSpan(t.Hour, t.Minute, 0)).ToList();

                var viewModal = new PrescriptionViewModel
                {
                    PrescriptionId = x.PrescriptionId,
                    Name = x.Name,
                    UnitDose = x.UnitDose,
                    CompleteDate = x.CompleteDateUtc,
                    StartDate = x.StartDateUtc,
                    TotalQuantity = x.QuantityRemaining(Request.GetUserTimeZone(), prescriptionTimes),
                    TimesOfDay = prescriptionTimes,
                };

                result.Add(viewModal);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("{id:guid}/refill/{refill:int}")]
        public async Task<IActionResult> Refill(Guid id, int refill)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecord = (await _userPrescriptionsRepository.GetByUserIdAsync(userId)).Single(x => x.PrescriptionId == id);
            prescriptionRecord.TotalQuantity += refill;
            await _userPrescriptionsRepository.UpdateAsync(prescriptionRecord);
            var timesOfDay = await _prescriptionTimesRepository.GetAsync(id);
            await _notificationService.AddOrUpdateEventNotification(new EventNotification
            {
                NotificationType = NotificationType.PrescriptionExpiration,
                Entity = $"{prescriptionRecord.Name} {prescriptionRecord.UnitDose}",
                EventDateUtc = prescriptionRecord.GetExpirationTimeUtc(Request.GetUserTimeZone(), timesOfDay.Select(x => new TimeOfDay
                {
                    Hour = x.Hour,
                    Minute = x.Minute
                }).ToList()),
                UserId = userId,
                NotificationId = prescriptionRecord.PrescriptionId
            });

            return Ok();
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PrescriptionViewModel model)
        {
            var prescriptionId = id;
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var userPrescriptionRecord = new UserPrescriptionsTableSchema.UserPrescriptionRecord
            {
                UserId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId),
                PrescriptionId = prescriptionId,
                CreatedDateUtc = DateTime.UtcNow,
                ModifiedDateUtc = DateTime.UtcNow,
                StartDateUtc = model.StartDate,
                CompleteDateUtc = model.CompleteDate,
                TotalQuantity = model.TotalQuantity,
                Name = model.Name,
                UnitDose = model.UnitDose,
            };

            var uniquePrescriptionTimeRecords = model.TimesOfDay.Select(x => new { x.Hour, x.Minute }).Distinct().Select(x =>
                new PrescriptionTimesTableSchema.PrescriptionTimeRecord
                {
                    PrescriptionId = prescriptionId,
                    PrescriptionTimeId = Guid.NewGuid(),
                    Hour = x.Hour,
                    Minute = x.Minute,
                    Second = 0
                }).ToList();

            await _userPrescriptionsRepository.UpdateAsync(userPrescriptionRecord);
            await _prescriptionTimesRepository.DeleteByPrescriptionIdAsync(id);
            foreach (var prescriptionRecord in uniquePrescriptionTimeRecords)
            {
                await _prescriptionTimesRepository.InsertAsync(prescriptionRecord);
            }

            await _notificationService.AddOrUpdateEventNotification(new EventNotification
            {
                NotificationType = NotificationType.PrescriptionExpiration,
                Entity = $"{model.Name} {model.UnitDose}",
                EventDateUtc = userPrescriptionRecord.GetExpirationTimeUtc(Request.GetUserTimeZone(), model.TimesOfDay),
                UserId = userId,
                NotificationId = prescriptionId
            });

            foreach (var timeOfDay in uniquePrescriptionTimeRecords)
            {
                _backgroundJobsInfrastructure.AddOrUpdateRecurringJob(
                    $"{prescriptionId}-{timeOfDay.Hour}-{timeOfDay.Minute}",
                    () => System.IO.File.WriteAllText(@$"C:\{timeOfDay.Hour}{timeOfDay.Minute}.txt", "hello world"),
                    $"{timeOfDay.Minute} {timeOfDay.Hour} * * *",
                    TZConvert.GetTimeZoneInfo(Request.GetUserTimeZone())
                );
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecord = (await _userPrescriptionsRepository.GetByUserIdAsync(userId)).Single(x => x.PrescriptionId == id);
            await _prescriptionTimesRepository.DeleteByPrescriptionIdAsync(prescriptionRecord.PrescriptionId);
            await _userPrescriptionsRepository.DeleteAsync(prescriptionRecord.PrescriptionId);
            await _notificationService.DeleteById(id);
            return Ok();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AddPrescriptionPayload payload)
        {
            var prescriptionId = Guid.NewGuid();
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var userPrescriptionRecord = new UserPrescriptionsTableSchema.UserPrescriptionRecord
            {
                UserId = userId,
                PrescriptionId = prescriptionId,
                CreatedDateUtc = DateTime.UtcNow,
                ModifiedDateUtc = DateTime.UtcNow,
                StartDateUtc = payload.StartDate,
                CompleteDateUtc = payload.CompleteDate,
                ExpirationDateUtc = null, // todo:need to get rid of this field soon
                Name = payload.Name,
                TotalQuantity = payload.TotalQuantity,
                UnitDose = payload.UnitDose,
            };

            var uniqueTimesOfDay = payload.TimesOfDay.Select(x => new { x.Hour, x.Minute }).Distinct().ToList();
            var uniquePrescriptionTimeRecords = uniqueTimesOfDay.Select(x =>
                 new PrescriptionTimesTableSchema.PrescriptionTimeRecord
                 {
                     PrescriptionId = prescriptionId,
                     PrescriptionTimeId = Guid.NewGuid(),
                     Hour = x.Hour,
                     Minute = x.Minute,
                     Second = 0
                 }).ToList();

            await _userPrescriptionsRepository.InsertAsync(userPrescriptionRecord);
            foreach (var prescriptionRecord in uniquePrescriptionTimeRecords)
            {
                await _prescriptionTimesRepository.InsertAsync(prescriptionRecord);
            }

            await _notificationService.AddOrUpdateEventNotification(new EventNotification
            {
                NotificationType = NotificationType.PrescriptionExpiration,
                Entity = $"{payload.Name} {payload.UnitDose}",
                EventDateUtc = userPrescriptionRecord.GetExpirationTimeUtc(Request.GetUserTimeZone(), uniquePrescriptionTimeRecords.Select(x => new TimeOfDay
                {
                    Hour = x.Hour,
                    Minute = x.Minute
                }).ToList()),
                UserId = userId,
                NotificationId = prescriptionId
            });

            return Ok();
        }
    }
}
