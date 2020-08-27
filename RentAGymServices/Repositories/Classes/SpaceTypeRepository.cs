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
  public class SpaceTypeRepository : ISpaceTypeRepository
  {
    // use a static thread-safe dictionary field to cache the SpaceTypees
    private static ConcurrentDictionary<string, SpaceType> spaceTypesCache;

    // use an instance data context field because it should not be 
    // cached due to their internal caching
    private RentAGym db;

    public SpaceTypeRepository(RentAGym db)
    {
      this.db = db;

      // pre-load SpaceTypes from database as a normal
      // Dictionary with AddressID is the key,
      // then convert to a thread-safe ConcurrentDictionary 
      if (spaceTypesCache == null)
      {
          spaceTypesCache = new ConcurrentDictionary<string, SpaceType>(
              db.SpaceTypes.ToDictionary(sp => sp.Type));
        // addressesCache = new ConcurrentDictionary<string, Address>(
        //   db.Addresses.ToDictionary(sp => Convert.ToString(sp.Type)));
      }
    }

    public Task<IEnumerable<SpaceType>> RetrieveAllAsync()
    {
      // for performance, get from cache
      return Task.Run<IEnumerable<SpaceType>>(
        () => spaceTypesCache.Values);
    }

    private SpaceType UpdateCache(string id, SpaceType sp)
    {
      SpaceType old;
      if (spaceTypesCache.TryGetValue(id, out old))
      {
        if (spaceTypesCache.TryUpdate(id, sp, old))
        {
          return sp;
        }
      }
      return null;
    }
  }
}