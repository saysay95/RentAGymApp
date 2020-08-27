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
  public class SpaceController : ControllerBase
  {
    private ISpaceRepository repo;

    // constructor injects repository registered in Startup
    public SpaceController(ISpaceRepository repo)
    {
      this.repo = repo;
    }

    // GET: api/Space/[id] 
    [HttpGet("{id}", Name = nameof(GetSpace))] // named route
    [ProducesResponseType(200, Type = typeof(Address))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetSpace(string id)
    {
      Space space = await repo.RetrieveAsync(id);
      if (space == null)
      {
        return NotFound(); // 404 Resource not found
      }
      return Ok(space); // 200 OK with customer in body
    }  

    // POST: api/Space
    // BODY: Space (JSON, XML) 
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Space))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Space space)
    {
      if (space == null)
      {
        return BadRequest(); // 400 Bad request
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); // 400 Bad request
      }

      Space added = await repo.CreateAsync(space);

      return CreatedAtRoute( // 201 Created
        routeName: nameof(GetSpace),
        routeValues: new { id = added.SpaceId.ToString().ToLower() },
        value: added);
    }
  }
}