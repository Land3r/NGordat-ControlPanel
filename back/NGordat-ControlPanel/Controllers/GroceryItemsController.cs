using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NGordatControlPanel.Services.Groceries;

namespace NGordat_ControlPanel.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryItemsController : ControllerBase
  {
    private readonly IGroceryItemService groceryItemService;

    private readonly ILogger<GroceryItemsController> logger;

    private readonly IStringLocalizer<GroceryItemsController> localizer;

    public GroceryItemsController(
      IGroceryItemService groceryItemService,
      ILogger<GroceryItemsController> logger,
      IStringLocalizer<GroceryItemsController> localizer)
    {
      this.groceryItemService = groceryItemService;
      this.logger = logger;
      this.localizer = localizer;
    }

    // GET: api/GroceryItems
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(groceryItemService.Get());
    }

    //// GET: api/GroceryItems/5
    //[HttpGet("{id}", Name = "Get")]
    //public string Get(int id)
    //{
    //  return "value";
    //}

    //// POST: api/GroceryItems
    //[HttpPost]
    //public void Post([FromBody] string value)
    //{
    //}

    //// PUT: api/GroceryItems/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{
    //}

    //// DELETE: api/ApiWithActions/5
    //[HttpDelete("{id}")]
    //public void Delete(int id)
    //{
    //}
  }
}
