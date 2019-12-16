namespace NGordatControlPanel.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NGordatControlPanel.Entities.Users;

  /// <summary>
  /// Collection of helpers for dealing with <see cref="User"/>.
  /// </summary>
  public static class UserExtension
  {
    /// <summary>
    /// Removes the password of a collection of <see cref="User"/>.
    /// </summary>
    /// <param name="users">The collection of <see cref="User"/>.</param>
    /// <returns>The collection of <see cref="User"/> without the password.</returns>
    public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
    {
      if (users == null)
      {
        throw new ArgumentNullException(nameof(users));
      }
      else
      {
        return users.Select(x => x.WithoutPassword());
      }
    }

    /// <summary>
    /// Removes the password of a <see cref="User"/>.
    /// </summary>
    /// <param name="user">The <see cref="User"/>.</param>
    /// <returns>The <see cref="User"/> without the password.</returns>
    public static User WithoutPassword(this User user)
    {
      if (user == null)
      {
        throw new ArgumentNullException(nameof(user));
      }
      else
      {
        user.Password = null;
        return user;
      }
    }
  }
}
