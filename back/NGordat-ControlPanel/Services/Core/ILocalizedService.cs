namespace NGordatControlPanel.Services.Core
{
  using System;
  using System.Globalization;

  using Microsoft.Extensions.Localization;

  /// <summary>
  /// <see cref="ILocalizedService{TService}"/> interface.
  /// Interface used to access a service using localized ressources.
  /// </summary>
  /// <typeparam name="TService">Le type du service à localiser.</typeparam>
  public interface ILocalizedService<TService>
  {
    /// <summary>
    /// Gets the localized ressources.
    /// </summary>
    IStringLocalizer<TService> Localizer { get; }

    /// <summary>
    /// Gets the localized ressource value in the current culture.
    /// </summary>
    /// <param name="key">The name of the localisation key.</param>
    /// <returns>The corresponding text that matches the key.</returns>
    public string GetLocalized(string key)
    {
      string result = this.Localizer[key]?.Value;

      if (result == null)
      {
        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "La clé {0} n'existe pas dans le fichier de ressource.", key));
      }
      else
      {
        return result;
      }
    }

    /// <summary>
    /// Gets the localized ressource value in the current culture.
    /// </summary>
    /// <param name="key">The name of the localisation key.</param>
    /// <param name="args">The args to give to the string.Format to format message.</param>
    /// <returns>The corresponding text that matches the key.</returns>
    public string GetLocalized(string key, params string[] args)
    {
      string result = this.Localizer[key]?.Value;

      if (result == null)
      {
        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "La clé {0} n'existe pas dans le fichier de ressource.", key));
      }
      else
      {
        return string.Format(CultureInfo.InvariantCulture, result, args);
      }
    }
  }
}
