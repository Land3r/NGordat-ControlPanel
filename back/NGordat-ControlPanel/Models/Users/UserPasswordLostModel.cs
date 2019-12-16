namespace NGordatControlPanel.Models.Users
{
  using NGordatControlPanel.Controllers;

  /// <summary>
  /// <see cref="UserAuthenticateModel"/> class.
  /// Class representing the model to communicate with the passwordlost endpoint of the <see cref="UsersController"/>.
  /// </summary>
  public class UserPasswordLostModel
  {
    /// <summary>
    /// Gets or sets the username of the <see cref="UserPasswordLostModel" /> that has forgotten his password.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email of the <see cref="UserPasswordLostModel" /> that has forgotten his password.
    /// </summary>
    public string Email { get; set; }
  }
}
