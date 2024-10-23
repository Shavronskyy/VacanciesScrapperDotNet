using HtmlAgilityPack;

namespace VacanciesScrapper_BLL.Services.Interfaces
{
    public interface IScrapperService
    {
        Task<HtmlDocument> GetHtml(string link);
    }
}