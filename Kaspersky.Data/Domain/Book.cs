using System;
using System.Collections.Generic;

namespace Kaspersky.Data.Domain
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public short Pages { get; set; }
        public string PublishingHouse { get; set; }
        public IEnumerable<Author> Autors { get; set; }
        public DateTime PublishDate { get; set; }
        public string Isbn { get; set; }
        public string ImageUrl { get; set; }
    }
}
