using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using services.UserPrescriptions.Payloads;
using services.UserPrescriptions.Persistence;
using System;
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
            var prescriptionRecords = payload.TimesOfDay.Select(x =>
                new PrescriptionTimesTableSchema.PrescriptionTimeRecord
                {
                    PrescriptionId = prescriptionId,
                    PrescriptionTimeId = x.Id,
                    Hour = x.Hour,
                    Minute = x.Minute,
                    Second = 0
                });

            await _userPrescriptionsRepository.InsertAsync(userPrescriptionRecord);
            foreach (var prescriptionRecord in prescriptionRecords)
            {
                await _prescriptionTimesRepository.InsertAsync(prescriptionRecord);
            }

            return Ok();
        }
    }
}
