namespace NGordatControlPanel.Settings.Emails
{
  /// <summary>
  /// <see cref="SmtpSettings"/> class.
  /// Class used to retrieve configuration for the email SMTP.
  /// </summary>
  public class SmtpSettings
  {
    /// <summary>
    /// Gets or sets the host of the <see cref="SmtpSettings"/> to connect to the SMTP.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the port of the <see cref="SmtpSettings"/> to connect to the SMTP.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username of the <see cref="SmtpSettings"/> to authenticate against SMTP.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the <see cref="SmtpSettings"/> to authenticate against SMTP.
    /// </summary>
    public string Password { get; set; }
  }
}
