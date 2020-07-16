using Microsoft.AspNetCore.Mvc;
using Shared;
using RentAGymService.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentAGymService.Controllers
{
  // base address: api/Addresses 
  [Route("api/[controller]")]
  [ApiController]
  public class AddressesController : ControllerBase
  {
    private IAddressRepository repo;

    // constructor injects repository registered in Startup
    public AddressesController(IAddressRepository repo)
    {
      this.repo = repo;
    }

    // GET: api/Addresses
    // GET: api/Addresses/?country=[country] 
    // this will always return a list of Addresses even if its empty
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Address>))]
    public async Task<IEnumerable<Address>> GetAddresses(string country)
    {
      if (string.IsNullOrWhiteSpace(country))
      {
        return await repo.RetrieveAllAsync();
      }
      else
      {
        return (await repo.RetrieveAllAsync())
          .Where(address => address.Country == country);
      }
    }

    // GET: api/Addresses/[id] 
    [HttpGet("{id}", Name = nameof(GetAddress))] // named route
    [ProducesResponseType(200, Type = typeof(Address))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAddress(string id)
    {
      Address c = await repo.RetrieveAsync(id);
      if (c == null)
      {
        return NotFound(); // 404 Resource not found
      }
      return Ok(c); // 200 OK with customer in body
    }

    // POST: api/Addresses
    // BODY: Address (JSON, XML) 
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Address))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Address c)
    {
      if (c == null)
      {
        return BadRequest(); // 400 Bad request
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); // 400 Bad request
      }

      Address added = await repo.CreateAsync(c);

      return CreatedAtRoute( // 201 Created
        routeName: nameof(GetAddress),
        routeValues: new { id = added.AddressId.ToString().ToLower() },
        value: added);
    }

    // PUT: api/Addresses/[id]
    // BODY: Address (JSON, XML) 
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(
      string id, [FromBody] Address c)
    {
    //   id = id.ToUpper();
    //   c.AddressId = (int)c.AddressId.ToString().ToUpper();

      if (c == null || c.AddressId.ToString() != id)
      {
        return BadRequest(); // 400 Bad request
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState); // 400 Bad request
      }

      var existing = await repo.RetrieveAsync(id);

      if (existing == null)
      {
        return NotFound(); // 404 Resource not found
      }

      await repo.UpdateAsync(id, c);

      return new NoContentResult(); // 204 No content
    }

    // DELETE: api/Addresses/[id] 
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
      if (id == "bad")
      {
        var problemDetails = new ProblemDetails
        {
          Status = StatusCodes.Status400BadRequest,
          Type = "https://localhost:5001/Addresses/failed-to-delete",
          Title = $"Address ID {id} found but failed to delete.",
          Detail = "More details like Company Name, Country and so on.",
          Instance = HttpContext.Request.Path
        };
        return BadRequest(problemDetails); // 400 Bad request
      }

      var existing = await repo.RetrieveAsync(id);
      if (existing == null)
      {
        return NotFound(); // 404 Resource not found
      }

      bool? deleted = await repo.DeleteAsync(id);

      if (deleted.HasValue && deleted.Value) // short circuit AND
      {
        return new NoContentResult(); // 204 No content
      }
      else
      {
        return BadRequest( // 400 Bad request
          $"Address {id} was found but failed to delete.");
      }
    }
  }
}