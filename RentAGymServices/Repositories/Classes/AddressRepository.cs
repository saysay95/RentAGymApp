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
  public class AddressRepository : IAddressRepository
  {
    // use a static thread-safe dictionary field to cache the addresses
    private static ConcurrentDictionary<string, Address> addressesCache;

    // use an instance data context field because it should not be 
    // cached due to their internal caching
    private RentAGym db;

    public AddressRepository(RentAGym db)
    {
      this.db = db;

      // pre-load addresses from database as a normal
      // Dictionary with AddressID is the key,
      // then convert to a thread-safe ConcurrentDictionary 
      if (addressesCache == null)
      {
        addressesCache = new ConcurrentDictionary<string, Address>(
          db.Addresses.ToDictionary(c => Convert.ToString(c.AddressId)));
      }
    }

    public async Task<Address> CreateAsync(Address c)
    {
      // add to database using EF Core
      EntityEntry<Address> added = await db.Addresses.AddAsync(c);
      int affected = await db.SaveChangesAsync();
      if (affected == 1)
      {
        // if the customer is new, add it to cache, else
        // call UpdateCache method
        return addressesCache.AddOrUpdate(Convert.ToString(c.AddressId), c, UpdateCache);
      }
      else
      {
        return null;
      }
    }

    public Task<IEnumerable<Address>> RetrieveAllAsync()
    {
      // for performance, get from cache
      return Task.Run<IEnumerable<Address>>(
        () => addressesCache.Values);
    }

    public Task<Address> RetrieveAsync(string id)
    {
      return Task.Run(() =>
      {
        // for performance, get from cache
        id = id.ToUpper();
        Address c;
        addressesCache.TryGetValue(id, out c);
        return c;
      });
    }

    private Address UpdateCache(string id, Address c)
    {
      Address old;
      if (addressesCache.TryGetValue(id, out old))
      {
        if (addressesCache.TryUpdate(id, c, old))
        {
          return c;
        }
      }
      return null;
    }

    public async Task<Address> UpdateAsync(string id, Address c)
    {
      // normalize address ID
      id = id.ToUpper();
      // c.AddressId = c.AddressId.ToUpper();

      // update in database
      db.Addresses.Update(c);
      int affected = await db.SaveChangesAsync();

      if (affected == 1)
      {
        // update in cache
        return UpdateCache(id, c);
      }
      return null;
    }

    public async Task<bool?> DeleteAsync(string id)
    {
      id = id.ToUpper();

      // remove from database
      Address c = db.Addresses.Find(id);
      db.Addresses.Remove(c);
      int affected = await db.SaveChangesAsync();

      if (affected == 1)
      {
        // remove from cache
        return addressesCache.TryRemove(id, out c);
      }
      else
      {
        return null;
      }
    }
  }
}