using HolidayPlanner.Readers;

namespace HolidayPlanner.Tests.Stubs
{
    public class NationalHolidaysReaderStub : INationalHolidaysReader
    { 
        public Task<Dictionary<string, IEnumerable<DateTime>>> GetHolidaysByCountryCodeAsync()
        {
            var result = new Dictionary<string, IEnumerable<DateTime>>
            {
                {
                    "FIN", new List<DateTime>
                    {
                        new(2021, 1, 1),
                        new(2021, 1, 6),
                        new(2021, 4, 2),
                        new(2021, 4, 5),
                    }
                },
            };
            return Task.FromResult(result);
        }
    }
}
