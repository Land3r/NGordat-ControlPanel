namespace NGordatControlPanel.Settings.Emails
{
  /// <summary>
  /// <see cref="EmailFromSettings"/> class.
  /// Class used to retrieve configuration for the from field for emails.
  /// </summary>
  public class EmailFromSettings
  {
    /// <summary>
    /// Gets or sets the address of the from field of the <see cref="EmailFromSettings"/>.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the name of the from field of the <see cref="EmailFromSettings"/>.
    /// </summary>
    public string Name { get; set; }
  }
}
