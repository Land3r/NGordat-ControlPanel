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
  /// <see cref="GroceryMeaninglessWordService"/> class.
  /// Class service CRUD for <see cref="GroceryMeaninglessWord"/>.
  /// </summary>
  public class GroceryMeaninglessWordService : AMongoEntityLocalizedService<GroceryMeaninglessWord, GroceryMeaninglessWordService>, IGroceryMeaninglessWordService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "GroceryMeaninglessWords";

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryMeaninglessWordService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public GroceryMeaninglessWordService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<GroceryMeaninglessWordService> localizer,
      [FromServices] ILogger<GroceryMeaninglessWordService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }
  }
}
