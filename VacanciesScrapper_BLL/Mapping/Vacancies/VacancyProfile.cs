using AutoMapper;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_DAL.Entities;

namespace VacanciesScrapper_BLL.Mapping.Vacancies
{
    class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, VacancyDto>().ReverseMap();
        }
    }
}
