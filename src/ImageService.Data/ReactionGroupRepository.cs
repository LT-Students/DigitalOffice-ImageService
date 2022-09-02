using System;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ReactionGroupRepository : IReactionGroupRepository
  {
    private readonly IDataProvider _provider;
    private readonly IReactionRepository _reactionRepository;

    public ReactionGroupRepository(
      IDataProvider provider,
      IReactionRepository reactionRepository)
    {
      _provider = provider;
      _reactionRepository = reactionRepository;
    }

    private IQueryable<DbReactionGroup> CreateGetReactionGroupPredicates(
      GetReactionGroupFilter filter,
      IQueryable<DbReactionGroup> query)
    {
      query = query.Where(rgl => rgl.Id == filter.GroupId);

      if (filter.IncludeReactions)
      {
        /*if (filter.IncludeActiveReactions.HasValue)
        {
          query = query.Include(rgl => rgl.Reactions.Where(rl => rl.IsActive == filter.IncludeActiveReactions.Value).OrderBy(rl => rl.Name));
        }
        else
        {
          query = query.Include(rgl => rgl.Reactions.OrderBy(rl => rl.Name));
        }*/
      }

      return query;
    }

    public async Task<Guid?> CreateAsync(DbReactionGroup dbReactionGroup)
    {
      if (dbReactionGroup is null)
      {
        return null;
      }

      _provider.ReactionsGroups.Add(dbReactionGroup);
      await _provider.SaveAsync();

      return dbReactionGroup.Id;
    }

    public Task<DbReactionGroup> GetAsync(GetReactionGroupFilter filter)
    {
      if (filter is null)
      {
        return null;
      }

      return CreateGetReactionGroupPredicates(
        filter, _provider.ReactionsGroups.OrderByDescending(x => x.CreatedAtUtc).AsQueryable())
        .FirstOrDefaultAsync();
    }

    public Task<bool> DoesExistAsync(Guid? groupId)
    {
      return groupId is null
        ? Task.FromResult(false)
        : _provider.ReactionsGroups.AnyAsync(x => x.Id == groupId && x.IsActive == true);
    }

    public Task<bool> DoesSameNameExistAsync(string name)
    {
      return _provider.ReactionsGroups.AnyAsync(x => x.Name == name && x.IsActive == true);
    }

    public Task<Guid> PickGroupAsync()
    {
      return _provider.ReactionsGroups.AnyAsync(x => _reactionRepository.CountReactionsInGroupAsync(x.Id).Result < 16 && x.IsActive == true).Result
        ? _provider.ReactionsGroups.Where(x => x.IsActive == true).Select(x => x.Id)
          .FirstAsync(x => _reactionRepository.CountReactionsInGroupAsync(x).Result < 16)
        : null;
    }
  }
}
