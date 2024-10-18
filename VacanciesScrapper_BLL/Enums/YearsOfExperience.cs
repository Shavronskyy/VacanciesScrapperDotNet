using System.Text.Json.Serialization;

namespace VacanciesScrapper_BLL.Enums
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

