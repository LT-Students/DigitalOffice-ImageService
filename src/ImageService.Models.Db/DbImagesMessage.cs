using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.ImageService.Models.Db
{
    public class DbImagesMessage
	{
        public const string TableName = "ImagesMessages";

        public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public string Extension { get; set; }
		public DateTime CreatedAtUtc { get; set; }
		public Guid CreatedBy { get; set; }
	}

    public class DbImagesConfigurationMessage : IEntityTypeConfiguration<DbImagesMessage>
    {
        public void Configure(EntityTypeBuilder<DbImagesMessage> builder)
        {
            builder
                .ToTable(DbImagesMessage.TableName);

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Content)
                .IsRequired();

            builder
                .Property(a => a.Extension)
                .IsRequired();
        }
    }
}
