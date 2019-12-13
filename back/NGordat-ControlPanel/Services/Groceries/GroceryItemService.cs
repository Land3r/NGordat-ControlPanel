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
  /// <see cref="GroceryItemService"/> service class.
  /// Service used to operate <see cref="GroceryItem"/>.
  /// </summary>
  public class GroceryItemService : AMongoEntityLocalizedService<GroceryItem, GroceryItemService>, IGroceryItemService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string CollectionName = "GroceryItems";

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="GroceryItemService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public GroceryItemService(
      [FromServices]IStringLocalizer<GroceryItemService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<GroceryItemService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
