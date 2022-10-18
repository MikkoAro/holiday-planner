using System.Runtime.Serialization;
using System.Text.Json;

namespace HolidayPlanner.Readers
{
    public interface INationalHolidaysReader
    {
        Task<Dictionary<string, IEnumerable<DateTime>>> GetHolidaysByCountryCodeAsync();
    }

    public class NationalHolidaysReader : INationalHolidaysReader
    {
        public Dictionary<string, IEnumerable<DateTime>> NationalHolidays { get; set; }

        public NationalHolidaysReader()
        {
            NationalHolidays = new Dictionary<string, IEnumerable<DateTime>>();
        }

        public async Task<Dictionary<string, IEnumerable<DateTime>>> GetHolidaysByCountryCodeAsync()
        {
            if (NationalHolidays.Count == 0)
            {
                using var reader = new StreamReader("NationalHolidays.json");
                var json = await reader.ReadToEndAsync();
                var deserialized =
                    JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(json);

                NationalHolidays = deserialized is null
                    ? throw new SerializationException("Could not read national holidays")
                    : deserialized.ToDictionary(
                        k => k.Key,
                        v => v.Value.Select(DateTime.Parse));
            }

            return NationalHolidays;
        }
    }
}
