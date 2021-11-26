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
    public class UserMedicationTodayController : ControllerBase
    {
        private readonly UserPrescriptionsRepository _userPrescriptionsRepository;
        private readonly PrescriptionTimesRepository _prescriptionTimesRepository;

        public UserMedicationTodayController(
            UserPrescriptionsRepository userPrescriptionsRepository,
            PrescriptionTimesRepository prescriptionTimesRepository
        )
        {
            _userPrescriptionsRepository = userPrescriptionsRepository;
            _prescriptionTimesRepository = prescriptionTimesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MedicationInfoViewModel>> Get()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecords = await _userPrescriptionsRepository.GetByUserIdAsync(userId);
            var result = new List<MedicationInfoViewModel>();
            foreach (var prescriptionRecord in prescriptionRecords)
            {
                var prescriptionTimes = await _prescriptionTimesRepository.GetAsync(prescriptionRecord.PrescriptionId);
                foreach (var prescriptionTime in prescriptionTimes)
                {
                    result.Add(new MedicationInfoViewModel
                    {
                        Name = prescriptionRecord.Name,
                        Quantity = prescriptionRecord.Quantity,
                        Hour = prescriptionTime.Hour,
                        Minute = prescriptionTime.Minute,
                    });
                }
            }
            return result.OrderBy(x => x.Hour).ThenBy(x => x.Minute);
        }
    }
}
