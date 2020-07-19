using Shared;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentAGymServices.Repositories
{
  public interface IAddressRepository
  {
    Task<Address> CreateAsync(Address c);
    Task<IEnumerable<Address>> RetrieveAllAsync();
    Task<Address> RetrieveAsync(string id);
    Task<Address> UpdateAsync(string id, Address c);
    Task<bool?> DeleteAsync(string id);
  }
}