namespace NGordatControlPanel.Settings
{
  using System;

  /// <summary>
  /// Classe <see cref="EnvironmentSettings"/>.
  /// Classe permettant de récupérer la configuration de l'environnement de l'application.
  /// </summary>
  public class EnvironmentSettings
  {
    /// <summary>
    /// Gets or sets the name of the environment.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the frontend url.
    /// </summary>
    public Uri FrontUrl { get; set; }

    /// <summary>
    /// Gets or sets the backend url.
    /// </summary>
    public Uri BackUrl { get; set; }
  }
}