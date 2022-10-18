
namespace HolidayPlanner.Readers
{
    public interface INationalHolidaysReaderWrapper
    {
        Task<IEnumerable<DateTime>> GetHolidaysByCountryCodeAsync(string countryCode = "FIN");
    }
    public class NationalHolidaysReaderWrapper : INationalHolidaysReaderWrapper
    {
        private readonly INationalHolidaysReader _reader;

        public NationalHolidaysReaderWrapper(INationalHolidaysReader reader)
        {
            _reader = reader;
        }

        public async Task<IEnumerable<DateTime>> GetHolidaysByCountryCodeAsync(string countryCode = "FIN")
        {
            var nationalHolidays = await _reader.GetHolidaysByCountryCodeAsync();
            return nationalHolidays
                .Where(x => x.Key == countryCode)
                .SelectMany(v => v.Value);
        }
    }
}
