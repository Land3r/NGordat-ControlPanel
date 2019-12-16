namespace NGordatControlPanel.Settings
{
  using System;

  /// <summary>
  /// <see cref="EnvironmentSettings"/> class.
  /// Class used to retrieve configuration for the environment.
  /// </summary>
  public class EnvironmentSettings
  {
    /// <summary>
    /// Gets or sets the name of the environment of the <see cref="EnvironmentSettings"/>.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the frontend url of the <see cref="EnvironmentSettings"/>.
    /// </summary>
    public Uri FrontUrl { get; set; }

    /// <summary>
    /// Gets or sets the backend url of the <see cref="EnvironmentSettings"/>.
    /// </summary>
    public Uri BackUrl { get; set; }
  }
}