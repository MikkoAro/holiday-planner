using HolidayPlanner.Services;
using Microsoft.Extensions.Hosting;

namespace HolidayPlanner
{
    internal sealed class HolidayPlannerApplication : IHostedService
    {
        private readonly IHolidayPlannerService _holidayPlannerService;
        public HolidayPlannerApplication(IHolidayPlannerService holidayPlannerService)
        {
            _holidayPlannerService = holidayPlannerService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            Console.WriteLine("-------------------------");
            Console.WriteLine("Welcome to HolidayPlanner");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("In this application you can find how many holidays you need to use for given time period. To EXIT input 0.");
            Console.WriteLine("Please insert time period e.g. 1.7.2021 - 29.7.2021");
            Console.WriteLine();
            
            while (true)
            {
                var userInput = Console.ReadLine();

                if (int.TryParse(userInput, out var exit) && exit == 0)
                {
                    return;
                }

                try
                {
                    var result = await _holidayPlannerService.ResolveHolidaysCount(userInput);
                    Console.WriteLine($"You need to use {result} holiday days for given period");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                
                Console.WriteLine();
                Console.WriteLine("Please insert time period e.g. 1.7.2021 - 29.7.2021, or 0 to exit");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
