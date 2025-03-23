using Moq;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.AllVacancies;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_Tests.MediatR.AllVacancies
{
	public class GetAllVacanciesByCategoryTests
	{
        private readonly Mock<IHomeVacanciesService> _serviceMock;
        private readonly Mock<ILoggerService> _loggerMock;

		public GetAllVacanciesByCategoryTests()
		{
            _loggerMock = new();
            _serviceMock = new();
		}

        [Fact]
        public async Task Handler_ShouldReturnErrorMsg_WhenVacanciesIsNull()
        {
            // Arrange
            var query = new GetAllVacanciesByCategoryQuery(Categories.Dotnet, YearsOfExperience.LessThanOne);
            var handler = new GetAllVacanciesByCategoryHandler(_serviceMock.Object, _loggerMock.Object);
            var expectedErrorMessage = $"Cannot find any vacancies";

            _serviceMock.Setup(x => x.GetAllVacanciesByCategory(default, default)).ReturnsAsync((IEnumerable<VacancyDto>)null);

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

