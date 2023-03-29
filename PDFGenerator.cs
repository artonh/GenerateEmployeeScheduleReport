using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

namespace GenerateEmployeeScheduleReport
{
    public class PDFGenerator
    {
        public void GeneratePDF(string FileName, StringBuilder stBuilder)
        {
            using (var document = new Document())
            {
                MemoryStream memoryStream = new MemoryStream();

                using (var writer = PdfWriter.GetInstance(document, memoryStream))
                {
                    document.Open();

                    Paragraph paragraph = new Paragraph();

                    foreach (var item in stBuilder.ToString().Split("\t"))
                    {
                        paragraph.Add(item);
                        paragraph.TabSettings = new TabSettings(56f);
                        paragraph.Add(Chunk.TABBING);
                    }

                    document.Add(paragraph);

                    document.Close();
                    byte[] bytes = memoryStream.ToArray();
                    File.WriteAllBytes(FileName, bytes);

                }

            }

        }
    }
}
