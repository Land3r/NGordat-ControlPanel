namespace NGordatControlPanel.Services.Core
{
  using System;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;

  /// <summary>
  /// <see cref="ALocalizedService{T}"/> abstract class.
  /// Class to implement a service using localized ressources.
  /// </summary>
  /// <typeparam name="TService">The underlying type of the service to localize.</typeparam>
  public abstract class ALocalizedService<TService> : ILocalizedService<TService>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ALocalizedService{T}"/> class.
    /// </summary>
    /// <param name="localizer">The localized ressources.</param>
    public ALocalizedService([FromServices] IStringLocalizer<TService> localizer)
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
