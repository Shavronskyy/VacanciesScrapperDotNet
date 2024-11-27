using FluentResults;

namespace VacanciesScrapper_BLL.MediatR.ResultVariations
{
    public class NullResult<T> : Result<T>
    {
        public NullResult()
            : base()
        {
        }
    }
}