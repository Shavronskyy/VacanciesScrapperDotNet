using GroqSharp.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.Extensions.Options;
using VacanciesScrapper_Utils.Options;
using VacanciesScrapper_BLL.Services.Interfaces;

namespace VacanciesScrapper_BLL.Services.Realizations
{
    public class AIAnalyzerService : IAIAnalyzerService
    {
        private readonly AIOptions _options;

        public AIAnalyzerService(IOptions<AIOptions> options)
        {
            _options = options.Value;
        }

        public async Task<int> AnalyzeVacancyAnswerInPrecents(string description)
        {
            var pdf = _options.CvUrl;

            var result = ExtractTextFromPdf(pdf);

            var groqClient = new GroqClient(_options.APIKEY, _options.Model);

            var response = await groqClient.CreateChatCompletionAsync(
                new Message
                {
                    Role = MessageRoleType.System,
                    Content = _options.SystemContent
                },
                new Message
                {
                    Role = MessageRoleType.Assistant,
                    Content = _options.AssistantContent
                },
                new Message
                {
                    Role = MessageRoleType.User,
                    Content = "CV:" + result + " " + "Vacancy description" + description
                });
            return Convert.ToInt32(response);
        }

        private static string ExtractTextFromPdf(string pdfPath)
        {
            using var pdfReader = new PdfReader(pdfPath);
            using var pdfDocument = new PdfDocument(pdfReader);
            var writer = new StringWriter();
            for (var i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                var pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
                writer.Write(pageText);
            }

            return writer.ToString();
        }
    }
}