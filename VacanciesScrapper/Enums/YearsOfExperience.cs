using System.Text.Json.Serialization;

namespace VacanciesScrapper.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum YearsOfExperience
	{
        LessThanOne,
        FromOneToThree,
        FromThreeToFive,
        FivePlus
    }
}

