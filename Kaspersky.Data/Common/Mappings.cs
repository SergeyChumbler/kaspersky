using Kaspersky.Data.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaspersky.Data.Common
{
    public static class Mapping
    {
        public static EntityTypeBuilder<Author> Map(this EntityTypeBuilder<Author> entity)
        {
            entity.BaseMap();
            entity.Property(t => t.Name)
                .HasMaxLength(20)
                .IsRequired();
            entity.Property(t => t.Surname)
                .HasMaxLength(20)
                .IsRequired();

            return entity;
        }

        public static EntityTypeBuilder<Book> Map(this EntityTypeBuilder<Book> entity)
        {
            entity.BaseMap();
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsRequired();
            entity.Property(e => e.Pages)
                .IsRequired();
            entity.Property(e => e.PublishDate)
                .IsRequired();
            entity.Property(e => e.PublishingHouse)
                .IsRequired();
            entity.Property(e => e.Isbn)
                .IsRequired();

            return entity;
        }

        private static void BaseMap<T>(this EntityTypeBuilder<T> entity) where T : class, IEntity
        {
            entity.HasKey(e => e.Id);
        }
    }
}
