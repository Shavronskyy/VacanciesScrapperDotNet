using GroqSharp;
using GroqSharp.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace VacanciesScrapper.Services;

public class AIAnalyzerService
{
    public static async Task<string> AnalyzeVacancyAnswerInPrecents(string description)
    {
        var apiKey = Environment.GetEnvironmentVariable("GROQ_APIKEY");
        var apiModel = Environment.GetEnvironmentVariable("GROQ_MODEL");


        var pdf = "C:\\Users\\User\\Desktop\\cv\\new cv\\Ukraine\\typescript\\Shavronskyy_Junior_.Net.pdf";

        var result = ExtractTextFromPdf(pdf);

        IGroqClient groqClient = new GroqClient(apiKey, apiModel);

        var response = await groqClient.CreateChatCompletionAsync(
            new Message { Role = MessageRoleType.System, Content = "You are job-search assistant, you help people how search job to find perfect job for them" },
            //new Message { Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, you need to answer if this vacancy fit to CV or not, and you need to answer in precents" },
            new Message { Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, " +
                                                                      "you have to answer in percentage how much the vacancy will fit the CV, " +
                                                                      "and you need to answer only in precents, without additional text, " +
                                                                      "you must to count by experience, technologies, programming languages, skills." +
                                                                      "main critearia for you is a main programming language from CV and from vacancy descriptino" +
                                                                      "for example: if CV have main tech stack '.NET' and vacancy is SWIFT, than this will be 0 precents" +
                                                                      "always at the end put '%'" },
            new Message { Role = MessageRoleType.User, Content = "CV:" + result + " " + "Vacancy description" + description });
        return (response);
    }

    static string ExtractTextFromPdf(string pdfPath)
    {
        using (PdfReader pdfReader = new PdfReader(pdfPath))
        using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
        {
            StringWriter writer = new StringWriter();
            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
            {
                string pageText = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i));
                writer.Write(pageText);
            }
            return writer.ToString();
        }
    }
}