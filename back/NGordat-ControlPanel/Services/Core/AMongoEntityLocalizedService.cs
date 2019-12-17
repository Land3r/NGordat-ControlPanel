namespace NGordatControlPanel.Services.Core
{
  using System;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Entities.Db;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="AMongoEntityLocalizedService{TEntity, TService}"/> abstract class.
  /// Class to implement a service using a logger, localized ressources and a mongodb collection with CRUD operations.
  /// </summary>
  /// <typeparam name="TEntity">The underlying type of the mongodb entity.</typeparam>
  /// <typeparam name="TService">The underlying type of the service to log and localize.</typeparam>
  public abstract class AMongoEntityLocalizedService<TEntity, TService> : AMongoEntityService<TEntity, TService>, ILocalizedService<TService>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AMongoEntityLocalizedService{TEntity, TService}"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="collectionName">The name of the mongodb collection.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="localizer">The localized ressources.</param>
    public AMongoEntityLocalizedService(
      IOptions<AppSettings> appSettings,
      string collectionName,
      [FromServices] ILogger<TService> logger,
      [FromServices]IStringLocalizer<TService> localizer)
      : base(appSettings, collectionName, logger)
    {
      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }
      else
      {
        this.Localizer = localizer;
      }
    }

    /// <summary>
    /// Gets the localized ressources.
    /// </summary>
    public IStringLocalizer<TService> Localizer { get; private set; }
  }
}
