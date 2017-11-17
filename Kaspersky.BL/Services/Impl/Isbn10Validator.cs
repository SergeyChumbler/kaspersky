using System.Linq;

namespace Kaspersky.BL.Services.Impl
{
    public class Isbn10Validator : IIsbnValidator
    {
        public bool CanValidate(string isbn) => isbn.Replace("-", string.Empty).Length == 10;

        public bool IsValid(string isbn)
        {
            isbn = isbn.Replace("-", string.Empty).ToLower();

            if (isbn.Last() == 'x')
            {
                if (!long.TryParse(isbn.Substring(0, isbn.Length - 1), out var numbers))
                    return false;

                return GetSumm(numbers) % 11 == 10;
            }
            else
            {

                if (!long.TryParse(isbn, out var numbers))
                    return false;

                var checkSum = numbers % 10;
                numbers = numbers / 10;

                return GetSumm(numbers) % 11 == checkSum;
            }
        }

        private static long GetSumm(long numbers)
        {
            var sum = 0L;

            for (var i = 8; i >= 0; i--)
            {
                sum += numbers % 10 * (i + 1);
                numbers /= 10;
            }

            return sum;
        }
    }
}
