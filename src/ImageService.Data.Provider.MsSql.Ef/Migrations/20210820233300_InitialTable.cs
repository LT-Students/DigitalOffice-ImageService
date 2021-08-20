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
            builder.CreateTable(
                name: DbImagesUser.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<Guid>(nullable: true),
                    Extentions = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesUser", x => x.Id);
                });

            builder.CreateTable(
                name: DbImagesNews.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<Guid>(nullable: true),
                    Extentions = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesNews", x => x.Id);
                });

            builder.CreateTable(
                name: DbImagesMessage.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<Guid>(nullable: true),
                    Extentions = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesMessage", x => x.Id);
                });

            builder.CreateTable(
                name: DbImagesProject.TableName,
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<Guid>(nullable: true),
                    Extentions = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesProject", x => x.Id);
                });
        }
    }
}
