﻿namespace NGordatControlPanel.Services.Groceries
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="GroceryActionService"/> class.
  /// Class service CRUD for <see cref="GroceryAction"/>.
  /// </summary>
  public class GroceryActionService : AMongoEntityLocalizedService<GroceryAction, GroceryActionService>, IGroceryActionService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "GroceryActions";

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryActionService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public GroceryActionService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<GroceryActionService> localizer,
      [FromServices] ILogger<GroceryActionService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
