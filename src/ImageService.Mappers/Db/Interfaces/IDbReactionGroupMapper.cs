using System.Collections.Generic;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;

[AutoInject]
public interface IDbReactionGroupMapper
{
  DbReactionGroup Map(CreateReactionGroupRequest request);
}
