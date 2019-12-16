namespace NGordatControlPanel.Settings
{
  /// <summary>
  /// <see cref="SecuritySettings"/> class.
  /// Class used to retrieve configuration for security.
  /// </summary>
  public class SecuritySettings
  {
    /// <summary>
    /// Gets or sets the salt used for hash protocols of the <see cref="SecuritySettings"/>.
    /// </summary>
    public string HashSalt { get; set; }

    /// <summary>
    /// Gets or sets the reset password token duration (in minutes) of the <see cref="SecuritySettings"/>.
    /// </summary>
    public int ResetPasswordTokenDurationInMinutes { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="JWTSettings"/> of the <see cref="SecuritySettings"/>.
    /// </summary>
    public JWTSettings JWT { get; set; }
  }
}