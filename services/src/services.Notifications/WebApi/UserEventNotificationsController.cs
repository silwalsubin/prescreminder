using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using services.Notifications.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace services.Notifications.WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserEventNotificationsController : ControllerBase
    {
        private readonly UserEventNotificationRepository _userEventNotificationRepository;

        public UserEventNotificationsController(UserEventNotificationRepository userEventNotificationRepository)
        {
            _userEventNotificationRepository = userEventNotificationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var notifications = await _userEventNotificationRepository.GetByUserIdAsync(userId);
            var expiringNotifications =
                notifications.Where(x => x.EventDateUtc.AddDays(-10).Date <= DateTime.UtcNow.Date);
            var nonClearedNotifications = expiringNotifications.Where(x =>
                !x.ClearedDateUtc.HasValue || (x.ClearedDateUtc.Value.Date != DateTime.UtcNow.Date))
                .OrderBy(x => x.EventDateUtc);
            var result = nonClearedNotifications.Select(x => new
            {
                x.NotificationId,
                Event = x.GetVerbiage(),
                EventDate = x.EventDateUtc
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("clear/{id}")]
        public async Task<IActionResult> Clear(Guid id)
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            var notifications = await _userEventNotificationRepository.GetByUserIdAsync(userId);
            if (notifications.Any(x => x.NotificationId == id))
            {
                await _userEventNotificationRepository.ClearByNotificationId(id);
            }
            return Ok();
        }
    }
}
