using System;
using System.Collections.Generic;
using Kaspersky.Data.Domain;

namespace Kaspersky.Data.Common
{
    public static class DbInitializer
    {
        public static void Initialize(KasperskyContext context)
        {
            context.Database.EnsureCreated();

            var authors = new[]
            {
                new Author { Name = "Дойл", Surname = "Конан"},
                new Author { Name = "Эрих", Surname = "Ремарк" },
                new Author { Name = "Оскар", Surname = "Уайлд" }
            };

            var books = new[]
            {
                new Book
                {
                    Title = "Приключения Шерлока Холмса",
                    Autors = new List<Author> {authors[0]},
                    ImageUrl = "img/sh.png",
                    Pages = 480,
                    PublishDate = new DateTime(2016, 2, 15),
                    PublishingHouse = "Питер",
                    Isbn = "0-9752298-0-X"
                },
                new Book
                {
                    Title = "Три товарища",
                    Autors = new List<Author>  {authors[1]},
                    ImageUrl = "img/3t.png",
                    Pages = 560,
                    PublishDate = new DateTime(1990, 6, 1),
                    PublishingHouse = "Коммерсантъ",
                    Isbn = "99921-58-10-7"
                },
                new Book
                {
                    Title = "Портрет Дориана Грея",
                    Autors = new List<Author>  {authors[2]},
                    ImageUrl = "img/pdg.png",
                    Pages = 652,
                    PublishDate = new DateTime(2005, 1, 1),
                    PublishingHouse = "Панорама",
                    Isbn = "978-1-86197-876-9"
                }
            };

            foreach (var book in books)
            {
                context.Books.Add(book);
            }

            context.SaveChanges();
        }
    }
}
