using System;
using System.Collections.Generic;
using Kaspersky.Web.Models;
using Kaspersky.Web.Models.Validators;
using NUnit.Framework;

namespace Kaspersky.Tests.ModelValidatorTests
{
	[TestFixture]
	public class ModelValidatorTests : TestBase
	{
		[Test]
		public void BookValidator_ShouldReturnTrue_IfModelValid()
		{
			var bookModel = ValidBook();

			var validator = new BookValidator(Service);
			var result = validator.Validate(bookModel);

			Assert.IsTrue(result.IsValid);
		}

		[Test, TestCaseSource(nameof(InvalidBookModels))]
		public void BookValidator_ShouldReturnFalse_IfModelValid(BookModel bookModel)
		{
			var validator = new BookValidator(Service);
			var result = validator.Validate(bookModel);

			Assert.IsFalse(result.IsValid);
		}

		[Test]
		public void AuthorValidator_ShouldReturnTrue_IfModelValid()
		{
			var autorModel = new AutorModel { Name = "Alex", Surname = "Gray" };

			var validator = new AutorValidator();
			var results = validator.Validate(autorModel);

			Assert.IsTrue(results.IsValid);
		}

		[TestCase("Alex", "There_is_whery_long_surname_more_than_20_symbols")]
		[TestCase("", "Gray")]
		[TestCase("Alex", "")]
		public void AuthorValidator_ShouldReturnFalse_IfModelInvalid(string name, string surname)
		{
			var autorModel = new AutorModel { Name = name, Surname = surname };

			var validator = new AutorValidator();
			var results = validator.Validate(autorModel);

			Assert.IsFalse(results.IsValid);
		}

		public static Func<BookModel> ValidBook => () => new BookModel
		{
			Title = "Три товарища",
			Autors = new[] { new AutorModel { Name = "Alex", Surname = "Step" } },
			ImageUrl = "",
			Pages = 560,
			PublishDate = new DateTime(1990, 6, 1),
			PublishingHouse = "Коммерсантъ",
			Isbn = "99921-58-10-7"
		};

		public static IEnumerable<BookModel> InvalidBookModels()
		{
			var model = ValidBook();

			//Empty title
			model.Title = string.Empty;
			yield return model;

			//long title
			model = ValidBook();
			model.Title = "There is invalid title with very invalid long name";
			yield return model;

			//empty autor
			model = ValidBook();
			model.Autors = null;
			yield return model;

			//pages = -10
			model = ValidBook();
			model.Pages = -10;
			yield return model;

			//pages > 10000
			model = ValidBook();
			model.Pages = 10001;
			yield return model;

			//publication date < 1800
			model = ValidBook();
			model.PublishDate = new DateTime(1700, 1, 1);
			yield return model;

			//invalid PublishingHouse 
			model = ValidBook();
			model.PublishingHouse = "There is invalid PublishingHouse with very invalid long name";
			yield return model;

			//Empty Isbn 
			model = ValidBook();
			model.Isbn = string.Empty;
			yield return model;

			//invalid Isbn 
			model = ValidBook();
			model.Isbn = "99921-58-10-1";
			yield return model;
		}

	}
}