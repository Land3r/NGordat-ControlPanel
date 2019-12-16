namespace NGordatControlPanel.Models.Users
{
  using System.ComponentModel.DataAnnotations;

  using NGordatControlPanel.Controllers;

  /// <summary>
  /// <see cref="UserAuthenticateModel"/> class.
  /// Class representing the model to communicate with the auth endpoint of the <see cref="UsersController"/>.
  /// </summary>
  public class UserAuthenticateModel
  {
    /// <summary>
    /// Gets or sets the username of the <see cref="UserAuthenticateModel" /> to authenticate.
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the <see cref="UserAuthenticateModel" /> to authenticate.
    /// </summary>
    [Required]
    public string Password { get; set; }
  }
}
