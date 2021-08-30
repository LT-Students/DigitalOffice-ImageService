using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LT.DigitalOffice.ImageService.Models.Db
{
    public class DbImageUser
    {
        public const string TableName = "ImagesUsers";

        public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public string Extension { get; set; }
		public DateTime CreatedAtUtc { get; set; }
		public Guid CreatedBy { get; set; }
	}

    public class DbImageUserConfiguration : IEntityTypeConfiguration<DbImageUser>
    {
        public void Configure(EntityTypeBuilder<DbImageUser> builder)
        {
            builder
                .ToTable(DbImageUser.TableName);

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
