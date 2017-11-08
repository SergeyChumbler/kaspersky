using Kaspersky.Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kaspersky.Data.Common
{
	public class KasperskyContext : DbContext
	{
		public KasperskyContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Author>().Map();
			modelBuilder.Entity<Book>().Map();
		}

		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
	}
}
