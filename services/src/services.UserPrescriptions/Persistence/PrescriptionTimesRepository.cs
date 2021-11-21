using contracts.Persistence;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<PrescriptionTimesTableSchema.PrescriptionTimeRecord>> GetAsync(Guid prescriptionId)
        {
            var sql = @$"
                SELECT * FROM [{_prescriptionTimesTableSchema.Schema}].[{_prescriptionTimesTableSchema.TableName}]
                WHERE PrescriptionId = @prescriptionId
            ";
            var result =
                await DbConnection.QueryAsync<PrescriptionTimesTableSchema.PrescriptionTimeRecord>(sql, new { prescriptionId });
            return result;
        }

        public async Task DeleteByPrescriptionIdAsync(Guid prescriptionId)
        {
            var sql = @$"
                DELETE FROM [{_prescriptionTimesTableSchema.Schema}].[{_prescriptionTimesTableSchema.TableName}]
                WHERE PrescriptionId = @prescriptionId
            ";
            await DbConnection.ExecuteAsync(sql, new { prescriptionId });
        }
    }
}
