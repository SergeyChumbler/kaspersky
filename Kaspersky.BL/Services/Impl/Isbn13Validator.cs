namespace Kaspersky.BL.Services.Impl
{
    public class Isbn13Validator : IIsbnValidator
    {
        public bool CanValidate(string isbn) => isbn.Replace("-", string.Empty).Length == 13;

        public bool IsValid(string isbn)
        {
            if (!long.TryParse(isbn.Replace("-", string.Empty), out var numbers))
                return false;

            var sum = 0L;
            var checkSum = numbers % 10;
            numbers = numbers / 10;

            for (var i = 11; i >= 0; i--)
            {
                sum += numbers % 10 * (i % 2 * 2 + 1);
                numbers /= 10;
            }

            var checkDigit = 10 - sum % 10;
            if (checkDigit == 10)
                checkDigit = 0;

            return checkDigit == checkSum;
        }
    }

}
