using System;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(ImageServiceDbContext))]
  [Migration("20220804161500_InitialTables")]
  class InitialTables : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      CreateTableImages(builder, DBTablesNames.MESSAGE);
      CreateTableImages(builder, DBTablesNames.NEWS);
      CreateTableImages(builder, DBTablesNames.PROJECT);
      CreateTableImages(builder, DBTablesNames.USER);
      CreateTableImages(builder, DBTablesNames.TASK);
      CreateTableImages(builder, DBTablesNames.EDUCATION);
      CreateTableImages(builder, DBTablesNames.OFFICE);
      CreateTableReactions(builder);
      CreateTableReactionsGroups(builder);
    }

    protected override void Down(MigrationBuilder builder)
    {
      builder.DropTable(DbReaction.TableName);
      builder.DropTable(DbReactionGroup.TableName);
    }

    private static void CreateTableImages(MigrationBuilder builder, string tableName)
    {
      builder.CreateTable(
        name: tableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          ParentId = table.Column<Guid>(nullable: true),
          Name = table.Column<string>(nullable: true),
          Content = table.Column<string>(nullable: false),
          Extension = table.Column<string>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{tableName}", x => x.Id);
        });
    }

    private static void CreateTableReactions(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbReaction.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          Unicode = table.Column<string>(nullable: true),
          Content = table.Column<string>(nullable: false),
          Extension = table.Column<string>(nullable: false),
          GroupId = table.Column<Guid>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          ModifiedAtUtc = table.Column<DateTime?>(nullable: true),
          ModifiedBy = table.Column<Guid?>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbReaction.TableName}", x => x.Id);
        });
  }

    private static void CreateTableReactionsGroups(MigrationBuilder builder)
    {
      builder.CreateTable(
        name: DbReactionGroup.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          Name = table.Column<string>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          CreatedBy = table.Column<Guid>(nullable: false),
          ModifiedAtUtc = table.Column<DateTime?>(nullable: true),
          ModifiedBy = table.Column<Guid?>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbReactionGroup.TableName}", x => x.Id);
        });
    }
  }
}
