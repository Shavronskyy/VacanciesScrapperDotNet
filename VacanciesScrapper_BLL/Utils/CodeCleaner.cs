using System;
using System.Text.RegularExpressions;

namespace VacanciesScrapper_BLL.Utils
{
	public class CodeCleaner
	{
		public CodeCleaner()
		{
		}

        public static void ScrubHtml(ref string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            var step3 = Regex.Replace(step2, @"\t|\n|\r", "");
            value = step3;
        }
    }
}

