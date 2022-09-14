using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.ImageService.Models.Db;

public class DbReactionGroup
{
  public const string TableName = "ReactionsGroups";
  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public Guid CreatedBy { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
  public Guid? ModifiedBy { get; set; }

  public ICollection<DbReaction> Reactions { get; set; }

  public DbReactionGroup()
  {
    Reactions = new HashSet<DbReaction>();
  }
}

public class DbReactionGroupConfiguration : IEntityTypeConfiguration<DbReactionGroup>
{
 public void Configure(EntityTypeBuilder<DbReactionGroup> builder)
  {
    builder
      .ToTable(DbReactionGroup.TableName);

    builder
      .HasKey(rg => rg.Id);

    builder
      .HasMany(rg => rg.Reactions)
      .WithOne(r => r.Group);
  }
}
