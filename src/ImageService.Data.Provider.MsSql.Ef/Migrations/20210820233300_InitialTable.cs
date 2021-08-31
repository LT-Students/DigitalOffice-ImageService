using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef.Migrations
{
    [DbContext(typeof(ImageServiceDbContext))]
    [Migration("20210820233300_InitialTables")]
    class InitialTable : Migration
    {
        protected override void Up(MigrationBuilder builder)
        {
            CreateTable(builder, DbImageMessage.TableName);
            CreateTable(builder, DbImageNews.TableName);
            CreateTable(builder, DbImageProject.TableName);
            CreateTable(builder, DbImageUser.TableName);
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
