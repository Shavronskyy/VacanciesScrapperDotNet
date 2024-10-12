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
        //var description =
            //"We seek a Senior PHP Developer with extensive experience in the LAMP stack. The client’s company is creating a strong neo-banking platform utilising AI, cloud computing, and Blockchain technology to explore new ways for people to conduct transactions, invest, and succeed in the digital economy. \n\nRole Overview:\n\nAs a PHP Developer, you will be responsible for tech debt improvements, unit test implementation and ongoing integrations with external banking systems.\n \n\nRequired Skills and Experience:\n\n5+ years of proven experience as a PHP Developer \nSolid experience with the LAMP stack (Linux, Apache, MySQL, PHP)\nFamiliarity with version control systems (preferably Git)\nExperience with integrating APIs and working with databases\nKnowledge of best practices in web development and application security\nAbility to work independently and collaboratively within a team environment\nStrong problem-solving skills and attention to detail\nUpper-Intermediate English level and higher\nPreferred Qualifications:\n\nExperience with payment integration - is a big plus\nBlockchain knowledge\nExperience with React\nFamiliarity with Docker for local development\nExperience with Jenkins or similar CI/CD tools\nUnderstanding of unit testing frameworks and methodologies\nKey Responsibilities:\n\nDevelop, test and maintain scalable Web application using the LAMP stack\nWork on technical integrations\nCollaborating closely with VP of Blockchain Operations\nImprove and optimize existing code, focusing on scalability and performance\nParticipate in code reviews and contribute to the overall improvement of development processes\nImplement unit tests and other testing frameworks to ensure code quality\nAssist in troubleshooting and debugging issues as they arise\nHiring Stages:\n\nInterview with Recruiter\nTechnical interview\nClient’s interview\nBenefits:\n\n18 paid vacation days in addition to public holidays\nPaid sick leaves\nCashback on sport\u26be\n50% compensation for an educational program\nHealthcare program\ud83e\ude7a\nOnline psychological therapy sessions\ud83d\udc86\nCorporate events (team buildings, holidays, etc.)\nPossibility to work remotely\nПро компанію Dewais\nDewais is a fast-growing Software Development company based in Kharkiv, Ukraine. We provide top-quality software development, design, testing, support and consulting services. We create large-scale solutions, as well as help startups to grow from the MVP to profitable business. And we always deliver wise solutions to the world to make it a better place to live.\nBesides a growth mindset, our core value is humanity. The motto of Dewais-people is Be with us, but remain yourself - this is your main secret of success! In Dewais each teammate feels supported and can speak openly about their ideas, concerns or anything. We are like a tribe, but a tribe of professionals. We all look in the same direction – to make the world a better place by bringing wise solutions. This idea makes us really energetic. And to make us even more energetic we are happy to meet new teammates.\n";

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