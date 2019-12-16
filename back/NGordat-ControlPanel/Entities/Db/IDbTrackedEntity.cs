namespace NGordatControlPanel.Entities.Db
{
  using System;

  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Users;

  /// <summary>
  /// <see cref="IDbTrackedEntity"/> interface.
  /// Interface representing a tracked Entity stored in database. Provides access to the Created and Updated properties of the Entity.
  /// </summary>
  public interface IDbTrackedEntity : IDbEntity
  {
    /// <summary>
    /// Gets or sets the creation date of the <see cref="IDbTrackedEntity"/>.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="UserReference"/> of the <see cref="User"/> that created the <see cref="IDbTrackedEntity"/>.
    /// </summary>
    public UserReference CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the updated date of the <see cref="IDbTrackedEntity"/>.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Updated { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="UserReference"/> of the <see cref="User"/> that updated the <see cref="IDbTrackedEntity"/>.
    /// </summary>
    public UserReference UpdatedBy { get; set; }
  }
}
