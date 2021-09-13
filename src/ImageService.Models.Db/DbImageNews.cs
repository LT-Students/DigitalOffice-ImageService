﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.ImageService.Models.Db
{
  public class DbImageNews
  {
    public const string TableName = "ImagesNews";

    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid CreatedBy { get; set; }
  }

  public class DbImageNewsConfiguration : IEntityTypeConfiguration<DbImageNews>
  {
    public void Configure(EntityTypeBuilder<DbImageNews> builder)
    {
      builder
        .ToTable(DbImageNews.TableName);

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
