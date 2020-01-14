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
  /// <see cref="GroceryMeaninglessWordsController"/> class.
  /// API Controller for <see cref="GroceryMeaninglessWord"/>.
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryMeaninglessWordsController : ControllerBase
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<GroceryMeaninglessWordsController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<GroceryMeaninglessWordsController> localizer;

    /// <summary>
    /// The <see cref="IGroceryMeaninglessWordService"/>.
    /// </summary>
    private readonly IGroceryMeaninglessWordService groceryMeaninglessWordService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryMeaninglessWordsController"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="groceryMeaninglessWordService">The <see cref="IGroceryMeaninglessWordService"/> to use.</param>
    public GroceryMeaninglessWordsController(
      IOptions<AppSettings> appSettings,
      ILogger<GroceryMeaninglessWordsController> logger,
      IStringLocalizer<GroceryMeaninglessWordsController> localizer,
      IGroceryMeaninglessWordService groceryMeaninglessWordService)
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

      if (groceryMeaninglessWordService == null)
      {
        throw new ArgumentNullException(nameof(groceryMeaninglessWordService));
      }
      else
      {
        this.groceryMeaninglessWordService = groceryMeaninglessWordService;
      }
    }

    /// <summary>
    /// Gets all the <see cref="GroceryMeaninglessWord"/>.
    /// GET: api/GroceryMeaninglessWord.
    /// </summary>
    /// <returns>The operation result.</returns>
    [HttpGet]
    public IActionResult Get()
    {
      this.logger.LogDebug(this.localizer["LogGetGroceryMeaninglessWordsTry"].Value);

      IEnumerable<GroceryMeaninglessWord> result;
      try
      {
        result = this.groceryMeaninglessWordService.Get();
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      this.logger.LogDebug(this.localizer["LogGetGroceryMeaninglessWordsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Gets a <see cref="GroceryMeaninglessWord"/>, based on it's id.
    /// GET: api/GroceryMeaninglessWords/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryMeaninglessWord"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogGetItemGroceryMeaninglessWordsTry"].Value);

      GroceryMeaninglessWord result;
      try
      {
        result = this.groceryMeaninglessWordService.Get(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogGetItemGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryMeaninglessWordsNotFound"].Value, id));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetItemGroceryMeaninglessWordsNotFound"].Value, id));
      }

      this.logger.LogDebug(this.localizer["LogGetItemGroceryMeaninglessWordsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Creates a new <see cref="GroceryMeaninglessWord"/>.
    /// POST: api/GroceryMeaninglessWords.
    /// </summary>
    /// <param name="model">The <see cref="GroceryMeaninglessWord"/> to create.</param>
    /// <returns>The operation result.</returns>
    [HttpPost]
    public IActionResult Post([FromBody] GroceryMeaninglessWord model)
    {
      this.logger.LogDebug(this.localizer["LogPostGroceryMeaninglessWordsTry"].Value);

      GroceryMeaninglessWord result;
      try
      {
        result = this.groceryMeaninglessWordService.Create(model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPostGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryMeaninglessWordsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPostGroceryMeaninglessWordsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPostGroceryMeaninglessWordsSuccess"].Value);
      return this.Created(new Uri(this.appSettings.Environment.BackUrl, $"api/grocerymeaninglesswords/{model?.Id}"), result);
    }

    /// <summary>
    /// Updates a <see cref="GroceryMeaninglessWord"/>.
    /// PUT: api/GroceryMeaninglessWords/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryMeaninglessWord"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryMeaninglessWord"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] GroceryMeaninglessWord model)
    {
      this.logger.LogDebug(this.localizer["LogPutGroceryMeaninglessWordsTry"].Value);

      ReplaceOneResult result;
      try
      {
        result = this.groceryMeaninglessWordService.Update(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPutGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryMeaninglessWordsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPutGroceryMeaninglessWordsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPutGroceryMeaninglessWordsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Updates partially a <see cref="GroceryMeaninglessWord"/>.
    /// PATCH: api/GroceryMeaninglessWords/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryMeaninglessWord"/> to update.</param>
    /// <param name="model">The updated data of the <see cref="GroceryMeaninglessWord"/>.</param>
    /// <returns>The operation result.</returns>
    [HttpPatch("{id}")]
    public IActionResult Patch(Guid id, [FromBody] GroceryMeaninglessWord model)
    {
      this.logger.LogDebug(this.localizer["LogPatchGroceryMeaninglessWordsTry"].Value);

      UpdateResult result;
      try
      {
        result = this.groceryMeaninglessWordService.UpdatePartially(id, model);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogPatchGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryMeaninglessWordsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPatchGroceryMeaninglessWordsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogPatchGroceryMeaninglessWordsSuccess"].Value);
      return this.Ok(result);
    }

    /// <summary>
    /// Deletes a <see cref="GroceryMeaninglessWord"/>.
    /// DELETE: api/GroceryMeaninglessWords/1a1a61cb-1ce4-4b17-8817-75832c3bbb87.
    /// </summary>
    /// <param name="id">The id of the <see cref="GroceryMeaninglessWord"/> to delete.</param>
    /// <returns>The operation result.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
      this.logger.LogDebug(this.localizer["LogDeleteGroceryMeaninglessWordsTry"].Value);

      DeleteResult result;
      try
      {
        result = this.groceryMeaninglessWordService.Remove(id);
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, this.localizer["LogDeleteGroceryMeaninglessWordsError"].Value);
        return this.Problem(
          statusCode: (int)HttpStatusCode.InternalServerError,
          title: ex.ToString(),
          detail: ex.StackTrace);
      }

      if (result == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryMeaninglessWordsNotFound"].Value));
        return this.NotFound(string.Format(CultureInfo.InvariantCulture, this.localizer["LogDeleteGroceryMeaninglessWordsNotFound"].Value));
      }

      this.logger.LogDebug(this.localizer["LogDeleteGroceryMeaninglessWordsSuccess"].Value);
      return this.Ok(result);
    }
  }
}
