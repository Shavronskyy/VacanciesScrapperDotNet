using GroqSharp;
using GroqSharp.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace VacanciesScrapper.Services;

public class AIAnalyzerService
{
    public static async Task<string> AnalyzeVacancyAnswerInPrecents()
    {
        var apiKey = Environment.GetEnvironmentVariable("GROQ_APIKEY");
        var apiModel = Environment.GetEnvironmentVariable("GROQ_MODEL");


        var pdf = "C:\\Users\\User\\Desktop\\cv\\new cv\\Ukraine\\typescript\\Shavronskyy_Junior_.Net.pdf";

        var result = ExtractTextFromPdf(pdf);

        var description = "Office Work in LVIV Inforceis a Software Development Company that provides a full range of top-quality IT services. " +
                          "Our mission is to develop first-class applications and Websites to provide our clients with the best solutions for maximizing " +
                          "their profits and converting their ideas into reality. Responsibilities:● Participate in requirements analysis● Collaborate with " +
                          "internal teams to produce software design and architecture● Write clean, scalable code using .NET programming languages● Test and deploy " +
                          "applications and systems● Revise, update, refactor and debug code● Improve existing software● Develop documentation throughout " +
                          "the software development life cycle (SDLC)● Serve as an expert on applications and provide technical support Required skills:● " +
                          "Familiarity with the ASP.NET framework, SQL Server and design/architecturalpatterns (e.g. Model-View-Controller (MVC))● " +
                          "Knowledge of at least one of the .NET languages (e.g. C#, Visual Basic .NET) and HTML5/CSS3● JS Angular/React● Familiarity " +
                          "with architecture styles/APIs (REST, RPC)● Understanding of Agile methodologies● Excellent troubleshooting and communication" +
                          " skills● Attention to detail● Intermediate - Upper-intermediate English level We offer:● Competitive salary● Interesting and " +
                          "challenging projects● Future career growth opportunities● Paid sick leave and working day vacation● Friendly team of " +
                          "professionals● Delicious coffee biscuits and tea for your good mood● The company covers 50% of the cost of courses you need";      

        IGroqClient groqClient = new GroqClient(apiKey, apiModel);

        var response = await groqClient.CreateChatCompletionAsync(
            new Message { Role = MessageRoleType.System, Content = "You are job-search assistant, you help people how search job to find perfect job for them" },
            //new Message { Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, you need to answer if this vacancy fit to CV or not, and you need to answer in precents" },
            new Message { Role = MessageRoleType.Assistant, Content = "Your job is analyze user's CV and vacancy, " +
                                                                      "you have to answer in percentage how much the vacancy will fit the CV, " +
                                                                      "and you need to answer only in precents, without additional text, " +
                                                                      "you must to count by experience, technologies, programming languages, skills." +
                                                                      "main critearia for you is a main programming language from CV and from vacancy descriptino" +
                                                                      "for example: if CV have main tech stack '.NET' and vacancy is SWIFT, than this will be 0 precents" },
            new Message { Role = MessageRoleType.User, Content = "CV:" + result + " " + "Vacancy description" + description });
        return ("---------------------------------------------------------------------------------\n" + response );
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