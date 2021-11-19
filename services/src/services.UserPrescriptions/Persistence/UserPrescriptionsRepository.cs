using contracts.Persistence;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;

namespace services.UserPrescriptions.Persistence
{
    public class UserPrescriptionsRepository : BaseRepository
    {
        private readonly UserPrescriptionsTableSchema _userPrescriptionsTableSchema;

        public UserPrescriptionsRepository(UserPrescriptionsTableSchema userPrescriptionsTableSchema)
        {
            _userPrescriptionsTableSchema = userPrescriptionsTableSchema;
        }

        public async Task InsertAsync(UserPrescriptionsTableSchema.UserPrescriptionRecord record)
        {
            await DbConnection.InsertAsync(record);
        }
    }
}
