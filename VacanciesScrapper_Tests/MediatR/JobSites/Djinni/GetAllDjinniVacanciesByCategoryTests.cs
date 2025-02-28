﻿using Moq;
using VacanciesScrapper_Utils.Enums;
using VacanciesScrapper_BLL.MediatR.JobSites.Djinni;
using VacanciesScrapper_BLL.Models;
using VacanciesScrapper_BLL.Services.Interfaces;
using VacanciesScrapper_BLL.Services.Logging;

namespace VacanciesScrapper_Tests.MediatR.JobSites.Djinni
{
	public class GetAllDjinniVacanciesByCategoryTests
	{
        private readonly Mock<IDjinniVacanciesService> _serviceMock;
        private readonly Mock<ILoggerService> _logger;

        public GetAllDjinniVacanciesByCategoryTests()
		{
            _serviceMock = new();
            _logger = new();
        }

        [Fact]
        public async Task Handler_ShouldReturnEmptyList_WhenVacanciesIsNull()
        {
            // Arrange
            var query = new GetAllDjinniVacanciesByCategoryQuery(Categories.Dotnet, YearsOfExperience.LessThanOne);
            var handler = new GetAllDjinniVacanciesByCategoryHandler(_serviceMock.Object, _logger.Object);

            _serviceMock.Setup(x => x.GetAllDjinniVacanciesByCategory(default, default)).ReturnsAsync((IEnumerable<Vacancy>)null);

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

