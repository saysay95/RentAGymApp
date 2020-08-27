using Shared;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentAGymServices.Repositories
{
  public interface ISearchResultsRepository
  {
    Task<IEnumerable<Space>> RetrieveAsync(string[] spacetypes, string city);
  }
}