using HolidayPlanner;
using HolidayPlanner.Readers;
using HolidayPlanner.Services;
using HolidayPlanner.Validators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddHostedService<HolidayPlannerApplication>()
            .AddScoped<IUserInputValidator, UserInputValidator>()
            .AddScoped<INationalHolidaysReaderWrapper, NationalHolidaysReaderWrapper>()
            .AddScoped<IHolidayPlannerService, HolidayPlannerService>()
            .AddSingleton<INationalHolidaysReader, NationalHolidaysReader>()
    )
    .Build();

await host.RunAsync();
