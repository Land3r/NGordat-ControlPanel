namespace NGordatControlPanel.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Net;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Groceries;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="GroceryQuantitiesController"/> class.
  /// API Controller for <see cref="GroceryQuantity"/>.
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryQuantitiesController : ControllerBase
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<GroceryQuantitiesController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<GroceryQuantitiesController> localizer;

    /// <summary>
    /// The <see cref="IGroceryQuantityService"/>.
    /// </summary>
    private readonly IGroceryQuantityService groceryQuantityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryQuantitiesController"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="groceryQuantityService">The <see cref="IGroceryQuantityService"/> to use.</param>
    public GroceryQuantitiesController(
      IOptions<AppSettings> appSettings,
      ILogger<GroceryQuantitiesController> logger,
      IStringLocalizer<GroceryQuantitiesController> localizer,
      IGroceryQuantityService groceryQuantityService)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings.Value;
      }

      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }
      else
      {
        this.logger = logger;
      }

      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }
      else
      {
        this.localizer = localizer;
      }

      if (groceryQuantityService == null)
      {
        throw new ArgumentNullException(nameof(groceryQuantityService));
      }
      else
      {
        this.groceryQuantityService = groceryQuantityService;
      }
    }

    /// <summary>
    /// Gets all the <see cref="GroceryQuantity"/>.
    /// GET: api/GroceryQuantity.
    /// </summary>
    /// <returns>The operation result.</returns>
    [HttpGet]
    public IActionResult Get()
    {
      this.logger.LogDebug(this.localizer["LogGetGroceryQuantitiesTry"].Value);

      IEnumerable<GroceryQuantity> result;
      try
      {
        result = this.groceryQuantityService.Get();
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      this.logger.LogDebug(this.localizer["LogGetGroceryQuantitiesSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Gets a <see cref="GroceryQuantity"/>, based on it's id.
    /// GET: api/GroceryQuantities/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryQuantity"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogGetItemGroceryQuantitiesTry"].Value);

      GroceryQuantity result;
      try
      {
        result = this.groceryQuantityService.Get(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetItemGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryQuantitiesNotFound"].Value, id));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryQuantitiesNotFound"].Value, id));
      }

      this.logger.LogDebug(this.localizer["LogGetItemGroceryQuantitiesSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Creates a new <see cref="GroceryQuantity"/>.
    /// POST: api/GroceryQuantities.
    /// </summary>
    /// <param name="model">The <see cref="GroceryQuantity"/> to create.</param>
    /// <returns>The operation result.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] GroceryQuantity model)
    {
      this.logger.LogDebug(this.localizer["LogPostGroceryQuantitiesTry"].Value);

      GroceryQuantity result;
      try
      {
        result = this.groceryQuantityService.Create(model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPostGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryQuantitiesNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryQuantitiesNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPostGroceryQuantitiesSuccess"].Value);
      return this.Created(new Uri(this.appSettings.Environment.BackUrl, $"api/groceryquantities/{model?.Id}"), result);
    }

    /// <summary>
    /// Updates a <see cref="GroceryQuantity"/>.
    /// PUT: api/GroceryQuantities/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryQuantity"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryQuantity"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] GroceryQuantity model)
    {
      this.logger.LogDebug(this.localizer["LogPutGroceryQuantitiesTry"].Value);

      ReplaceOneResult result;
      try
      {
        result = this.groceryQuantityService.Update(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPutGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryQuantitiesNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryQuantitiesNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPutGroceryQuantitiesSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Updates partially a <see cref="GroceryQuantity"/>.
    /// PATCH: api/GroceryQuantities/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryQuantity"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryQuantity"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, [FromBody] GroceryQuantity model)
    {
      this.logger.LogDebug(this.localizer["LogPatchGroceryQuantitiesTry"].Value);

      UpdateResult result;
      try
      {
        result = this.groceryQuantityService.UpdatePartially(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPatchGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryQuantitiesNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryQuantitiesNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPatchGroceryQuantitiesSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Deletes a <see cref="GroceryQuantity"/>.
    /// DELETE: api/GroceryQuantities/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryQuantity"/> to delete.</param>
    /// <returns>The operation result.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogDeleteGroceryQuantitiesTry"].Value);

      DeleteResult result;
      try
      {
        result = this.groceryQuantityService.Remove(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogDeleteGroceryQuantitiesError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryQuantitiesNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryQuantitiesNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogDeleteGroceryQuantitiesSuccess"].Value);
      return this.Ok(result);
    }
  }
}
