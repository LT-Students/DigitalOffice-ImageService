﻿using LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageProject
{
    public class GetImageProjectCommand : IGetImageProjectCommand
    {
        private readonly IImageProjectRepository _imageProjectRepository;
        private readonly IImageDataResponseMapper _imageResponseMapper;

        public GetImageProjectCommand(
            IImageProjectRepository imageProjectRepository,
            IImageDataResponseMapper imageResponseMapper)
        {
            _imageProjectRepository = imageProjectRepository;
            _imageResponseMapper = imageResponseMapper;
        }

        public OperationResultResponse<ImageDataResponse> Execute(Guid parentId)
        {
            OperationResultResponse<ImageDataResponse> response = new();

            DbImagesProject dbImageProject = _imageProjectRepository.Get(parentId);

            if (dbImageProject == null)
            {
                response.Status = OperationResultStatusType.Failed;
                response.Errors.Add("Image was not found.");
                return response;
            }

            response.Body = _imageResponseMapper.Map(dbImageProject);
            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}