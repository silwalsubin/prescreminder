using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using services.Users.Payload;
using services.Users.Persistence;

namespace services.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public UserController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<UsersTableSchema.UserRecord>> Get()
        {
            var result = (await _usersRepository.GetAll()).ToList();
            if (!result.Any())
            {
                var testUser = new UsersTableSchema.UserRecord
                {
                    UserId = Guid.NewGuid(),
                    UserName = "subinho",
                    EmailAddress = "subinho09@gmail.com",
                    Password = "1234",
                    FirstName = "Subin",
                    MiddleName = null,
                    LastName = "Silwal",
                    DateOfBirthUtc = DateTime.UtcNow
                };
                await _usersRepository.Insert(testUser);
                result.Add(testUser);
            }

            return result;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLogInPayload payload)
        {
            var emailAddressProvided = !string.IsNullOrWhiteSpace(payload.EmailAddress);
            var userRecord = emailAddressProvided
                ? await _usersRepository.GetByEmailAddress(payload.EmailAddress)
                : await _usersRepository.GetByUserName(payload.UserName);

            if (userRecord == null || payload.Password != userRecord.Password)
                return BadRequest("Invalid credentials");

            var claims = new[]
            {
                new Claim(ClaimType.UserId, userRecord.UserId.ToString()),
            };
            var token = AuthenticationMiddleWare.GenerateJsonWebToken(claims);
            return Ok(new
            {
                token
            });
        }

        [HttpGet]
        [Route("authorize")]
        public ActionResult Authorize()
        {
            var currentUser = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            return Ok("");
        }
    }
}
