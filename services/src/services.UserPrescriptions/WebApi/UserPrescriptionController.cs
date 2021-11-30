using contracts.Notifications;
using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using services.UserPrescriptions.Domain;
using services.UserPrescriptions.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace services.UserPrescriptions.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPrescriptionController : ControllerBase
    {
        private readonly UserPrescriptionsRepository _userPrescriptionsRepository;
        private readonly PrescriptionTimesRepository _prescriptionTimesRepository;
        private readonly INotificationService _notificationService;

        public UserPrescriptionController(
            UserPrescriptionsRepository userPrescriptionsRepository,
            PrescriptionTimesRepository prescriptionTimesRepository,
            INotificationService notificationService
        )
        {
            _userPrescriptionsRepository = userPrescriptionsRepository;
            _prescriptionTimesRepository = prescriptionTimesRepository;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IEnumerable<PrescriptionViewModel>> Get()
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
                }).ToList();

                var viewModal = new PrescriptionViewModel
                {
                    PrescriptionId = x.PrescriptionId,
                    Name = x.Name,
                    Quantity = x.Quantity,
                    CompleteDate = x.CompleteDateUtc,
                    StartDate = x.StartDateUtc,
                    ExpirationDate = x.ExpirationDateUtc,
                    TimesOfDay = prescriptionTimes
                };

                result.Add(viewModal);
            }
            return result;
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
                ExpirationDateUtc = model.ExpirationDate,
                Name = model.Name,
                Quantity = model.Quantity,
            };

            var uniquePrescriptionTimeRecords = model.TimesOfDay.Select(x => new { x.Hour, x.Minute }).Distinct().Select(x =>
                new PrescriptionTimesTableSchema.PrescriptionTimeRecord
                {
                    PrescriptionId = prescriptionId,
                    PrescriptionTimeId = Guid.NewGuid(),
                    Hour = x.Hour,
                    Minute = x.Minute,
                    Second = 0
                });

            await _userPrescriptionsRepository.UpdateAsync(userPrescriptionRecord);
            await _prescriptionTimesRepository.DeleteByPrescriptionIdAsync(id);
            foreach (var prescriptionRecord in uniquePrescriptionTimeRecords)
            {
                await _prescriptionTimesRepository.InsertAsync(prescriptionRecord);
            }

            if (model.ExpirationDate.HasValue)
            {
                await _notificationService.AddOrUpdateEventNotification(new EventNotification
                {
                    Event = $"Prescription Expiration for {model.Name}",
                    EventDateUtc = model.ExpirationDate.Value,
                    UserId = userId,
                    NotificationId = prescriptionId
                });
            }
            else
            {
                await _notificationService.DeleteById(prescriptionId);
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
                ExpirationDateUtc = payload.ExpirationDate,
                Name = payload.Name,
                Quantity = payload.Quantity,
            };

            var uniquePrescriptionTimeRecords = payload.TimesOfDay.Select(x => new { x.Hour, x.Minute }).Distinct().Select(x =>
                 new PrescriptionTimesTableSchema.PrescriptionTimeRecord
                 {
                     PrescriptionId = prescriptionId,
                     PrescriptionTimeId = Guid.NewGuid(),
                     Hour = x.Hour,
                     Minute = x.Minute,
                     Second = 0
                 });

            await _userPrescriptionsRepository.InsertAsync(userPrescriptionRecord);
            foreach (var prescriptionRecord in uniquePrescriptionTimeRecords)
            {
                await _prescriptionTimesRepository.InsertAsync(prescriptionRecord);
            }

            if (payload.ExpirationDate.HasValue)
            {
                await _notificationService.AddOrUpdateEventNotification(new EventNotification
                {
                    Event = $"Prescription Expiration for {payload.Name}",
                    EventDateUtc = payload.ExpirationDate.Value,
                    UserId = userId,
                    NotificationId = prescriptionId
                });
            }

            return Ok();
        }
    }
}
