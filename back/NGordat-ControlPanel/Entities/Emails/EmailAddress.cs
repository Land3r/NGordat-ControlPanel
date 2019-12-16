namespace NGordatControlPanel.Entities.Emails
{
  /// <summary>
  /// <see cref="EmailAddress"/> class.
  /// Class representing a user implied in a email exchange.
  /// It can be a recipient or sender.
  /// </summary>
  public class EmailAddress
  {
    /// <summary>
    /// Gets or sets the email address of the <see cref="EmailAddress"/>.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets the name of the <see cref="EmailAddress"/>.
    /// </summary>
    public string Name { get; set; }
  }
}
