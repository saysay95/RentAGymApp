using Shared;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentAGymServices.Repositories
{
  public interface ICityRepository
  {
    Task<IEnumerable<City>> RetrieveAllAsync(string name);
  }
}