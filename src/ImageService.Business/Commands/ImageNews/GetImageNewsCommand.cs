using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews
{
    public class GetImageNewsCommand : IGetImageNewsCommand
    {
        private readonly IImageNewsRepository _imageNewsRepository;
        private readonly IImageDataResponseMapper _imageNewsResponseMapper;
        public GetImageNewsCommand(
            IImageNewsRepository imageNewsRepository,
            IImageDataResponseMapper imageNewsResponseMapper)
        {
            _imageNewsRepository = imageNewsRepository;
            _imageNewsResponseMapper = imageNewsResponseMapper;
        }

        public OperationResultResponse<ImageDataResponse> Execute(Guid parentId)
        {
            OperationResultResponse<ImageDataResponse> response = new();

            DbImagesNews dbImageNews = _imageNewsRepository.Get(parentId);

            if (dbImageNews == null)
            {
                response.Body = null;
                response.Errors.Add("Image was not found.");
                return response;
            }

            response.Body = _imageNewsResponseMapper.Map(dbImageNews);
            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
