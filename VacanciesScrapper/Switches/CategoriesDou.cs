using System;
using VacanciesScrapper.Enums;

namespace VacanciesScrapper.Switches
{
	public class CategoriesDou
	{
        public static string GetCategory(Categories? cat)
        {
            switch (cat)
            {
                case Categories.DOTNET:
                    return "vacancies/?category=.NET";
                case Categories.Angular:
                    return "vacancies/?category=Angular";
                case Categories.JavaScript:
                    return "vacancies/?category=JavaScript";
                case Categories.React:
                    return "vacancies/?category=React.js";
                case Categories.Vue:
                    return "vacancies/?category=Vue.js";
                case Categories.FullStack:
                    return "vacancies/?category=Fullstack";
                case Categories.Java:
                    return "vacancies/?category=Java";
                case Categories.Python:
                    return "vacancies/?category=Python";
                case Categories.PHP:
                    return "vacancies/?category=PHP";
                case Categories.Node:
                    return "vacancies/?category=Node.js";
                case Categories.IOS:
                    return "vacancies/?category=iOS";
                case Categories.Android:
                    return "vacancies/?category=Android";
                case Categories.ReactNative:
                    return "vacancies/?category=React+Native";
                case Categories.C:
                    return "vacancies/?category=C+Lang";
                case Categories.CPP:
                    return "vacancies/?category=CPP";
                case Categories.Flutter:
                    return "vacancies/?category=Flutter";
                case Categories.Golang:
                    return "vacancies/?category=Golang";
                case Categories.Ruby:
                    return "vacancies/?category=Ruby";
                case Categories.Scala:
                    return "vacancies/?category=Scala";
                case Categories.Kotlin:
                    return "vacancies/?category=Kotlin";
                default:
                    return "";
            }
        }

        public static string GetExperience(YearsOfExperience? exp)
        {
            switch (exp)
            {
                case YearsOfExperience.None:
                    return "";

                case YearsOfExperience.LessThanOne:
                    return "&exp=0-1";

                case YearsOfExperience.FromOneToThree:
                    return "&exp=1-3";

                case YearsOfExperience.FromThreeToFive:
                    return "&exp=3-5";

                case YearsOfExperience.FivePlus:
                    return "&exp=5plus";
                default:
                    return "";
            }
        }
    }
}

