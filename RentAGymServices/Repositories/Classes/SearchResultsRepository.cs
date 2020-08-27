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
  public class SearchResultsRepository : ISearchResultsRepository
  {
    private RentAGym db;

    public SearchResultsRepository(RentAGym db)
    {
      this.db = db;
    }

    public Task<IEnumerable<Space>> RetrieveAsync(string[] spacetypes, string city)
    {
      ConcurrentDictionary<string, Space> dic = new ConcurrentDictionary<string, Space>();

      var spaces = db.Spaces
                  .Join(db.Addresses,
                        space => space.AddressId,
                        address => address.AddressId,
                        (space, address) => new
                        {
                            Id = space.SpaceId,
                            Name = space.Name,
                            Description = space.Description,
                            City = address.City
                        }
                  )
                 .Where(a => a.City.ToLower().Equals(city.ToLower()))
                  .Take(10);
      foreach(var s in spaces)
      {
        Space sp = new Space {SpaceId = s.Id, 
                              Name = s.Name,
                              Description = s.Description};
        dic.TryAdd(sp.SpaceId.ToString(), sp);
      }

// var Track = db.Track
//             .Join(db.MediaType,
//                 o => o.MediaTypeId,
//                 i => i.MediaTypeId,
//                 (o, i) =>
//                 new
//                 {
//                     Name = o.Name,
//                     Composer = o.Composer,
//                     MediaType = i.Name
//                 }
//             ).Take(5);
 
//         foreach (var t in Track)
//         {
//             Console.WriteLine("{0} {1} {2}", t.Name, t.Composer, t.MediaType);
//         }
      // var res = from space in db.Spaces
      //       join address in db.Addresses
      //         on space.AddressId equals address.AddressId
      //         into MatchedSpaces
      //         select space;

      //         Space sp = res;


      // ConcurrentDictionary<string, Space> dic = new ConcurrentDictionary<string, Space>(
      //   res).ToDictionary(s => Convert.ToString(s.SpaceId));

      // ConcurrentDictionary<string, Space> dic = new ConcurrentDictionary<string, Space>(
      //         db.Spaces
      //         .Join(db.Addresses,
      //               space => space.AddressId,
      //               address => address.AddressId,
      //               (Space)=> new
      //               {
      //                   City = address.City,
      //                   SpaceId = space.SpaceId
      //               }

      //               // (space, address) => new
      //               // {
      //                   // City = address.City,
      //                   // SpaceId = space.SpaceId
      //               // }
      //         )
      //         //.Where(address => address.City.ToLower().Equals(city.ToLower()))
      //         .Take(10)
      //         .ToDictionary(s => Convert.ToString(s.SpaceId)));

      return Task.Run<IEnumerable<Space>>(
        () => dic.Values);
    }
  }
}