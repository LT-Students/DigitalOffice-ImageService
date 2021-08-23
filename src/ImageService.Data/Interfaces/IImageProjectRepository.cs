using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageProjectRepository
    {
        List<DbImagesProject> Get(List<Guid> Id);

        bool Delete(DbImagesProject imagesProject);

        List<Guid> Create(List<DbImagesProject> imagesProject);

        DbImagesProject Get(Guid imageId);
    }
}
