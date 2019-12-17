namespace NGordatControlPanel.Services.Users
{
  using NGordatControlPanel.Entities.Users;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IUserPasswordResetTokenService"/> class.
  /// Class service CRUD for <see cref="UserPasswordResetToken"/>.
  /// </summary>
  public interface IUserPasswordResetTokenService : ICrudService<UserPasswordResetToken>
  {
    /// <summary>
    /// Gets a <see cref="UserPasswordResetToken"/>, based on it's token value.
    /// </summary>
    /// <remarks>Note that the token is hashed before being stored in database.</remarks>
    /// <param name="token">The token value of the <see cref="UserPasswordResetToken"/>.</param>
    /// <returns>The <see cref="UserPasswordResetToken"/>, if found.</returns>
    UserPasswordResetToken GetByToken(string token);

    /// <summary>
    /// Gets whether or not the <see cref="UserPasswordResetToken"/> is valid.
    /// </summary>
    /// <param name="userPasswordResetToken">The <see cref="UserPasswordResetToken"/> to validate.</param>
    /// <returns>Whether or not the <see cref="UserPasswordResetToken"/> is valid.</returns>
    bool IsValid(UserPasswordResetToken userPasswordResetToken);
  }
}