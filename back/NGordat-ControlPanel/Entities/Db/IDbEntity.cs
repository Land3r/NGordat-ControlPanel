namespace NGordatControlPanel.Entities.Db
{
  using System;

  using MongoDB.Bson.Serialization.Attributes;

  /// <summary>
  /// <see cref="IDbEntity"/> interface.
  /// Interface representing an Entity stored in database. Provides access to the Id property of the Entity.
  /// </summary>
  public interface IDbEntity
  {
    /// <summary>
    /// Gets or sets the id of the <see cref="IDbEntity"/>.
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }
  }
}
