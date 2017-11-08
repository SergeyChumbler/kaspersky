using System;
using System.Collections.Generic;

namespace Kaspersky.Web.Models
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public short Pages { get; set; }
		public string PublishingHouse { get; set; }
		public IEnumerable<AutorModel> Autors { get; set; }
		public DateTime PublishDate { get; set; }
		public string Isbn { get; set; }
		public string ImageUrl { get; set; }
	}
}
