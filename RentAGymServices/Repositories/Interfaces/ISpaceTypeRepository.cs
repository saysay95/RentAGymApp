using Shared;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentAGymServices.Repositories
{
  public interface ISpaceTypeRepository
  {
    Task<IEnumerable<SpaceType>> RetrieveAllAsync();
  }
}