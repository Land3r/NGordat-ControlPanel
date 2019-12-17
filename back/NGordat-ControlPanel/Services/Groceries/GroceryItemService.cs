namespace NGordatControlPanel.Services.Groceries
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Entities.Groceries;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="GroceryItemService"/> class.
  /// Class service CRUD for <see cref="GroceryItem"/>.
  /// </summary>
  public class GroceryItemService : AMongoEntityLocalizedService<GroceryItem, GroceryItemService>, IGroceryItemService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "GroceryItems";

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryItemService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public GroceryItemService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<GroceryItemService> localizer,
      [FromServices] ILogger<GroceryItemService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
