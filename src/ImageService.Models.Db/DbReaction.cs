using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.ImageService.Models.Db;

public class DbReaction
{
  public const string TableName = "Reactions";

  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Unicode { get; set; }
  public string Content { get; set; }
  public string Extension { get; set; }
  public Guid GroupId { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
  public Guid? ModifiedBy { get; set; }

  public DbReactionGroup Group { get; set; }
}

public class DbReactionConfiguration : IEntityTypeConfiguration<DbReaction>
{
  public void Configure(EntityTypeBuilder<DbReaction> builder)
  {
    builder
      .ToTable(DbReaction.TableName);

    builder
      .HasKey(r => r.Id);

    builder
      .HasOne(r => r.Group)
      .WithMany(rg => rg.Reactions);
  }
}
