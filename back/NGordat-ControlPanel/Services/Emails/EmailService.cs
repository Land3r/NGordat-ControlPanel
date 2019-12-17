namespace NGordatControlPanel.Services.Emails
{
  using System;

  using MailKit.Net.Smtp;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using MimeKit;
  using MimeKit.Text;

  using NGordatControlPanel.Entities.Emails;
  using NGordatControlPanel.Extensions;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="EmailService"/> class.
  /// Class service to handle email exchanges.
  /// </summary>
  public class EmailService : ALoggedLocalizedService<EmailService>, IEmailService, IDisposable
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The <see cref="IEmailTemplateService"/>.
    /// </summary>
    private readonly IEmailTemplateService emailTemplateService;

    /// <summary>
    /// The <see cref="SmtpClient"/>.
    /// </summary>
    private readonly SmtpClient smtpClient = new SmtpClient();

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="emailTemplateService">The <see cref="IEmailTemplateService"/> to use.</param>
    public EmailService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<EmailService> localizer,
      [FromServices] ILogger<EmailService> logger,
      IEmailTemplateService emailTemplateService)
      : base(logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings.Value;
      }

      if (emailTemplateService == null)
      {
        throw new ArgumentNullException(nameof(emailTemplateService));
      }
      else
      {
        this.emailTemplateService = emailTemplateService;
      }
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="EmailService"/> class.
    /// </summary>
    ~EmailService()
    {
      this.Dispose(false);
    }

    /// <summary>
    /// Gets a value indicating whether the service is connected to the SMTP Server.
    /// </summary>
    private bool IsConnected
    {
      get { return this.smtpClient.IsConnected; }
    }

    /// <summary>
    /// Sends an email, based on an html template.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <param name="values">The values to inject into template for compiling.</param>
    public void SendTemplate(EmailAddress address, string templateName, dynamic values)
    {
      if (string.IsNullOrEmpty(templateName))
      {
        throw new ArgumentNullException(nameof(templateName));
      }

      EmailTemplate emailTemplate = this.emailTemplateService.GetByName(templateName);
      if (emailTemplate == null)
      {
        throw new ApplicationException($"EmailTemplate nammed {templateName} not found.");
      }

      // We need to call explicitly the extension method because it uses dynamic type parameters.
      var email = EmailTemplateExtension.Compile(emailTemplate, values);

      // Item1 is the email subject.
      // Item2 is the email body.
      this.Send(address, email.Item1, email.Item2);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    public void Send(EmailAddress address, string subject, string body)
    {
      if (address == null)
      {
        throw new ArgumentNullException(nameof(address));
      }
      else if (string.IsNullOrEmpty(subject))
      {
        throw new ArgumentNullException(nameof(subject));
      }
      else if (body == null)
      {
        throw new ArgumentNullException(nameof(body));
      }

      MimeMessage message = new MimeMessage();

      // From
      message.From.Add(new MailboxAddress(this.appSettings.Email.From.Name, this.appSettings.Email.From.Address));

      // To
      // TODO: Faire une vérif de la validitée des emails avant.
      if (!string.IsNullOrEmpty(address?.Name))
      {
        message.To.Add(new MailboxAddress(address.Name, address.Address));
      }
      else
      {
        message.To.Add(new MailboxAddress(address.Address));
      }

      // Subject
      message.Subject = subject;

      // Body
      message.Body = new TextPart(TextFormat.Html)
      {
        Text = body,
      };

      if (!this.IsConnected)
      {
        this.Connect();
      }

      this.smtpClient.Send(message);
    }

    /// <summary>
    /// Try to sends an email.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool TrySend(EmailAddress address, string subject, string body)
    {
      try
      {
        this.Send(address, subject, body);
        return true;
      }
      catch (SmtpCommandException ex)
      {
        // TODO : Localiser
        this.Logger.LogCritical(ex, "Error while sending email.");
        return false;
      }
    }

    /// <summary>
    /// Try to sends an email, based on an html template.
    /// </summary>
    /// <param name="address">The <see cref="EmailAddress"/> of the person to send email to.</param>
    /// <param name="templateName">The name of the <see cref="EmailTemplate"/>.</param>
    /// <param name="values">The values to inject into template for compiling.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    public bool TrySendTemplate(EmailAddress address, string templateName, dynamic values)
    {
      try
      {
        this.SendTemplate(address, templateName, values);
        return true;
      }
      catch (SmtpCommandException ex)
      {
        // TODO: Localiser
        this.Logger.LogCritical(ex, "Error while sending email.");
        return false;
      }
    }

    /// <summary>
    /// Asks for the dispose of the elements from this <see cref="EmailService"/> instance from memory.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose the elements from this <see cref="EmailService"/> instance from memory.
    /// </summary>
    /// <param name="disposing">Whether or not the instance of <see cref="EmailService"/> is disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (this.IsConnected)
      {
        this.smtpClient.Disconnect(true);
      }

      this.smtpClient.Dispose();
    }

    /// <summary>
    /// Connects to the SMPT Server.
    /// </summary>
    private void Connect()
    {
      if (!this.IsConnected)
      {
        this.smtpClient.Connect(this.appSettings.Email.Smtp.Host, this.appSettings.Email.Smtp.Port, true);
        this.smtpClient.Authenticate(this.appSettings.Email.Smtp.Username, this.appSettings.Email.Smtp.Password);
      }
    }
  }
}
