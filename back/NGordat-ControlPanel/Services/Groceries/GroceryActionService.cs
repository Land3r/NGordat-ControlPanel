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
  /// <see cref="GroceryActionService"/> service class.
  /// Service used to operate <see cref="GroceryAction"/>.
  /// </summary>
  public class GroceryActionService : AMongoEntityLocalizedService<GroceryAction, GroceryActionService>, IGroceryActionService
  {
    /// <summary>
    /// Le nom de la collection mongo.
    /// </summary>
    private const string CollectionName = "GroceryActions";

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="GroceryActionService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources de localisation.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger utilisé par le service.</param>
    public GroceryActionService(
      [FromServices]IStringLocalizer<GroceryActionService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<GroceryActionService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
