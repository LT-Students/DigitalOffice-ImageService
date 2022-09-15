using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data;

public class ReactionRepository : IReactionRepository
{
  private readonly IDataProvider _provider;

  public ReactionRepository(IDataProvider provider)
  {
    _provider = provider;
  }

  private IQueryable<DbReaction> CreateFindReactionPredicates(
    FindReactionFilter filter,
    IQueryable<DbReaction> query)
  {
    if (!string.IsNullOrEmpty(filter.NameIncludeSubstring))
    {
      query = query.Where(rl => rl.Name.Contains(filter.NameIncludeSubstring));
    }

    if (!string.IsNullOrEmpty(filter.UnicodeIncludeSubstring))
    {
      query = query.Where(rl => rl.Unicode.Contains(filter.UnicodeIncludeSubstring));
    }

    if (!filter.IsAscendingSort)
    {
      query = query.OrderByDescending(rl => rl.Name);
    }

    if (filter.IsActive.HasValue)
    {
      query = query.Where(rl => rl.IsActive == filter.IsActive.Value);
    }

    return query;
  }

  public Task CreateAsync(DbReaction dbReaction)
  {
    if (dbReaction is null)
    {
      return null;
    }

    _provider.Reactions.Add(dbReaction);
    return _provider.SaveAsync();
  }

  public Task<DbReaction> GetAsync(Guid reactionId)
  {
    return _provider.Reactions.FirstOrDefaultAsync(x => x.Id == reactionId);
  }

  public async Task<(List<DbReaction> dbReactions, int totalCount)> FindReactionAsync(FindReactionFilter filter)
  {
    if (filter is null)
    {
      return (null, default);
    }

    IQueryable<DbReaction> dbReactionList = CreateFindReactionPredicates(
      filter,
      _provider.Reactions.OrderBy(x => x.Name).AsQueryable());

    return (
      await dbReactionList.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
      await dbReactionList.CountAsync());
  }

  public Task<bool> DoesSameNameExistAsync(string name)
  {
    return _provider.Reactions.AnyAsync(x => x.Name == name && x.IsActive);
  }

  public Task<int> CountReactionsInGroupAsync(Guid groupId)
  {
    return _provider.Reactions.Where(x => x.GroupId == groupId && x.IsActive).CountAsync();
  }
}
