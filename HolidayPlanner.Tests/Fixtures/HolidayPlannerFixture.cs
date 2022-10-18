using HolidayPlanner.Readers;
using HolidayPlanner.Services;
using HolidayPlanner.Tests.Stubs;
using HolidayPlanner.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace HolidayPlanner.Tests.Fixtures
{
    public class HolidayPlannerFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public HolidayPlannerFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddScoped<IUserInputValidator, UserInputValidator>()
                .AddScoped<INationalHolidaysReaderWrapper, NationalHolidaysReaderWrapper>()
                .AddScoped<IHolidayPlannerService, HolidayPlannerService>()
                .AddSingleton<INationalHolidaysReader, NationalHolidaysReaderStub>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
