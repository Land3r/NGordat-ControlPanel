namespace NGordatControlPanel.Controllers
{
  using System;
  using System.Globalization;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Entities.Db;
  using NGordatControlPanel.Entities.Emails;
  using NGordatControlPanel.Entities.Users;
  using NGordatControlPanel.Helpers;
  using NGordatControlPanel.Models.Users;
  using NGordatControlPanel.Services.Emails;
  using NGordatControlPanel.Services.Users;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// Classe de controlleur UsersController.
  /// Controlleur pour les <see cref="User">Utilisateurs</see>.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    private readonly ILogger<UsersController> logger;

    /// <summary>
    /// Les ressources de langue.
    /// </summary>
    private readonly IStringLocalizer<UsersController> localizer;

    /// <summary>
    /// Le service des utilisateurs.
    /// </summary>
    private readonly IUserService userService;

    /// <summary>
    /// Le service de réinitialisation des mots de passes utilisateurs.
    /// </summary>
    private readonly IUserPasswordResetTokenService userPasswordResetTokenService;

    /// <summary>
    /// Le service email.
    /// </summary>
    private readonly IEmailService emailService;

    /// <summary>
    /// Instancie une nouvelle instance de <see cref="UsersController"/>.
    /// </summary>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="userService">Le <see cref="IUserService"/>.</param>
    /// <param name="userPasswordResetTokenService">Le <see cref="IUserPasswordResetTokenService"/>.</param>
    /// <param name="emailService">Le <see cref="IEmailService"/>.</param>
    /// <param name="logger">Le logger utilisé.</param>
    /// <param name="localizer">Les ressources localisées.</param>
    public UsersController(
      IOptions<AppSettings> appSettings,
      ILogger<UsersController> logger,
      IStringLocalizer<UsersController> localizer,
      IUserService userService,
      IUserPasswordResetTokenService userPasswordResetTokenService,
      IEmailService emailService)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings?.Value;
      }

      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }
      else
      {
        this.logger = logger;
      }

      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }
      else
      {
        this.localizer = localizer;
      }

      if (userService == null)
      {
        throw new ArgumentNullException(nameof(userService));
      }
      else
      {
        this.userService = userService;
      }

      if (userPasswordResetTokenService == null)
      {
        throw new ArgumentNullException(nameof(userPasswordResetTokenService));
      }
      else
      {
        this.userPasswordResetTokenService = userPasswordResetTokenService;
      }

      if (emailService == null)
      {
        throw new ArgumentNullException(nameof(emailService));
      }
      else
      {
        this.emailService = emailService;
      }
    }

    /// <summary>
    /// Authentifie un <see cref="User">Utilisateur</see>.
    /// </summary>
    /// <param name="model">Le <see cref="UserAuthenticateModel"/> utilisé pour authentifier <see cref="User">Utilisateur</see>.</param>
    /// <returns>L'<see cref="User">Utilisateur</see> si l'authentification est ok.</returns>
    [AllowAnonymous]
    [HttpPost("auth")]
    public IActionResult Authenticate([FromBody]UserAuthenticateModel model)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogLoginTry"].Value, model?.Username));
      var user = this.userService.Authenticate(model);

      if (user == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogLoginFailed"].Value, model?.Username));
        return this.BadRequest(new { message = this.localizer["LoginFailed"].Value });
      }
      else
      {
        this.logger.LogInformation(string.Format(CultureInfo.InvariantCulture, this.localizer["LogLoginSuccess"].Value, model?.Username));
        return this.Ok(user);
      }
    }

    /// <summary>
    /// Obtient l'utilisateur en cours, au travers du token JWT fourni.
    /// </summary>
    /// <returns>L'<see cref="User">Utilisateur</see> si l'authentification est ok.</returns>
    [HttpGet]
    public IActionResult GetCurrentUser()
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetCurrentUserTry"].Value));
      var user = this.userService.GetCurrentUser();

      if (user == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetCurrentUserFailed"].Value));
        return this.BadRequest(new { message = this.localizer["LoginFailed"].Value });
      }
      else
      {
        this.logger.LogInformation(string.Format(CultureInfo.InvariantCulture, this.localizer["LogGetCurrentUserSuccess"].Value, user.Username));
        return this.Ok(user);
      }
    }

    /// <summary>
    /// Enregistre un nouvel utilisateur.
    /// </summary>
    /// <param name="model">Les données de l'utilisateur à créer.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]User model)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogRegisterTry"].Value));

      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      User user;
      try
      {
        user = this.userService.Register(model);
      }
      catch (Exception ex)
      {
        if (ex.GetType() == typeof(ArgumentException))
        {
          this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, ex.Message));
          return this.Conflict(new { message = ex.Message });
        }

        throw ex;
      }

      if (user == null)
      {
        this.logger.LogWarning(string.Format(CultureInfo.InvariantCulture, this.localizer["LogRegisterFailed"].Value, model?.Username));
        return this.BadRequest(new { message = this.localizer["RegisterFailed"].Value });
      }
      else
      {
        this.logger.LogInformation(string.Format(CultureInfo.InvariantCulture, this.localizer["LogRegisterSuccess"].Value, model?.Username));

        // Envoi de l'email d'activation.
        this.emailService.SendTemplate(new EmailAddress() { Address = model.Email, Name = model.Username }, "Register", new
        {
          username = user.Username,
          activateaccountlink = $"{new Uri(this.appSettings.Environment.FrontUrl, $"#/user/activate/{user.ActivationToken}")}",
          sitename = this.appSettings.Environment.Name,
          siteurl = this.appSettings.Environment.FrontUrl,
          unsubscribeurl = new Uri(this.appSettings.Environment.FrontUrl, "/unsubscribe").ToString(),
        });
        return this.Ok(user);
      }
    }

    /// <summary>
    /// Active un utilisateur.
    /// </summary>
    /// <param name="token">Le token d'activation de l'utilisateur.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpGet("activate/{token}")]
    public IActionResult Activate(string token)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogActivateTry"].Value));

      if (string.IsNullOrEmpty(token))
      {
        throw new ArgumentNullException(nameof(token));
      }

      User user = this.userService.Activate(token);

      if (user == null)
      {
        this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogActivateNotFound"].Value, token));
        return this.NotFound(new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogActivateNotFound"].Value, token) });
      }

      return this.Ok(new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogActivateSuccess"].Value, token) });
    }

    /// <summary>
    /// Génére un token de réinitialisation de mot de passe utilisateur.
    /// </summary>
    /// <param name="model">Les données de l'utilisateur dont le mot de passe doit être réinitialiser.</param>
    /// <returns>Le résultat de l'opération.</returns>
    [AllowAnonymous]
    [HttpPost("forgotpassword")]
    public IActionResult ForgotPassword([FromBody]UserPasswordLostModel model)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPasswordLostTokenTry"].Value));

      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      User user = null;
      if (!string.IsNullOrEmpty(model.Email))
      {
        user = this.userService.GetByEmail(model.Email);
      }
      else if (!string.IsNullOrEmpty(model.Username))
      {
        user = this.userService.GetByUsername(model.Username);
      }

      if (user == null)
      {
        this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPasswordLostTokenUserNotFound"].Value, new { method = !string.IsNullOrEmpty(model.Email) ? "email" : "username", value = model.Email ?? model.Username }));
        return this.NotFound(new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogPasswordLostTokenUserNotFound"].Value) });
      }

      UserPasswordResetToken userPasswordResetToken;
      string token;
      try
      {
        token = CryptographicHelper.GetUrlSafeToken(24);
        userPasswordResetToken = new UserPasswordResetToken()
        {
          Token = token,
          ValidUntil = DateTime.UtcNow.AddMinutes(this.appSettings.Security.ResetPasswordTokenDurationInMinutes),
          Created = DateTime.UtcNow,
          CreatedBy = new UserReference() { Id = user.Id, Username = user.Username },
        };
        userPasswordResetToken = this.userPasswordResetTokenService.Create(userPasswordResetToken);

        // Envoie de l'email, avec le token en clair.
        this.emailService.SendTemplate(new EmailAddress() { Address = user.Email, Name = user.Username }, "PasswordLost", new
        {
          username = user.Username,
          resetpasswordlink = $"{new Uri(this.appSettings.Environment.FrontUrl, $"#/resetpassword/{token}")}",
          sitename = this.appSettings.Environment.Name,
          siteurl = this.appSettings.Environment.FrontUrl,
          unsubscribeurl = new Uri(this.appSettings.Environment.FrontUrl, "/unsubscribe").ToString(),
        });
      }
      catch (Exception ex)
      {
        // TODO: Gérer les exceptions, avec message localisé
        this.logger.LogError(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPasswordLostTokenFailed"].Value));
        throw ex;
      }

      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogPasswordLostTokenSuccess"].Value, new { value = model.Email ?? model.Username }));
      return this.Ok();
    }

    /// <summary>
    /// Réinitialise le mot de passe d'un utilisateur.
    /// </summary>
    /// <param name="model">Les données de réinitialisation.</param>
    /// <returns>Les informations de l'utilisateur, si le token est correct.</returns>
    [AllowAnonymous]
    [HttpPost("resetpassword")]
    public IActionResult ResetPassword(UserResetPasswordModel model)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordTry"].Value));

      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      // Token valide ?
      IActionResult existsResult = this.ResetPasswordExists(model.ResetPasswordToken);
      User user;

      if (existsResult is OkObjectResult)
      {
        UserPasswordLostResponseModel result = (existsResult as OkObjectResult).Value as UserPasswordLostResponseModel;
        if (result.Email == model.Email && result.Username == model.Username)
        {
          // Relié au bon utilisateur.
          user = this.userService.GetByEmail(result.Email);
          this.userService.UpdatePassword(user.Id, model.Password);

          return this.Ok();
        }
        else
        {
          return this.Conflict(new { message = this.localizer["LogResetPasswordUserConflict"].Value });
        }
      }
      else
      {
        return existsResult;
      }
    }

    /// <summary>
    /// Vérifie si le token fourni existe.
    /// </summary>
    /// <param name="token">Le token à valider.</param>
    /// <returns>Les informations de l'utilisateur, si le token est correct.</returns>
    [AllowAnonymous]
    [HttpGet("resetpassword/{token}")]
    public IActionResult ResetPasswordExists(string token)
    {
      this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsTry"].Value));

      if (string.IsNullOrEmpty(token))
      {
        throw new ArgumentNullException(nameof(token));
      }

      UserPasswordResetToken userPasswordResetToken = this.userPasswordResetTokenService.GetByToken(token);
      User user;

      if (userPasswordResetToken == null)
      {
        this.logger.LogDebug(string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsNotFound"].Value, token));
        return this.NotFound(new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsNotFound"].Value, token) });
      }

      if (this.userPasswordResetTokenService.IsValid(userPasswordResetToken))
      {
        user = this.userService.Get(userPasswordResetToken.CreatedBy.Id);
        if (user != null)
        {
          return this.Ok(new UserPasswordLostResponseModel() { Username = user.Username, Email = user.Email });
        }
        else
        {
          this.logger.LogError(string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsUserNotFound"].Value));
          return this.NotFound(new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsUserNotFound"].Value) });
        }
      }
      else
      {
        return this.StatusCode(498, new { message = string.Format(CultureInfo.InvariantCulture, this.localizer["LogResetPasswordExistsNotValid"].Value) });
      }
    }
  }
}
