using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef
{
    public class ImageServiceDbContext : DbContext, IDataProvider
    {
        public DbSet<DbImagesUser> ImagesUsers { get; set; }
        public DbSet<DbImagesProject> ImagesProjects { get; set; }
        public DbSet<DbImagesNews> ImagesNews { get; set; }
        public DbSet<DbImagesMessage> ImagesMessages { get; set; }

        public ImageServiceDbContext(DbContextOptions<ImageServiceDbContext> options)
            : base(options)
        {
        }

        // Fluent API is written here.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.ImageService.Models.Db"));
        }

        public void EnsureDeleted()
        {
            Database.EnsureDeleted();
        }

        public bool IsInMemory()
        {
            return Database.IsInMemory();
        }

        public object MakeEntityDetached(object obj)
        {
            Entry(obj).State = EntityState.Detached;
            return Entry(obj).State;
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
