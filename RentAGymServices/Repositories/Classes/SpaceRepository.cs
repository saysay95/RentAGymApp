using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System;
using RentAGymServices.Repositories;

namespace RentAGymServices.Repositories
{
  public class SpaceRepository : ISpaceRepository
  {
    // use a static thread-safe dictionary field to cache the Spaces
    private static ConcurrentDictionary<string, Space> spacesCaches;

    // use an instance data context field because it should not be 
    // cached due to their internal caching
    private RentAGym db;

    public SpaceRepository(RentAGym db)
    {
      this.db = db;

      // pre-load spaces from database as a normal
      // Dictionary with SpaceID is the key,
      // then convert to a thread-safe ConcurrentDictionary 
      if (spacesCaches == null)
      {
        spacesCaches = new ConcurrentDictionary<string, Space>(
          db.Spaces.ToDictionary(s => Convert.ToString(s.SpaceId)));
      }
    }

    public async Task<Space> CreateAsync(Space s)
    {
      // add to database using EF Core
      EntityEntry<Space> added = await db.Spaces.AddAsync(s);
      int affected = await db.SaveChangesAsync();
      if (affected == 1)
      {
        // if the space is new, add it to cache, else
        // call UpdateCache method
        return spacesCaches.AddOrUpdate(Convert.ToString(s.AddressId), s, UpdateCache);
      }
      else
      {
        return null;
      }
    }

    public Task<IEnumerable<Space>> RetrieveAllAsync()
    {
      // for performance, get from cache
      return Task.Run<IEnumerable<Space>>(
        () => spacesCaches.Values);
    }

    public Task<Space> RetrieveAsync(string id)
    {
      return Task.Run(() =>
      {
        // for performance, get from cache
        id = id.ToUpper();
        Space s;
        spacesCaches.TryGetValue(id, out s);
        return s;
      });
    }

    private Space UpdateCache(string id, Space space)
    {
      Space oldSpace;
      if (spacesCaches.TryGetValue(id, out oldSpace))
      {
        if (spacesCaches.TryUpdate(id, space, oldSpace))
        {
          return space;
        }
      }
      return null;
    }

    public async Task<Space> UpdateAsync(string id, Space space)
    {
      // normalize address ID
      id = id.ToUpper();
      // c.AddressId = c.AddressId.ToUpper();

      db.Spaces.Update(space);
      int affected = await db.SaveChangesAsync();

      if (affected == 1)
      {
        return UpdateCache(id, space);
      }
      return null;
    }

    public async Task<bool?> DeleteAsync(string id)
    {
      id = id.ToUpper();

      Space space = db.Spaces.Find(id);
      db.Spaces.Remove(space);
      int affected = await db.SaveChangesAsync();

      if (affected == 1)
      {
        return spacesCaches.TryRemove(id, out space);
      }
      else
      {
        return null;
      }
    }
  }
}