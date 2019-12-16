namespace NGordatControlPanel.Entities.Db
{
  using System;

  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Users;

  /// <summary>
  /// <see cref="ADbTrackedEntity"/> abstract class.
  /// Class representing a tracked Entity stored in database. Provides access to the Created and Updated properties of the Entity.
  /// </summary>
  public abstract class ADbTrackedEntity : ADbEntity, IDbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the creation date of the <see cref="ADbTrackedEntity"/>.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="UserReference"/> of the <see cref="User"/> that created the <see cref="ADbTrackedEntity"/>.
    /// </summary>
    public UserReference CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the updated date of the <see cref="ADbTrackedEntity"/>.
    /// </summary>
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Updated { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="UserReference"/> of the <see cref="User"/> that updated the <see cref="ADbTrackedEntity"/>.
    /// </summary>
    public UserReference UpdatedBy { get; set; }
  }
}
