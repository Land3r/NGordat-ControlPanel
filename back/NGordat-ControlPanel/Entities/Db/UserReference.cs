namespace NGordatControlPanel.Entities.Db
{
  using System;

  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Users;

  /// <summary>
  /// <see cref="UserReference"/> class.
  /// Class representing a <see cref="User"/>. Used to store a reference to the user in other entities.
  /// </summary>
  public class UserReference
  {
    /// <summary>
    /// Gets or sets the id of the <see cref="User"/> represented by this <see cref="UserReference"/>.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the <see cref="User"/> represented by this <see cref="UserReference"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }
  }
}
