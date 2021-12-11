using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using prescreminder.Utilities;
using services.UserPrescriptions.Domain;
using services.UserPrescriptions.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

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
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(Request.GetUserTimeZone());
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var prescriptionRecords = (await _userPrescriptionsRepository.GetByUserIdAsync(userId));
            var result = new List<MedicationInfoViewModel>();
            foreach (var prescriptionRecord in prescriptionRecords)
            {
                var prescriptionTimes = await _prescriptionTimesRepository.GetAsync(prescriptionRecord.PrescriptionId);

                foreach (var prescriptionTime in prescriptionTimes)
                {
                    var timeSpan = new TimeSpan(prescriptionTime.Hour, prescriptionTime.Minute, prescriptionTime.Second);
                    var medicationTimeUtc = timeZoneInfo.GetStartOfDayUtc().Add(timeSpan);
                    if (medicationTimeUtc >= prescriptionRecord.StartDateUtc)
                    {
                        result.Add(new MedicationInfoViewModel
                        {
                            Name = prescriptionRecord.Name,
                            Quantity = prescriptionRecord.UnitDose,
                            Hour = prescriptionTime.Hour,
                            Minute = prescriptionTime.Minute,
                        });
                    }
                }
            }
            return result.OrderBy(x => x.Hour).ThenBy(x => x.Minute);
        }
    }
}
