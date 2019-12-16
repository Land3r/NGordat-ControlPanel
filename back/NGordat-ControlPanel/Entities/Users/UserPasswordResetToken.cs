namespace NGordatControlPanel.Entities.Users
{
  using System;

  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="UserPasswordResetToken"/> class.
  /// Class representing a request from the <see cref="User"/> to reset it's password.
  /// </summary>
  public class UserPasswordResetToken : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the token value of the <see cref="UserPasswordResetToken"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets the date at which the token becomes invalid (cause he expires).
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime ValidUntil { get; set; }
  }
}
