using System.Text.Json.Serialization;

namespace VacanciesScrapper.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Categories
	{
		DOTNET,
		Java,
		Angular,
		JavaScript,
		React,
		Vue,
		FullStack,
		Python,
		PHP,
		Node,
		IOS,
		Android,
		ReactNative,
		C,
		CPP,
		Flutter,
		Golang,
		Ruby,
		Scala,
		Kotlin
	}
}

