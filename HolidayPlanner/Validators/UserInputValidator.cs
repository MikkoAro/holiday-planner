using System.ComponentModel.DataAnnotations;

namespace HolidayPlanner.Validators
{
    public interface IUserInputValidator
    {
        public (DateTime from, DateTime to) ValidateRequest(string? userInput);
    }

    public class UserInputValidator : IUserInputValidator
    {
        public (DateTime from, DateTime to) ValidateRequest(string? userInput)
        {
            var (from, to) = TryParseInput(userInput);

            if (from >= to)
                throw new ValidationException("Start date cannot be after end date");
            
            if ((to - from).Days >= 50)
                throw new ValidationException("Time period can't exceed 50 days");

            if (!IsInHolidayPeriod(from, to))
                throw new ValidationException("Time period must be within holiday season");

            return (from, to);
        }

        private static (DateTime from, DateTime to) TryParseInput(string? userInput)
        {
            if (userInput is null)
                throw new ArgumentNullException(nameof(userInput));

            try
            {
                var userInputDates = userInput.Replace(" ", "").Split("-");
                return (DateTime.Parse(userInputDates[0]), DateTime.Parse(userInputDates[1]));

            }
            catch (Exception)
            {
                throw new ValidationException("Invalid time period given, see example.");
            }
        }

        private static bool IsInHolidayPeriod(DateTime from, DateTime to)
        {
            if (from.Year != to.Year)
                return true;

            var holidayPeriodChangeDate = new DateTime(from.Year, 4, 1);
            return from >= holidayPeriodChangeDate || to < holidayPeriodChangeDate;
        }
    }
}
