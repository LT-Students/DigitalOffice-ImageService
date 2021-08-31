using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ImageService.Data
{
    public class ImageUserRepository : IImageUserRepository
    {
        private readonly IDataProvider _provider;

        public ImageUserRepository(IDataProvider provider)
        {
            _provider = provider;
        }

        public List<Guid> Create(List<DbImageUser> imagesUsers)
        {
            if (imagesUsers.Contains(null))
            {
                return null;
            }

            _provider.ImagesUsers.AddRange(imagesUsers);
            _provider.Save();

            return imagesUsers.Select(x => x.Id).ToList();
        }

        public bool Remove(List<Guid> imageIds)
        {
            if (imageIds == null)
            {
                return false;
            }

            List<DbImageUser> imagesUsers = _provider.ImagesUsers
                .Where(x => imageIds.Contains(x.Id) || (x.ParentId != null && imageIds.Contains((Guid)x.ParentId)))
                .ToList();

            if (imagesUsers == null)
            {
                return false;
            }

            List<Guid> parentIds = new();

            foreach (DbImageUser imageUser in imagesUsers)
            {
                if (imageUser.ParentId != null
                    && imageUser.Id != imageUser.ParentId
                    && !imageIds.Contains((Guid)imageUser.ParentId))
                {
                    parentIds.Add((Guid)imageUser.ParentId);
                }
            }

            imagesUsers.AddRange(_provider.ImagesUsers.Where(x => parentIds.Contains(x.Id)));

            _provider.ImagesUsers.RemoveRange(imagesUsers);
            _provider.Save();

            return true;
        }

        public List<DbImageUser> Get(List<Guid> imageIds)
        {
            return _provider.ImagesUsers.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImageUser Get(Guid imageId)
        {
            return _provider.ImagesUsers.FirstOrDefault(x => x.Id == imageId);
        }
    }
}
