using contracts.Persistence;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using prescreminder.Utilities;

namespace prescreminder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class HeartBeatController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Is Alive";
        }

        [HttpGet]
        [Route("database")]
        public string Database()
        {
            var connection = new SqlConnection(AppSettingsUtility.GetSettings<PersistenceSettings>().DbConnectionString);
            var result = connection.QueryFirstOrDefault<string>("SELECT GetDate()");
            return $"Database IsAlive {result}";
        }
    }
}
