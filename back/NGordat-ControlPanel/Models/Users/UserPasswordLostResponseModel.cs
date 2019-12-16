namespace NGordatControlPanel.Models.Users
{
  using NGordatControlPanel.Controllers;

  /// <summary>
  /// <see cref="UserPasswordLostResponseModel"/> class.
  /// Class representing the model of a response of the passwordlost endpoint of the <see cref="UsersController"/>.
  /// </summary>
  public class UserPasswordLostResponseModel
  {
    /// <summary>
    /// Gets or sets the username of the <see cref="UserPasswordLostResponseModel"/>.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email of the <see cref="UserPasswordLostResponseModel"/>.
    /// </summary>
    public string Email { get; set; }
  }
}
