using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces;

[AutoInject]
public interface IReactionGroupRepository
{
  Task CreateAsync(DbReactionGroup dbReactionGroup);
  Task<DbReactionGroup> GetAsync(GetReactionGroupFilter filter);
  Task<bool> DoesExistAsync(Guid groupId);
  Task<bool> DoesSameNameExistAsync(string name);
  Guid PickGroup();
}
