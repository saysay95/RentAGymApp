using Microsoft.AspNetCore.Mvc;
using Shared;
using RentAGymServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentAGymServices.Controllers
{
  // base address: api/Addresses 
  [Route("api/[controller]")]
  [ApiController]
  public class CitiesController : ControllerBase
  {
    private ICityRepository repo;

    // constructor injects repository registered in Startup
    public CitiesController(ICityRepository repo)
    {
      this.repo = repo;
    }

    // GET: api/Cities
    // GET: api/Cities/?name=[name] 
    // this will always return a list of cities even if its empty
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
    public async Task<IEnumerable<City>> GetCities(string name)
    {
      return await repo.RetrieveAllAsync(name);
    }    
  }
}