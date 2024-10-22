using System.Text.Json.Serialization;

namespace VacanciesScrapper_BLL.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Categories
	{
		None,
		Dotnet,
		Java,
		Angular,
		JavaScript,
		React,
		Vue,
		FullStack,
		Python,
		Php,
		Node,
		Ios,
		Android,
		ReactNative,
		C,
		Cpp,
		Flutter,
		Golang,
		Ruby,
		Scala,
		Kotlin
	}
}

