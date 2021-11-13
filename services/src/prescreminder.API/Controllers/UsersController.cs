using Microsoft.AspNetCore.Mvc;
using prescreminder.Database.Tables.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prescreminder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersRepository _usersRepository;

        public UsersController(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UsersTableSchema.UserRecord>> Get()
        {
            var result = (await _usersRepository.Get()).ToList();
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
    }
}
