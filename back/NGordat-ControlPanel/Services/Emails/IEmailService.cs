namespace NGordatControlPanel.Services.Emails
{
  using NGordatControlPanel.Entities.Emails;

  /// <summary>
  /// <see cref="EmailService"/> interface.
  /// Interface service to handle email exchanges.
  /// </summary>
  public interface IEmailService
  {
    /// <summary>
    /// Sends an email, based on an html template.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <param name="values">The values to inject into template for compiling.</param>
    void SendTemplate(EmailAddress address, string templateName, dynamic values);

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    void Send(EmailAddress address, string subject, string body);

    /// <summary>
    /// Try to sends an email, based on an html template.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <param name="values">The values to inject into template for compiling.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    bool TrySendTemplate(EmailAddress address, string templateName, dynamic values);

    /// <summary>
    /// Try to sends an email.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    bool TrySend(EmailAddress address, string subject, string body);
  }
}
