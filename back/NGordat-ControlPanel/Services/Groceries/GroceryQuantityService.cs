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
  /// <see cref="GroceryQuantityService"/> class.
  /// Class service CRUD for <see cref="GroceryQuantity"/>.
  /// </summary>
  public class GroceryQuantityService : AMongoEntityLocalizedService<GroceryQuantity, GroceryQuantityService>, IGroceryQuantityService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "GroceryQuantities";

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryQuantityService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public GroceryQuantityService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<GroceryQuantityService> localizer,
      [FromServices] ILogger<GroceryQuantityService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
