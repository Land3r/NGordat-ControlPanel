namespace NGordatControlPanel.Services.Users
{
  using System;
  using System.Globalization;
  using System.IdentityModel.Tokens.Jwt;
  using System.Linq;
  using System.Security.Claims;
  using System.Text;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using Microsoft.IdentityModel.Tokens;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Users;
  using NGordatControlPanel.Extensions;
  using NGordatControlPanel.Helpers;
  using NGordatControlPanel.Models.Users;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="UserService"/> class.
  /// Class service for handling <see cref="User"/>.
  /// </summary>
  public class UserService : AMongoEntityLocalizedService<User, UserService>, IUserService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "Users";

    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The <see cref="IHttpContextAccessor"/>.
    /// </summary>
    private readonly IHttpContextAccessor httpContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="httpContext">Le contexte http.</param>
    public UserService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<UserService> localizer,
      [FromServices] ILogger<UserService> logger,
      [FromServices]IHttpContextAccessor httpContext)
      : base(appSettings, CollectionName, logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings.Value;
      }

      if (httpContext == null)
      {
        throw new ArgumentNullException(nameof(httpContext));
      }
      else
      {
        this.httpContext = httpContext;
      }
    }

    /// <summary>
    /// Authenticates a <see cref="User"/>, based on the provided <see cref="UserAuthenticateModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="UserAuthenticateModel"/> to use to authenticate the <see cref="User"/>.</param>
    /// <returns>The <see cref="User" /> in an authenticated state (with token values).</returns>
    public User Authenticate(UserAuthenticateModel model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      string hashedPassword = CryptographicHelper.GetHash(model.Password, this.appSettings.Security.HashSalt);
      User user = this.Entities.Find(x => x.Username == model.Username && x.Password == hashedPassword).FirstOrDefault();

      if (user == null)
      {
        return null;
      }
      else
      {
        // Is user active ?
        if (user.Active == false)
        {
          return null;
        }

        // JWT token generation.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this.appSettings.Security.JWT.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
          Subject = new ClaimsIdentity(new Claim[]
          {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email.ToString(CultureInfo.InvariantCulture)),
          }),
          Expires = DateTime.UtcNow.AddDays(this.appSettings.Security.JWT.DurationInDays),
          SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        user.Token = tokenHandler.WriteToken(token);

        return user.WithoutPassword();
      }
    }

    /// <summary>
    /// Gets the current <see cref="User"/>.
    /// </summary>
    /// <returns>The current <see cref="User"/>.</returns>
    public User GetCurrentUser()
    {
      if (this.httpContext.HttpContext.User != null)
      {
        Guid id = new Guid(this.httpContext.HttpContext.User.Identity.Name);
        return this.Get(id).WithoutPassword();
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Gets a <see cref="User"/>, based on it's username.
    /// </summary>
    /// <param name="username">The username of the <see cref="User"/> to find.</param>
    /// <returns>The <see cref="User"/>, if found.</returns>
    public User GetByUsername(string username)
    {
      if (string.IsNullOrEmpty(username))
      {
        throw new ArgumentNullException(nameof(username));
      }

      return this.Entities.Find(elm => elm.Username == username).FirstOrDefault()?.WithoutPassword();
    }

    /// <summary>
    /// Gets a <see cref="User"/>, based on it's email.
    /// </summary>
    /// <param name="email">The email value of the <see cref="User"/> to find.</param>
    /// <returns>The <see cref="User"/>, if found.</returns>
    public User GetByEmail(string email)
    {
      if (string.IsNullOrEmpty(email))
      {
        throw new ArgumentNullException(nameof(email));
      }

      return this.Entities.Find(elm => elm.Email == email).FirstOrDefault()?.WithoutPassword();
    }

    /// <summary>
    /// Registers a new <see cref="User"/>.
    /// </summary>
    /// <param name="model">The <see cref="User"/> to create.</param>
    /// <returns>The created <see cref="User"/>.</returns>
    public User Register(User model)
    {
      if (model == null)
      {
        throw new ArgumentNullException(nameof(model));
      }

      // The username of the user should be unique.
      if (this.GetByUsername(model.Username) != null)
      {
        throw new ArgumentException((this as ILocalizedService<UserService>).GetLocalized("RegisterErrorUserUsernameAlreadyExists", model.Username));
      }

      // The email of the user should be unique.
      else if (this.GetByEmail(model.Email) != null)
      {
        throw new ArgumentException((this as ILocalizedService<UserService>).GetLocalized("RegisterErrorUserEmailAlreadyExists", model.Email));
      }

      model.ActivationToken = CryptographicHelper.GetUrlSafeToken(24);
      model.Active = false;

      return this.Create(model)?.WithoutPassword();
    }

    /// <summary>
    /// Activates a <see cref="User"/>.
    /// </summary>
    /// <param name="token">The activation token of the <see cref="User"/>.</param>
    /// <returns>The activated <see cref="User"/>.</returns>
    public User Activate(string token)
    {
      User user = this.Entities.Find(elm => elm.ActivationToken == token).FirstOrDefault();

      if (user != null)
      {
        user.ActivationToken = null;
        user.Active = true;
        this.Update(user);

        return user;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Creates a <see cref="User"/>.
    /// </summary>
    /// <remarks>Note that the password is hashed before being stored in database.</remarks>
    /// <param name="elm">The <see cref="User"/> to create.</param>
    /// <returns>The created <see cref="User"/>.</returns>
    public override User Create(User elm)
    {
      if (elm == null)
      {
        throw new ArgumentNullException(nameof(elm));
      }

      string hashedPassword = CryptographicHelper.GetHash(elm.Password, this.appSettings.Security.HashSalt);
      elm.Password = hashedPassword;

      return base.Create(elm);
    }

    /// <summary>
    /// Updates the password of a <see cref="User"/>.
    /// </summary>
    /// <remarks>Note that the password is hashed before being stored in database.</remarks>
    /// <param name="id">The is of the <see cref="User"/> to update.</param>
    /// <param name="clearPassword">The password in clear text to set to the <see cref="User"/>.</param>
    /// <returns>The operation result.</returns>
    public ReplaceOneResult UpdatePassword(Guid id, string clearPassword)
    {
      User user = this.Get(id);

      string hashedPassword = CryptographicHelper.GetHash(clearPassword, this.appSettings.Security.HashSalt);
      user.Password = hashedPassword;

      return this.Update(user);
    }
  }
}
