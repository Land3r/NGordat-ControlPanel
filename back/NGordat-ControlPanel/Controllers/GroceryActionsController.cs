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
  /// <see cref="GroceryActionsController"/> class.
  /// API Controller for <see cref="GroceryAction"/>.
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryActionsController : ControllerBase
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<GroceryActionsController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<GroceryActionsController> localizer;

    /// <summary>
    /// The <see cref="IGroceryActionService"/>.
    /// </summary>
    private readonly IGroceryActionService groceryActionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryActionsController"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="groceryActionService">The <see cref="IGroceryActionService"/> to use.</param>
    public GroceryActionsController(
      IOptions<AppSettings> appSettings,
      ILogger<GroceryActionsController> logger,
      IStringLocalizer<GroceryActionsController> localizer,
      IGroceryActionService groceryActionService)
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

      if (groceryActionService == null)
      {
        throw new ArgumentNullException(nameof(groceryActionService));
      }
      else
      {
        this.groceryActionService = groceryActionService;
      }
    }

    /// <summary>
    /// Gets all the <see cref="GroceryAction"/>.
    /// GET: api/GroceryActions.
    /// </summary>
    /// <returns>The operation result.</returns>
    [HttpGet]
    public IActionResult Get()
    {
      this.logger.LogDebug(this.localizer["LogGetGroceryActionsTry"].Value);

      IEnumerable<GroceryAction> result;
      try
      {
        result = this.groceryActionService.Get();
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetGroceryActionsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      this.logger.LogDebug(this.localizer["LogGetGroceryActionsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Gets a <see cref="GroceryAction"/>, based on it's id.
    /// GET: api/GroceryActions/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryAction"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogGetItemGroceryActionsTry"].Value);

      GroceryAction result;
      try
      {
        result = this.groceryActionService.Get(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetItemGroceryActionsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryActionsNotFound"].Value, id));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryActionsNotFound"].Value, id));
      }

      this.logger.LogDebug(this.localizer["LogGetItemGroceryActionsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Creates a new <see cref="GroceryAction"/>.
    /// POST: api/GroceryActions.
    /// </summary>
    /// <param name="model">The <see cref="GroceryAction"/> to create.</param>
    /// <returns>The operation result.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] GroceryAction model)
    {
      this.logger.LogDebug(this.localizer["LogPostGroceryActionsTry"].Value);

      GroceryAction result;
      try
      {
        result = this.groceryActionService.Create(model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPostGroceryActionsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryActionsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryActionsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPostGroceryActionsSuccess"].Value);
      return this.Created(new Uri(this.appSettings.Environment.BackUrl, $"api/groceryactions/{model.Id}"), result);
    }

    /// <summary>
    /// Updates a <see cref="GroceryAction"/>.
    /// PUT: api/GroceryActions/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryAction"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryAction"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] GroceryAction model)
    {
      this.logger.LogDebug(this.localizer["LogPutGroceryActionsTry"].Value);

      ReplaceOneResult result;
      try
      {
        result = this.groceryActionService.Update(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPutGroceryActionsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryActionsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryActionsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPutGroceryActionsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Updates partially a <see cref="GroceryAction"/>.
    /// PATCH: api/GroceryActions/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryAction"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryAction"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, [FromBody] GroceryAction model)
    {
      this.logger.LogDebug(this.localizer["LogPatchGroceryActionsTry"].Value);

      ReplaceOneResult result;
      try
      {
        result = this.groceryActionService.Update(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPatchGroceryActionsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryActionsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryActionsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPatchGroceryActionsSuccess"].Value);
      return this.Ok(result);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
