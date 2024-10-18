using HtmlAgilityPack;

namespace VacanciesScrapper.Services.Interfaces;

public interface IScrapperService
{
    Task<HtmlDocument> GetHtml(string link);
}