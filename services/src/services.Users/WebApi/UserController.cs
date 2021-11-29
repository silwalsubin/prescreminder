using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using middleware.Authentication;
using prescreminder.Utilities;
using services.Users.Payload;
using services.Users.Persistence;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace services.Users.WebApi
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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserLogInPayload payload)
        {
            var isEmailAddress = payload.EmailAddressOrUserName.Contains("@");
            var userRecord = isEmailAddress
                ? await _usersRepository.GetByEmailAddress(payload.EmailAddressOrUserName)
                : await _usersRepository.GetByUserName(payload.EmailAddressOrUserName);

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

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegisterPayload payload)
        {
            if (!payload.UserName.IsAlphaNumeric())
                return BadRequest("Username must be letters or numbers");

            if (!payload.EmailAddress.IsEmailAddress())
                return BadRequest("Invalid Email Address");

            if (!string.Equals(payload.Password, payload.ConfirmPassword))
                return BadRequest("Passwords do not match");

            if (await _usersRepository.GetByEmailAddress(payload.EmailAddress) != null)
                return BadRequest("Email Address has already been taken");

            if (await _usersRepository.GetByUserName(payload.UserName) != null)
                return BadRequest("UserName has already been taken");

            var userRecord = new UsersTableSchema.UserRecord
            {
                UserName = payload.UserName,
                EmailAddress = payload.EmailAddress,
                Password = payload.Password,
                FirstName = payload.FirstName,
                MiddleName = payload.MiddleName,
                LastName = payload.LastName,
                DateOfBirthUtc = payload.DateOfBirth,
                UserId = Guid.NewGuid(),
            };

            await _usersRepository.InsertAsync(userRecord);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = HttpContext.GetClaimValue<Guid>(ClaimType.UserId);
            // need to delete other records associated with the users as well.
            await _usersRepository.DeleteByUserId(userId);
            return Ok();
        }
    }
}
