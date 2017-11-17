using NUnit.Framework;

namespace Kaspersky.Tests.ServiceTests
{
    [TestFixture]
    public class IsbnValidatorTests : TestBase
    {

        [TestCase("978-1-56619-909-4", true)]
        [TestCase("978-1-5661-909-4", false)]
        [TestCase("978-1-5661-90933-4", false)]
        public void Isbn13Validator_ShouldWorkWithIsbn_IfIsbnLenghtIs13(string isbn, bool expected)
        {
            var result = Isbn13Validator.CanValidate(isbn);

            Assert.AreEqual(result, expected);
        }

        [TestCase("978-1-56619-909-4", false)]
        [TestCase("0-9752298-0-X", true)]
        [TestCase("0-9752298-0", false)]
        public void Isbn10Validator_ShouldWorkWithIsbn_IfIsbnLenghtIs10(string isbn, bool expected)
        {
            var result = Isbn10Validator.CanValidate(isbn);

            Assert.AreEqual(result, expected);
        }


        [TestCase("0-9752298-0-X")]
        [TestCase("99921-58-10-7")]
        [TestCase("978-1-56619-909-4")]
        [TestCase("978-1-86197-876-9")]
        public void IsbnService_ShouldReturnTrue_IfIsbnValid(string isbn)
        {
            var result = Service.Validate(isbn);

            Assert.IsTrue(result);
        }

        [TestCase("0-97598-0-X")]
        [TestCase("9992158-150-7")]
        [TestCase("978-1-56619-909-3")]
        [TestCase("")]
        public void IsbnService_ShouldNotValidateIsbn_IfIsbnNotValid(string isbn)
        {
            var result = Service.Validate(isbn);

            Assert.IsFalse(result);
        }
    }
}
