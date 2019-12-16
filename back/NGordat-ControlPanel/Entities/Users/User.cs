namespace NGordatControlPanel.Entities.Users
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="User"/> class.
  /// Class representing a user.
  /// </summary>
  public class User : ADbEntity
  {
    /// <summary>
    /// Gets or sets the firstname of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the lastname of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the username of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the email of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets whether or not the <see cref="User"/> is active.
    /// </summary>
    [BsonRepresentation(BsonType.Boolean)]
    public bool? Active { get; set; }

    /// <summary>
    /// Gets or sets the activation token of the <see cref="User"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string ActivationToken { get; set; }

    /// <summary>
    /// Gets or sets the token of the <see cref="User"/>.
    /// Not an actual property of the <see cref="User"/>. It is injected into the entity for login.
    /// </summary>
    [BsonIgnore]
    public string Token { get; set; }
  }
}
