using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.ImageService.Models.Db
{
    public class DbImageMessage
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

    public class DbImageMessageConfiguration : IEntityTypeConfiguration<DbImageMessage>
    {
        public void Configure(EntityTypeBuilder<DbImageMessage> builder)
        {
            builder
                .ToTable(DbImageMessage.TableName);

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
