using System;
using VacanciesScrapper.Enums;

namespace VacanciesScrapper.Switches
{
	public class CategoriesDjinni
	{
		public CategoriesDjinni()
		{

		}

		public static string GetCategory(Categories? cat)
		{
			switch (cat)
			{
				case Categories.None:
					return "";
				case Categories.DOTNET:
					return "?primary_keyword=.NET";
				case Categories.Angular:
					return "?primary_keyword=Angular";
				case Categories.JavaScript:
					return "?primary_keyword=JavaScript";
				case Categories.React:
					return "?primary_keyword=React.js";
				case Categories.Vue:
					return "?primary_keyword=Vue.js";
				case Categories.FullStack:
					return "?primary_keyword=Fullstack";
				case Categories.Java:
					return "?primary_keyword=Java";
				case Categories.Python:
					return "?primary_keyword=Python";
				case Categories.PHP:
					return "?primary_keyword=PHP";
				case Categories.Node:
					return "?primary_keyword=Node.js";
				case Categories.IOS:
					return "?primary_keyword=iOS";
				case Categories.Android:
					return "?primary_keyword=Android";
				case Categories.ReactNative:
					return "?primary_keyword=React+Native";
				case Categories.C:
					return "/?primary_keyword=C+Lang";
				case Categories.CPP:
					return "?primary_keyword=CPP";
				case Categories.Flutter:
					return "?primary_keyword=Flutter";
				case Categories.Golang:
					return "?primary_keyword=Golang";
				case Categories.Ruby:
					return "?primary_keyword=Ruby";
				case Categories.Scala:
					return "?primary_keyword=Scala";
				case Categories.Kotlin:
					return "?primary_keyword=Kotlin";
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
                    return "&exp_level=no_exp";
                case YearsOfExperience.FromOneToThree:
                    return "&exp_level=1y&exp_level=2y&exp_level=3y";
                case YearsOfExperience.FromThreeToFive:
                    return "&exp_level=3y&exp_level=4y&exp_level=5y";
                case YearsOfExperience.FivePlus:
                    return "&exp_level=5y&exp_level=6y&exp_level=7y&exp_level=8y&exp_level=9y&exp_level=10y";
				default:
					return "";
            }
        }
    }
}

