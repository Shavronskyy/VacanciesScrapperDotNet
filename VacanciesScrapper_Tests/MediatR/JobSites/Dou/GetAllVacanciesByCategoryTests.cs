using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.DOU;
using VacanciesScrapper_BLL.Services.Interfaces;
using Moq;
using Xunit;
using VacanciesScrapper_BLL.Services.Logging;
using VacanciesScrapper_BLL.Models;

namespace VacanciesScrapper_Tests.MediatR.JobSites.Dou
{
    public class GetAllVacanciesByCategoryTests
    {
        private readonly Mock<IDouVacanciesService> _serviceMock;
        private readonly Mock<ILoggerService> _logger;

        public GetAllVacanciesByCategoryTests()
        {
            _serviceMock = new();
            _logger = new();
        }

        [Fact]
        public async Task Handler_ShouldReturnEmptyList_WhenVacanciesIsNull()
        {
            // Arrange
            var query = new GetAllDouVacanciesByCategoryQuery(Categories.Dotnet, YearsOfExperience.LessThanOne);
            var handler = new GetAllDouVacanciesByCategoryHandler(_serviceMock.Object, _logger.Object);

            _serviceMock.Setup(x => x.GetAllDouVacanciesByCategory(default, default)).ReturnsAsync((IEnumerable<Vacancy>)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.True(result.Value.Count() < 1);
            });
        }
    }
}