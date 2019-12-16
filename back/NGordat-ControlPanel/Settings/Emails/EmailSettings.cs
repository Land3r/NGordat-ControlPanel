namespace NGordatControlPanel.Settings.Emails
{
  /// <summary>
  /// <see cref="EmailSettings"/> class.
  /// Class used to retrieve configuration for the emails.
  /// </summary>
  public class EmailSettings
  {
    /// <summary>
    /// Gets or sets the <see cref="SmtpSettings"/> of the <see cref="EmailSettings"/>.
    /// </summary>
    public SmtpSettings Smtp { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="EmailFromSettings"/> of the <see cref="EmailSettings"/>.
    /// </summary>
    public EmailFromSettings From { get; set; }
  }
}
