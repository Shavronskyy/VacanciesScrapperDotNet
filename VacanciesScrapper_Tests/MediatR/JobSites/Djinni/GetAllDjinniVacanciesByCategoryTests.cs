using Moq;
using VacanciesScrapper_BLL.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.Djinni;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_Tests.MediatR.JobSites.Djinni
{
	public class GetAllDjinniVacanciesByCategoryTests
	{
        private Mock<IDjinniVacanciesService> _serviceMock;
        private Mock<ILoggerService> _logger;

        public GetAllDjinniVacanciesByCategoryTests()
		{
            _serviceMock = new();
            _logger = new();
        }

        [Fact]
        public async Task Handler_ShouldReturnErrorMsg_WhenVacanciesIsNull()
        {
            // Arrange
            var query = new GetAllDjinniVacanciesByCategoryQuery(Categories.Dotnet, YearsOfExperience.LessThanOne);
            var handler = new GetAllDjinniVacanciesByCategoryHandler(_serviceMock.Object, _logger.Object);
            var expectedErrorMessage = $"Cannot find any vacancies";

            _serviceMock.Setup(x => x.GetAllDjinniVacanciesByCategory(default, default)).ReturnsAsync((IEnumerable<Vacancy>)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.True(result.IsFailed);
                Assert.Equal(expectedErrorMessage, result.Errors.FirstOrDefault()?.Message);
            });
        }
    }
}

