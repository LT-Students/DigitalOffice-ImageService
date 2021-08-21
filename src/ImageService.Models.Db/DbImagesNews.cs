using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.ImageService.Models.Db
{
    public class DbImagesNews
    {
        public const string TableName = "ImagesNews";

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Extentions { get; set; }
        public DateTime CreatedAtUTC { get; set; }
        public Guid CreatedBy { get; set; }
    }

    public class DbImagesConfigurationNews : IEntityTypeConfiguration<DbImagesNews>
    {
        public void Configure(EntityTypeBuilder<DbImagesNews> builder)
        {
            builder
                .ToTable(DbImagesNews.TableName);

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Content)
                .IsRequired();

            builder
                .Property(a => a.Extentions)
                .IsRequired();
        }
    }
}
