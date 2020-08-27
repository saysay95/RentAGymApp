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
  public class CityRepository : ICityRepository
  {
    private RentAGym db;

    public CityRepository(RentAGym db)
    {
      this.db = db;
    }

    public Task<IEnumerable<City>> RetrieveAllAsync(string name)
    {
      ConcurrentDictionary<string, City> dic = new ConcurrentDictionary<string, City>(
              db.Cities
              .Where(city => city.Name.ToLower().StartsWith(name.ToLower()))
              .Take(10)
              .ToDictionary(c => Convert.ToString(c.CityId)));

      return Task.Run<IEnumerable<City>>(
        () => dic.Values);
    }
  }
}