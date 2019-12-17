namespace NGordatControlPanel.Services.Users
{
  using System;
  using System.Linq;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Users;
  using NGordatControlPanel.Helpers;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="UserPasswordResetTokenService"/> class.
  /// Class service CRUD for <see cref="UserPasswordResetToken"/>.
  /// </summary>
  public class UserPasswordResetTokenService : AMongoEntityLocalizedService<UserPasswordResetToken, UserPasswordResetTokenService>, IUserPasswordResetTokenService
  {
    /// <summary>
    /// The name of the mongodb collection.
    /// </summary>
    private const string CollectionName = "UserPasswordResetToken";

    /// <summary>
    /// Initializes a new instance of the <see cref="UserPasswordResetTokenService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public UserPasswordResetTokenService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<UserPasswordResetTokenService> localizer,
      [FromServices] ILogger<UserPasswordResetTokenService> logger)
      : base(appSettings, CollectionName, logger, localizer)
    {
    }

    /// <summary>
    /// Gets a <see cref="UserPasswordResetToken"/>, based on it's token value.
    /// </summary>
    /// <remarks>Note that the token is hashed before being stored in database.</remarks>
    /// <param name="token">The token value of the <see cref="UserPasswordResetToken"/>.</param>
    /// <returns>The <see cref="UserPasswordResetToken"/>, if found.</returns>
    public UserPasswordResetToken GetByToken(string token)
    {
      if (string.IsNullOrEmpty(token))
      {
        throw new ArgumentNullException(nameof(token));
      }

      // We are using a hash function for storing reset password token for security reasons.
      // Basically it makes it more difficult for an attacker with a read access to the database to gain access to the user account.
      // See https://security.stackexchange.com/questions/86913/should-password-reset-tokens-be-hashed-when-stored-in-a-database for more info.
      string hash = CryptographicHelper.GetHash(token);

      return this.Entities.Find(elm => elm.Token == hash).FirstOrDefault();
    }

    /// <summary>
    /// Creates a <see cref="UserPasswordResetToken"/>.
    /// </summary>
    /// <remarks>Note that the token is hashed before being stored in database.</remarks>
    /// <param name="elm">The <see cref="UserPasswordResetToken"/> to create.</param>
    /// <returns>The created <see cref="UserPasswordResetToken"/>.</returns>
    public override UserPasswordResetToken Create(UserPasswordResetToken elm)
    {
      // We are using a hash function for storing reset password token for security reasons.
      // Basically it makes it more difficult for an attacker with a read access to the database to gain access to the user account.
      // See https://security.stackexchange.com/questions/86913/should-password-reset-tokens-be-hashed-when-stored-in-a-database for more info.
      string hash = CryptographicHelper.GetHash(elm?.Token);
      elm.Token = hash;

      return base.Create(elm);
    }

    /// <summary>
    /// Gets whether or not the <see cref="UserPasswordResetToken"/> is valid.
    /// </summary>
    /// <param name="userPasswordResetToken">The <see cref="UserPasswordResetToken"/> to validate.</param>
    /// <returns>Whether or not the <see cref="UserPasswordResetToken"/> is valid.</returns>
    public bool IsValid(UserPasswordResetToken userPasswordResetToken)
    {
      if (userPasswordResetToken == null)
      {
        throw new ArgumentNullException(nameof(userPasswordResetToken));
      }

      if (userPasswordResetToken.ValidUntil > DateTime.UtcNow)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
  }
}
