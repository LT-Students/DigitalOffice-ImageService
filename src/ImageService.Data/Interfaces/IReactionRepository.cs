using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces;

[AutoInject]
public interface IReactionRepository
{
  Task CreateAsync(DbReaction dbReaction);
  Task<DbReaction> GetAsync(Guid reactionId);
  Task<(List<DbReaction> dbReactions, int totalCount)> FindReactionAsync(FindReactionFilter filter);
  Task<bool> DoesSameNameExistAsync(string name);
  Task<int> CountReactionsInGroupAsync(Guid groupId);
}
