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
  public class GroceryActionsController : ControllerBase
  {
    private readonly IGroceryActionService groceryActionService;

    private readonly ILogger<GroceryActionsController> logger;

    private readonly IStringLocalizer<GroceryActionsController> localizer;

    public GroceryActionsController(
      IGroceryActionService groceryActionService,
      ILogger<GroceryActionsController> logger,
      IStringLocalizer<GroceryActionsController> localizer)
    {
      this.groceryActionService = groceryActionService;
      this.logger = logger;
      this.localizer = localizer;
    }

    // GET: api/GroceryActions
    [HttpGet]
    public IActionResult Get()
    {
      return Ok(groceryActionService.Get());
    }

    // GET: api/GroceryActions/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
      return "value";
    }

    // POST: api/GroceryActions
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/GroceryActions/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
