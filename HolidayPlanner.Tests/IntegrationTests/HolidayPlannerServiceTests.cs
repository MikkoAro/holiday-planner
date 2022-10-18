using System.ComponentModel.DataAnnotations;
using HolidayPlanner.Services;
using HolidayPlanner.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HolidayPlanner.Tests.IntegrationTests
{
    public class HolidayPlannerServiceTests : IClassFixture<HolidayPlannerFixture>
    {
        private readonly ServiceProvider _serviceProvider;

        public HolidayPlannerServiceTests(HolidayPlannerFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Theory]
        [InlineData("01.01.2021 - 14.01.2021", 10)]
        [InlineData("01.04.2021 - 06.04.2021", 3)]
        [InlineData("01.01.2021-19.02.2021", 41)] //max range request
        public async Task Should_Return_Holidays_Count(string input, int expected)
        {
            var holidayPlannerService = _serviceProvider.GetService<IHolidayPlannerService>();
            var result = await holidayPlannerService!.ResolveHolidaysCount(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("01.01.2021-20.02.2021")] // >50 days
        [InlineData("08.11.2021-01.10.2021")] // from > to
        [InlineData("15.03.2021-06.04.2021")] // not within holiday period
        [InlineData("ok")]
        public async Task Should_Throw_ValidationError(string input)
        {
            var holidayPlannerService = _serviceProvider.GetService<IHolidayPlannerService>();
            await Assert.ThrowsAsync<ValidationException>(
                 () => holidayPlannerService!.ResolveHolidaysCount(input));
        }

        [Fact]
        public async Task Should_Throw_NullException()
        {
            var holidayPlannerService = _serviceProvider.GetService<IHolidayPlannerService>();
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => holidayPlannerService!.ResolveHolidaysCount(null));
        }
    }
}