using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using prescreminder.Utilities;
using services.UserMedicationIntakeHistories.Domain;
using services.UserMedicationIntakeHistories.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneConverter;

namespace services.UserMedicationIntakeHistories.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMedicationIntakeHistoriesController : ControllerBase
    {
        private readonly UserMedicationIntakeHistoriesRepository _userMedicationIntakeHistoriesRepository;

        public UserMedicationIntakeHistoriesController(UserMedicationIntakeHistoriesRepository userMedicationIntakeHistoriesRepository)
        {
            _userMedicationIntakeHistoriesRepository = userMedicationIntakeHistoriesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserMedicationIntakeHistoryPayload payload)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var historyId = Guid.NewGuid();

            var record = new UserMedicationIntakeHistoriesTableSchema.UserMedicationIntakeHistoryRecord
            {
                HistoryId = historyId,
                UserId = userId,
                PrescriptionName = payload.PrescriptionName,
                Quantity = payload.Quantity,
                Hour = payload.Hour,
                Minute = payload.Minute,
                EventDateUtc = DateTime.UtcNow,
            };

            await _userMedicationIntakeHistoriesRepository.InsertAsync(record);
            return Ok(historyId);
        }

        [HttpGet]
        [Route("today")]
        public async Task<IActionResult> GetHistoriesForToday()
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(Request.GetUserTimeZone());
            var dateFromUtc = timeZoneInfo.GetStartOfDayUtc();
            var dateToUtc = timeZoneInfo.GetEndOfDayUtc();

            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var result = await _userMedicationIntakeHistoriesRepository.GetByEventDateRangeAsync(userId, dateFromUtc, dateToUtc);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var historyRecords = await _userMedicationIntakeHistoriesRepository.GetByUserIdAsync(userId);
            var recordFound = historyRecords.Any(x => x.HistoryId == id);

            if (recordFound)
                await _userMedicationIntakeHistoriesRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
