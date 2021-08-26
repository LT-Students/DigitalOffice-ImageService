using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImageNews dbImagesNews)
        {
            if (dbImagesNews == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImagesNews.Id,
                Content = dbImagesNews.Content,
                Name = dbImagesNews.Name,
                Extension = dbImagesNews.Extension
            };
        }

        public ImageDataResponse Map(DbImageMessage dbImageMessage)
        {
            if (dbImageMessage == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImageMessage.Id,
                Content = dbImageMessage.Content,
                Name = dbImageMessage.Name,
                Extension = dbImageMessage.Extension
            };
        }

        public ImageDataResponse Map(DbImageProject dbImageProject)
        {
            if (dbImageProject == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImageProject.Id,
                Content = dbImageProject.Content,
                Name = dbImageProject.Name,
                Extension = dbImageProject.Extension
            };
        }

        public ImageDataResponse Map(DbImageUser dbImagesUser)
        {
            if (dbImagesUser == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImagesUser.Id,
                Content = dbImagesUser.Content,
                Name = dbImagesUser.Name,
                Extension = dbImagesUser.Extension
            };
        }
    }
}
