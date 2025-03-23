using AutoMapper;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_DAL.Entities;

namespace VacanciesScrapper_BLL.Mapping.Companies
{
    class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
