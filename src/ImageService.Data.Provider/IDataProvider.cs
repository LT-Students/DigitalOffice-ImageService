using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Database;
using LT.DigitalOffice.Kernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data.Provider
{
    [AutoInject(InjectType.Scoped)]
    public interface IDataProvider : IBaseDataProvider
    {
        DbSet<DbImageUser> ImagesUsers { get; set; }
        DbSet<DbImageProject> ImagesProjects { get; set; }
        DbSet<DbImageNews> ImagesNews { get; set; }
        DbSet<DbImageMessage> ImagesMessages { get; set; }
    }
}
