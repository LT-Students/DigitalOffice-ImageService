using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.Kernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbImage> Images { get; set; }

    Task<int> ExecuteRawSqlAsync(string query);

    IQueryable<DbImage> FromSqlRaw(string query);
  }
}
