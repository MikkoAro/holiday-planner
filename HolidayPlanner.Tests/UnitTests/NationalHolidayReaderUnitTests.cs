using HolidayPlanner.Readers;
using Xunit;

namespace HolidayPlanner.Tests.UnitTests
{
    public class NationalHolidayReaderUnitTests
    {
        [Fact]
        public async Task Should_Read_National_Holidays_From_JSON()
        {
            var reader = new NationalHolidaysReader();
            var result = await reader.GetHolidaysByCountryCodeAsync();
            
            Assert.NotEmpty(result);
            Assert.Contains(result, x => x.Key == "FIN");
            Assert.Equal(21, result["FIN"].Count());
        }
    }
}
