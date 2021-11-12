using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using prescreminder.Database.Domain;
using prescreminder.Utilities;

namespace prescreminder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
