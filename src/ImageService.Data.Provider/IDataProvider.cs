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
        DbSet<DbImagesUser> ImagesUsers { get; set; }
        DbSet<DbImagesProject> ImagesProjects { get; set; }
        DbSet<DbImagesNews> ImagesNews { get; set; }
        DbSet<DbImagesMessage> ImagesMessages { get; set; }
    }
}
