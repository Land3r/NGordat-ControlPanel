namespace NGordatControlPanel.Settings
{
  /// <summary>
  /// <see cref="JWTSettings"/> class.
  /// Class used to retrieve configuration for JWT token.
  /// </summary>
  public class JWTSettings
  {
    /// <summary>
    /// Gets or sets the secret key used for generating JWT tokens of the <see cref="JWTSettings"/>.
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Gets or sets the JWT token valid duration (in days) of the <see cref="JWTSettings"/>.
    /// </summary>
    public int DurationInDays { get; set; }
  }
}
