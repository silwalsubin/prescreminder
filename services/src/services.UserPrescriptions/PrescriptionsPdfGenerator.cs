using iTextSharp.text;
using iTextSharp.text.pdf;
using services.UserPrescriptions.Persistence;
using System.IO;

namespace services.UserPrescriptions
{
    public class PrescriptionsPdfGenerator
    {
        private readonly UserPrescriptionsRepository _prescriptionsRepository;

        public PrescriptionsPdfGenerator(UserPrescriptionsRepository prescriptionsRepository)
        {
            _prescriptionsRepository = prescriptionsRepository;
        }

        public byte[] GetFileStream()
        {
            using var stream = new MemoryStream();
            using var document = new Document(PageSize.A4, 30, 30, 30, 30);
            using var pdfWriter = PdfWriter.GetInstance(document, stream);
            document.Open();
            document.Add(new Paragraph("Hello World"));
            document.Close();
            var result = stream.ToArray();
            pdfWriter.CloseStream = false;
            pdfWriter.Close();
            return result;
        }
    }
}
