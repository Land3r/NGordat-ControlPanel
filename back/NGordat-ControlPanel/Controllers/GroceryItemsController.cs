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
  /// <see cref="GroceryItemsController"/> class.
  /// API Controller for <see cref="GroceryItem"/>.
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryItemsController : ControllerBase
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<GroceryItemsController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<GroceryItemsController> localizer;

    /// <summary>
    /// The <see cref="IGroceryItemService"/>.
    /// </summary>
    private readonly IGroceryItemService groceryItemService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryItemsController"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="groceryItemService">The <see cref="IGroceryItemService"/> to use.</param>
    public GroceryItemsController(
      IOptions<AppSettings> appSettings,
      ILogger<GroceryItemsController> logger,
      IStringLocalizer<GroceryItemsController> localizer,
      IGroceryItemService groceryItemService)
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

      if (groceryItemService == null)
      {
        throw new ArgumentNullException(nameof(groceryItemService));
      }
      else
      {
        this.groceryItemService = groceryItemService;
      }
    }

    /// <summary>
    /// Gets all the <see cref="GroceryItem"/>.
    /// GET: api/GroceryItems.
    /// </summary>
    /// <returns>The operation result.</returns>
    [HttpGet]
    public IActionResult Get()
    {
      this.logger.LogDebug(this.localizer["LogGetGroceryItemsTry"].Value);

      IEnumerable<GroceryItem> result;
      try
      {
        result = this.groceryItemService.Get();
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      this.logger.LogDebug(this.localizer["LogGetGroceryItemsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Gets a <see cref="GroceryItem"/>, based on it's id.
    /// GET: api/GroceryItems/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryItem"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogGetItemGroceryItemsTry"].Value);

      GroceryItem result;
      try
      {
        result = this.groceryItemService.Get(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetItemGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryItemsNotFound"].Value, id));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryItemsNotFound"].Value, id));
      }

      this.logger.LogDebug(this.localizer["LogGetItemGroceryItemsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Creates a new <see cref="GroceryItem"/>.
    /// POST: api/GroceryItems.
    /// </summary>
    /// <param name="model">The <see cref="GroceryItem"/> to create.</param>
    /// <returns>The operation result.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] GroceryItem model)
    {
      this.logger.LogDebug(this.localizer["LogPostGroceryItemsTry"].Value);

      GroceryItem result;
      try
      {
        result = this.groceryItemService.Create(model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPostGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryItemsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryItemsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPostGroceryItemsSuccess"].Value);
      return this.Created(new Uri(this.appSettings.Environment.BackUrl, $"api/GroceryItems/{model?.Id}"), result);
    }

    /// <summary>
    /// Updates a <see cref="GroceryItem"/>.
    /// PUT: api/GroceryItems/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryItem"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryItem"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] GroceryItem model)
    {
      this.logger.LogDebug(this.localizer["LogPutGroceryItemsTry"].Value);

      ReplaceOneResult result;
      try
      {
        result = this.groceryItemService.Update(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPutGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryItemsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryItemsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPutGroceryItemsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Updates partially a <see cref="GroceryItem"/>.
    /// PATCH: api/GroceryItems/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryItem"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryItem"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, [FromBody] GroceryItem model)
    {
      this.logger.LogDebug(this.localizer["LogPatchGroceryItemsTry"].Value);

      UpdateResult result;
      try
      {
        result = this.groceryItemService.UpdatePartially(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPatchGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryItemsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryItemsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPatchGroceryItemsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Deletes a <see cref="GroceryItem"/>.
    /// DELETE: api/GroceryItems/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryItem"/> to delete.</param>
    /// <returns>The operation result.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogDeleteGroceryItemsTry"].Value);

      DeleteResult result;
      try
      {
        result = this.groceryItemService.Remove(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogDeleteGroceryItemsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryItemsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryItemsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogDeleteGroceryItemsSuccess"].Value);
      return this.Ok(result);
    }
  }
}