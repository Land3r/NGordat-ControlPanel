namespace NGordatControlPanel.Services.Emails
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Emails;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="EmailTemplateService"/> class.
  /// Class service CRUD for <see cref="EmailTemplate"/>.
  /// </summary>
  public class EmailTemplateService : AMongoEntityLocalizedService<EmailTemplate, EmailTemplateService>, IEmailTemplateService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "EmailTemplates";

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public EmailTemplateService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<EmailTemplateService> localizer,
      [FromServices] ILogger<EmailTemplateService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }

    /// <summary>
    /// Gets a <see cref="EmailTemplate"/>, based on it's name.
    /// </summary>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <returns>The <see cref="EmailTemplate"/>, if found.</returns>
    public EmailTemplate GetByName(string templateName)
    {
      return this.Entities.Find(template => template.Name == templateName).FirstOrDefault();
    }
  }
}
