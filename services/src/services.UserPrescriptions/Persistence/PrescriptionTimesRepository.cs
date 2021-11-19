using contracts.Persistence;
using Dapper.Contrib.Extensions;
using System.Threading.Tasks;

namespace services.UserPrescriptions.Persistence
{
    public class PrescriptionTimesRepository : BaseRepository
    {
        private readonly PrescriptionTimesTableSchema _prescriptionTimesTableSchema;

        public PrescriptionTimesRepository(PrescriptionTimesTableSchema prescriptionTimesTableSchema)
        {
            _prescriptionTimesTableSchema = prescriptionTimesTableSchema;
        }

        public async Task InsertAsync(PrescriptionTimesTableSchema.PrescriptionTimeRecord record)
        {
            await DbConnection.InsertAsync(record);
        }
    }
}
