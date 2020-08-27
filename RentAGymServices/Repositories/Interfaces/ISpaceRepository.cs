using Shared;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentAGymServices.Repositories
{
  public interface ISpaceRepository
  {
    Task<Space> CreateAsync(Space space);
    Task<IEnumerable<Space>> RetrieveAllAsync();
    Task<Space> RetrieveAsync(string id);
    Task<Space> UpdateAsync(string id, Space space);
    Task<bool?> DeleteAsync(string id);
  }
}