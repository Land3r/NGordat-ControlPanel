namespace NGordatControlPanel.Services.Emails
{
  using NGordatControlPanel.Entities.Emails;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IEmailTemplateService"/> interface.
  /// Interface service CRUD for <see cref="EmailTemplate"/>.
  /// </summary>
  public interface IEmailTemplateService : ICrudService<EmailTemplate>
  {
    /// <summary>
    /// Gets a <see cref="EmailTemplate"/>, based on it's name.
    /// </summary>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <returns>The <see cref="EmailTemplate"/>, if found.</returns>
    EmailTemplate GetByName(string templateName);
  }
}