using System;
using FluentValidation;
using Kaspersky.BL.Services;

namespace Kaspersky.Web.Models.Validators
{
    public class BookValidator : AbstractValidator<BookModel>
    {
        public BookValidator(IIsbnService isbnService)
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(b => b.Pages)
                .GreaterThan((short) 0)
                .LessThan((short) 10000);
            RuleFor(b => b.PublishingHouse)
                .MaximumLength(30);
            RuleFor(b => b.PublishDate)
                .GreaterThan(new DateTime(1800, 1, 1));
            RuleFor(b => b.Autors)
                .NotEmpty()
                .SetCollectionValidator(new AutorValidator());
            RuleFor(b => b.Isbn)
                .NotEmpty()
                .Must(isbnService.Validate)
                .WithMessage("ISBN - неправильный формат");
        }
    }
}
