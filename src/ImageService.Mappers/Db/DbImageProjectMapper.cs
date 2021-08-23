﻿using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageProjectMapper : IDbImageProjectMapper
    {
        public DbImagesProject Map(Guid parentId, string name, string content, string extension, Guid createdBy)
        {
            return new DbImagesProject
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = name,
                Content = content,
                Extension = extension,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }
    }
}