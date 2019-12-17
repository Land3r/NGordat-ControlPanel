namespace NGordatControlPanel.Services.Users
{
  using System;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Users;
  using NGordatControlPanel.Models.Users;
  using NGordatControlPanel.Services.Core;

  /// <summary>
  /// <see cref="IUserService"/> interface.
  /// Interface service for handling <see cref="User"/>.
  /// </summary>
  public interface IUserService : ICrudService<User>
  {
    /// <summary>
    /// Authenticates a <see cref="User"/>, based on the provided <see cref="UserAuthenticateModel"/>.
    /// </summary>
    /// <param name="model">The <see cref="UserAuthenticateModel"/> to use to authenticate the <see cref="User"/>.</param>
    /// <returns>The <see cref="User" /> in an authenticated state (with token values).</returns>
    User Authenticate(UserAuthenticateModel model);

    /// <summary>
    /// Gets the current <see cref="User"/>.
    /// </summary>
    /// <returns>The current <see cref="User"/>.</returns>
    User GetCurrentUser();

    /// <summary>
    /// Gets a <see cref="User"/>, based on it's username.
    /// </summary>
    /// <param name="username">The username of the <see cref="User"/> to find.</param>
    /// <returns>The <see cref="User"/>, if found.</returns>
    User GetByUsername(string username);

    /// <summary>
    /// Gets a <see cref="User"/>, based on it's email.
    /// </summary>
    /// <param name="email">The email value of the <see cref="User"/> to find.</param>
    /// <returns>The <see cref="User"/>, if found.</returns>
    User GetByEmail(string email);

    /// <summary>
    /// Registers a new <see cref="User"/>.
    /// </summary>
    /// <param name="model">The <see cref="User"/> to create.</param>
    /// <returns>The created <see cref="User"/>.</returns>
    User Register(User model);

    /// <summary>
    /// Activates a <see cref="User"/>.
    /// </summary>
    /// <param name="token">The activation token of the <see cref="User"/>.</param>
    /// <returns>The activated <see cref="User"/>.</returns>
    User Activate(string token);

    /// <summary>
    /// Updates the password of a <see cref="User"/>.
    /// </summary>
    /// <remarks>Note that the password is hashed before being stored in database.</remarks>
    /// <param name="id">The is of the <see cref="User"/> to update.</param>
    /// <param name="clearPassword">The password in clear text to set to the <see cref="User"/>.</param>
    /// <returns>The operation result.</returns>
    ReplaceOneResult UpdatePassword(Guid id, string clearPassword);
  }
}
