﻿using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses
{
    public class ImageNewsResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
    }
}
