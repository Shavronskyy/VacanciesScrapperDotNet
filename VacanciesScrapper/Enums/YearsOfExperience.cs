using System.Text.Json.Serialization;

namespace VacanciesScrapper.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum YearsOfExperience
	{
        None,
        LessThanOne,
        FromOneToThree,
        FromThreeToFive,
        FivePlus
    }
}

