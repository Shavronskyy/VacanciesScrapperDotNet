namespace VacanciesScrapper_Utils.Options
{
    public class AIOptions
    {
        public const string Key = "Groq";
        public string APIKEY { get; set; }
        public string Model { get; set; }
        public string CvUrl { get; set; }
        public string SystemContent { get; set; }
        public string AssistantContent { get; set; }
    }
}