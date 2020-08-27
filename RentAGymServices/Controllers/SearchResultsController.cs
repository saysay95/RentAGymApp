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
  public class SearchResultsController : ControllerBase
  {
    private ISearchResultsRepository repo;

    // constructor injects repository registered in Startup
    public SearchResultsController(ISearchResultsRepository repo)
    {
      this.repo = repo;
    }

    // GET: /SearchResults?spacetypes=mat&spacetypes=ring&spacetypes=oct&city=cergy
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Space>))]
    public async Task<IEnumerable<Space>> GetSearchResults([FromQuery] string[] spacetypes, string city)
    {
      return await repo.RetrieveAsync(spacetypes, city);
    }    
  }
}