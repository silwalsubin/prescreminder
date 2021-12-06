using iText.Html2pdf;
using services.UserPrescriptions.Persistence;
using services.Users.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace services.UserPrescriptions
{
    public class PrescriptionsPdfGenerator
    {
        private readonly UserPrescriptionsRepository _prescriptionsRepository;
        private readonly PrescriptionTimesRepository _prescriptionTimesRepository;
        private readonly UsersRepository _usersRepository;

        public PrescriptionsPdfGenerator(
            UserPrescriptionsRepository prescriptionsRepository,
            UsersRepository usersRepository,
            PrescriptionTimesRepository prescriptionTimesRepository)
        {
            _prescriptionsRepository = prescriptionsRepository;
            _usersRepository = usersRepository;
            _prescriptionTimesRepository = prescriptionTimesRepository;
        }

        public async Task<byte[]> GetFileStream(Guid userId)
        {
            await using var stream = new MemoryStream();
            var html = @$"
                <!DOCTYPE html>
                <html>
                    <head>
                        <link rel=""stylesheet"" href=""pdf-styles.css"">
                    </head>
                    <body>
                        <div class=""header"">
                            <div class=""header-title"">Prescription List for {await GetUserFullName(userId)}</div>
                            {DateTime.UtcNow:F} UTC
                        </div>
                        <div class=""prescription-list"">
                            <table>
                              <tr class=""table-header"">
                                <th>Name</th>
                                <th>Unit Dose</th>
                                <th>Intake Time(s)</th>
                              </tr>
                              {await GetPrescriptionListHtml(userId)}
                            </table>
                        </div>
                    </body>
                </html>
            ";


            var converterProperties = new ConverterProperties();
            var stylesPath = @$"{AppDomain.CurrentDomain.BaseDirectory}Styles\pdf-styles.css";
            converterProperties.SetBaseUri(stylesPath);
            HtmlConverter.ConvertToPdf(html, stream, converterProperties);
            var result = stream.ToArray();
            return result;
        }

        private async Task<string> GetUserFullName(Guid userId)
        {
            return await _usersRepository.GetUserFullName(userId);
        }

        private async Task<string> GetPrescriptionListHtml(Guid userId)
        {
            var result = new List<string>();
            var prescriptions = (await _prescriptionsRepository.GetByUserIdAsync(userId)).OrderBy(x => x.Name);
            foreach (var prescription in prescriptions)
            {
                result.Add(@$"
                    <tr>
                        <td>{prescription.Name}</td>
                        <td>{prescription.UnitDose}</td>
                        <td>{await GetTimesAsync(prescription.PrescriptionId)}</td>
                    </tr>
                ");
            }
            return string.Join(Environment.NewLine, result);
        }

        private async Task<string> GetTimesAsync(Guid prescriptionId)
        {
            var prescriptionTimes = (await _prescriptionTimesRepository.GetAsync(prescriptionId))
                                    .OrderBy(x => x.Hour)
                                    .ThenBy(x => x.Minute);
            return string.Join(", ", prescriptionTimes.Select(x => GetAmPm(x.Hour, x.Minute)));
        }

        private static string GetAmPm(int hour, int minute)
        {
            var timeSpan = new TimeSpan(hour, minute, 0);
            var time = DateTime.Today.Add(timeSpan);
            return time.ToString("hh:mm tt");
        }
    }
}
