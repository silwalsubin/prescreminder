using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using services.UserPrescriptions.Payloads;
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

        public UserPrescriptionController(UserPrescriptionsRepository userPrescriptionsRepository, PrescriptionTimesRepository prescriptionTimesRepository)
        {
            _userPrescriptionsRepository = userPrescriptionsRepository;
            _prescriptionTimesRepository = prescriptionTimesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PrescriptionViewModal>> Get()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecords = await _userPrescriptionsRepository.GetByUserIdAsync(userId);
            var result = new List<PrescriptionViewModal>();
            foreach (var x in prescriptionRecords)
            {
                var prescriptionTimes = (await _prescriptionTimesRepository.GetAsync(x.PrescriptionId)).Select(t => new TimeOfDay
                {
                    Hour = t.Hour,
                    Minute = t.Minute,
                }).ToList();

                var viewModal = new PrescriptionViewModal
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecord = (await _userPrescriptionsRepository.GetByUserIdAsync(userId)).Single(x => x.PrescriptionId == id);
            await _prescriptionTimesRepository.DeleteByPrescriptionIdAsync(prescriptionRecord.PrescriptionId);
            await _userPrescriptionsRepository.DeleteAsync(prescriptionRecord.PrescriptionId);
            return Ok();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] AddPrescriptionPayload payload)
        {
            var prescriptionId = Guid.NewGuid();
            var userPrescriptionRecord = new UserPrescriptionsTableSchema.UserPrescriptionRecord
            {
                UserId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId),
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

            return Ok();
        }
    }
}
