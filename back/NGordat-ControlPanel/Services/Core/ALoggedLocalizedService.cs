namespace NGordatControlPanel.Services.Core
{
  using System;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;

  /// <summary>
  /// <see cref="ALoggedLocalizedService{TService}"/> abstract class.
  /// Class to implement a service using a logger and localized ressources.
  /// </summary>
  /// <typeparam name="TService">The underlying type of the service to log and localize.</typeparam>
  public abstract class ALoggedLocalizedService<TService> : ALoggedService<TService>, ILocalizedService<TService>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ALoggedLocalizedService{TService}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="localizer">The localized ressources.</param>
    public ALoggedLocalizedService(
      ILogger<TService> logger,
      [FromServices]IStringLocalizer<TService> localizer)
      : base(logger)
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
