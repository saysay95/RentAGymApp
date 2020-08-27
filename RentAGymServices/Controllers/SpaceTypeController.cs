using Microsoft.AspNetCore.Mvc;
using Shared;
using RentAGymServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentAGymServices.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SpaceTypesController : ControllerBase
  {
    private ISpaceTypeRepository repo;

    // constructor injects repository registered in Startup
    public SpaceTypesController(ISpaceTypeRepository repo)
    {
      this.repo = repo;
    }

    // GET: api/SpaceTypes
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<SpaceType>))]
    public async Task<IEnumerable<SpaceType>> GetSpaceTypes()
    {
        return await repo.RetrieveAllAsync();
    }
  }
}