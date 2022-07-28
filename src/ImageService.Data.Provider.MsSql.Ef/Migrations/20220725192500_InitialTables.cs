using System;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(ImageServiceDbContext))]
  [Migration("20220725192500_InitialTables")]
  class InitialTables : Migration
  {
    protected override void Up(MigrationBuilder builder)
    {
      CreateTable(builder, DBTablesNames.MESSAGE);
      CreateTable(builder, DBTablesNames.NEWS);
      CreateTable(builder, DBTablesNames.PROJECT);
      CreateTable(builder, DBTablesNames.USER);
      CreateTable(builder, DBTablesNames.TASK);
      CreateTable(builder, DBTablesNames.EDUCATION);
      CreateTable(builder, DBTablesNames.OFFICE);
      CreateTable(builder, DBTablesNames.REACTION);
    }

    private static void CreateTable(MigrationBuilder builder, string tableName)
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
  }
}
