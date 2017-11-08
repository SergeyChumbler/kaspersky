using Kaspersky.BL.Services;
using Kaspersky.BL.Services.Impl;
using NUnit.Framework;

namespace Kaspersky.Tests
{
	[TestFixture]
    public class TestBase
    {
	    protected IIsbnService Service;
	    protected IIsbnValidator Isbn13Validator;
	    protected IIsbnValidator Isbn10Validator;
	    [SetUp]
	    public void Init()
	    {
		    Isbn13Validator = new Isbn13Validator();
		    Isbn10Validator = new Isbn10Validator();
		    Service = new IsbnService(new[] { Isbn13Validator, Isbn10Validator });
	    }
	}
}
