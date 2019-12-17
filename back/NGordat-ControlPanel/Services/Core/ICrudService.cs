namespace NGordatControlPanel.Services.Core
{
  using System;
  using System.Collections.Generic;

  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="ICrudService{TEntity}"/> interface.
  /// Interface used to access a service a mongodb collection with CRUD operations.
  /// </summary>
  /// <typeparam name="TEntity">The underlying type of the mongodb entity.</typeparam>
  public interface ICrudService<TEntity>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Gets the collection of <see cref="TEntity"/>.
    /// </summary>
    IMongoCollection<TEntity> Entities { get; }

    /// <summary>
    /// Gets all the <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <returns>The list of all <see cref="TEntity"/>.</returns>
    public IEnumerable<TEntity> Get();

    /// <summary>
    /// Gets an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to find.</param>
    /// <returns>The <see cref="TEntity"/> with the provided id, if found.</returns>
    public TEntity Get(Guid id);

    /// <summary>
    /// Creates a new <see cref="TEntity"/> in the collection.
    /// </summary>
    /// <param name="elm">The <see cref="TEntity"/> to create.</param>
    /// <returns>The created <see cref="TEntity"/>.</returns>
    public TEntity Create(TEntity elm);

    /// <summary>
    /// Updates an <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <param name="elmIn">The <see cref="TEntity"/> to update.</param>
    /// <returns>The operation result.</returns>
    public ReplaceOneResult Update(TEntity elmIn);

    /// <summary>
    /// Updates an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to update.</param>
    /// <param name="elmIn">The <see cref="TEntity"/> to update.</param>
    /// <returns>The operation result.</returns>
    public ReplaceOneResult Update(Guid id, TEntity elmIn);

    /// <summary>
    /// Updates partially (only the provided properties) an <see cref="TEntity"/>.
    /// </summary>
    /// <param name="elmIn">The new data of the <see cref="TEntity"/>.</param>
    /// <returns>The operation result.</returns>
    public UpdateResult UpdatePartially(TEntity elmIn);

    /// <summary>
    /// Updates partially (only the provided properties) an <see cref="TEntity"/>.
    /// </summary>
    /// <param name="id">The if of the <see cref="TEntity"/>.</param>
    /// <param name="elmIn">The new data of the <see cref="TEntity"/>.</param>
    /// <returns>The operation result.</returns>
    public UpdateResult UpdatePartially(Guid id, TEntity elmIn);

    /// <summary>
    /// Deletes an <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <param name="elmIn">The <see cref="TEntity"/> to delete.</param>
    /// <returns>The operation result.</returns>
    public DeleteResult Remove(TEntity elmIn);

    /// <summary>
    /// Deletes an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
    /// <returns>The operation result.</returns>
    public DeleteResult Remove(Guid id);
  }
}
