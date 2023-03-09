using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

namespace GenerateEmployeeScheduleReport
{
    public class PDFGenerator
    {
        public void GeneratePDF(string FileName, StringBuilder st)
        {
            using (var document = new Document())
            {
                MemoryStream memoryStream = new MemoryStream();

                using (var writer = PdfWriter.GetInstance(document, memoryStream))
                {
                    document.Open();

                    Paragraph paragraph = new Paragraph(st.ToString());
                    document.Add(paragraph);

                    document.Close();

                    byte[] bytes = memoryStream.ToArray();
                    File.WriteAllBytes(FileName, bytes);
                }
            }
        }
    }
}
