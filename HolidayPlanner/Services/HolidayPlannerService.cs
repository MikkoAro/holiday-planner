using HolidayPlanner.Readers;
using HolidayPlanner.Validators;

namespace HolidayPlanner.Services
{
    public interface IHolidayPlannerService
    {
        Task<int> ResolveHolidaysCount(string? input);
    }

    public class HolidayPlannerService : IHolidayPlannerService
    {
        private readonly IUserInputValidator _validator;
        private readonly INationalHolidaysReaderWrapper _nationalHolidaysReaderWrapper;

        public HolidayPlannerService(IUserInputValidator validator, 
            INationalHolidaysReaderWrapper nationalHolidaysReaderWrapper)
        {
            _validator = validator;
            _nationalHolidaysReaderWrapper = nationalHolidaysReaderWrapper;
        }

        public async Task<int> ResolveHolidaysCount(string? userInput)
        {
            var (from, to) = _validator.ValidateRequest(userInput);
            var nationalHolidays =
                await _nationalHolidaysReaderWrapper.GetHolidaysByCountryCodeAsync();
            return CountHolidays(from, to, nationalHolidays);
        }

        private static int CountHolidays(DateTime from, DateTime to, IEnumerable<DateTime> nationalHolidays)
        {
            var daysCount = (to - from).Days;

            var dates = Enumerable.Range(0, 1 + daysCount)
                .Select(x => from.AddDays(x))
                .ToList();

            return dates.Count -
                dates.Count(
                    x => x.DayOfWeek == DayOfWeek.Sunday ||
                        nationalHolidays.Contains(x));
        }
    }
}
