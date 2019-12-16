namespace NGordatControlPanel.Models.Users
{
  using NGordatControlPanel.Controllers;

  /// <summary>
  /// <see cref="UserResetPasswordModel"/> class.
  /// Class representing the model to communicate with the resetpassword endpoint of the <see cref="UsersController"/>.
  /// </summary>
  public class UserResetPasswordModel
  {
    /// <summary>
    /// Gets or sets the reset password token of the <see cref="UserResetPasswordModel"/>.
    /// </summary>
    public string ResetPasswordToken { get; set; }

    /// <summary>
    /// Gets or sets the email of the <see cref="UserResetPasswordModel"/>.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the username of the <see cref="UserResetPasswordModel"/>.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the <see cref="UserResetPasswordModel"/>.
    /// </summary>
    public string Password { get; set; }
  }
}
