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
                    Content =
                        "You are job-search assistant, you help people how search job to find perfect job for them"
                },
                //new Message { Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, you need to answer if this vacancy fit to CV or not, and you need to answer in precents" },
                new Message
                {
                    Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, " +
                                                                "you have to answer in percentage how much the vacancy will fit the CV, " +
                                                                "and you need to answer only in precents, without additional text, " +
                                                                "you must to count by experience, technologies, programming languages, skills." +
                                                                "main critearia for you is a main programming language from CV and from vacancy descriptino" +
                                                                "for example: if CV have main tech stack '.NET' and vacancy is SWIFT, than this will be 0 precents" +
                                                                "you dont need to put '%' at the end"
                },
                new Message
                {
                    Role = MessageRoleType.User, Content = "CV:" + result + " " + "Vacancy description" + description
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