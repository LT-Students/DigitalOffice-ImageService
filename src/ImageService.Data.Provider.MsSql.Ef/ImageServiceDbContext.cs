using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef
{
  public class ImageServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbImage> Images { get; set; }
    public DbSet<DbReaction> Reactions { get; set; }
    public DbSet<DbReactionGroup> ReactionsGroups { get ; set; }

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

    /*public void Save()
    {
      SaveChanges();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }*/

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    async Task IBaseDataProvider.SaveAsync()
    {
      await SaveChangesAsync();
    }

    public Task<int> ExecuteRawSqlAsync(string query)
    {
      return Database.ExecuteSqlRawAsync(query);
    }

    public IQueryable<DbImage> FromSqlRaw(string query)
    {
      return Images.FromSqlRaw(query);
    }
  }
}
