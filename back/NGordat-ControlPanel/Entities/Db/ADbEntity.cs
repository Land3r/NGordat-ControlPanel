namespace NGordatControlPanel.Entities.Db
{
  using System;

  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// <see cref="ADbEntity"/> abstract class.
  /// Class representing an Entity stored in database. Provides access to the Id property of the Entity.
  /// </summary>
  public abstract class ADbEntity : IDbEntity
  {
    /// <summary>
    /// Gets or sets the if of the <see cref="ADbEntity"/>.
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }
  }
}
