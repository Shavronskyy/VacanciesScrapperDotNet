namespace VacanciesScrapper_Utils.Options
{
    public class AIOptions
    {
        public const string Key = "Groq";
        public string APIKEY { get; }
        public string Model { get; }
        public string CvUrl { get; }
    }
}