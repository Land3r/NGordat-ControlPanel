namespace NGordatControlPanel.Services.Core
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.Json;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using MongoDB.Bson;
  using MongoDB.Driver;

  using NGordatControlPanel.Entities.Db;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="AMongoEntityService{TEntity, TService}"/> abstract class.
  /// Class to implement a service using a logger and a mongodb collection with CRUD operations.
  /// </summary>
  /// <typeparam name="TEntity">The underlying type of the mongodb entity.</typeparam>
  /// <typeparam name="TService">The underlying type of the service to log and localize.</typeparam>
  public abstract class AMongoEntityService<TEntity, TService> : ALoggedService<TService>, ICrudService<TEntity>
    where TEntity : IDbEntity
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AMongoEntityService{TEntity, TService}"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="collectionName">The name of the mongodb collection.</param>
    /// <param name="logger">The logger.</param>
    public AMongoEntityService(
      IOptions<AppSettings> appSettings,
      string collectionName,
      [FromServices] ILogger<TService> logger)
      : base(logger)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else if (string.IsNullOrEmpty(collectionName))
      {
        throw new ArgumentNullException(nameof(collectionName));
      }

      var client = new MongoClient(appSettings?.Value.MongoDb.ConnectionString);
      var db = client.GetDatabase(appSettings?.Value.MongoDb.DatabaseName);

      this.Entities = db.GetCollection<TEntity>(collectionName);
    }

    /// <summary>
    /// Gets the collection of <see cref="TEntity"/>.
    /// </summary>
    public IMongoCollection<TEntity> Entities { get; private set; }

    /// <summary>
    /// Gets all the <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <returns>The list of all <see cref="TEntity"/>.</returns>
    public virtual IEnumerable<TEntity> Get()
    {
      return this.Entities.Find(elm => true).ToEnumerable();
    }

    /// <summary>
    /// Gets an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to find.</param>
    /// <returns>The <see cref="TEntity"/> with the provided id, if found.</returns>
    public virtual TEntity Get(Guid id)
    {
      return this.Entities.Find<TEntity>(elm => elm.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Creates a new <see cref="TEntity"/> in the collection.
    /// </summary>
    /// <param name="elm">The <see cref="TEntity"/> to create.</param>
    /// <returns>The created <see cref="TEntity"/>.</returns>
    public virtual TEntity Create(TEntity elm)
    {
      if (elm is IDbTrackedEntity)
      {
        IDbTrackedEntity elmTracked = elm as IDbTrackedEntity;
        if (elmTracked.Created == null)
        {
          (elm as IDbTrackedEntity).Created = DateTime.UtcNow;
        }

        if (elmTracked.Updated == DateTime.MinValue)
        {
          (elm as IDbTrackedEntity).Updated = (elm as IDbTrackedEntity).Created;
        }

        if (elmTracked.CreatedBy == null)
        {
          (elm as IDbTrackedEntity).CreatedBy = new UserReference()
          {
            Id = Guid.NewGuid(),
            Username = "System",
          };
        }

        if (elmTracked.UpdatedBy == null)
        {
          (elm as IDbTrackedEntity).UpdatedBy = (elm as IDbTrackedEntity).CreatedBy;
        }
      }

      // Id field is automatically populated.
      this.Entities.InsertOne(elm);

      return elm;
    }

    /// <summary>
    /// Updates an <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <param name="elmIn">The <see cref="TEntity"/> to update.</param>
    /// <returns>The operation result.</returns>
    public virtual ReplaceOneResult Update(TEntity elmIn)
    {
      return this.Update(elmIn.Id, elmIn);
    }

    /// <summary>
    /// Updates an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to update.</param>
    /// <param name="elmIn">The <see cref="TEntity"/> to update.</param>
    /// <returns>The operation result.</returns>
    public virtual ReplaceOneResult Update(Guid id, TEntity elmIn)
    {
      if (elmIn is IDbTrackedEntity)
      {
        IDbTrackedEntity elmTracked = elmIn as IDbTrackedEntity;
        (elmIn as IDbTrackedEntity).Updated = DateTime.UtcNow;
        (elmIn as IDbTrackedEntity).UpdatedBy = new UserReference()
        {
          Id = Guid.NewGuid(),
          Username = "System", // TODO.
        };
      }

      return this.Entities.ReplaceOne(book => book.Id == id, elmIn);
    }

    /// <summary>
    /// Updates partially (only the provided properties) an <see cref="TEntity"/>.
    /// </summary>
    /// <param name="elmIn">The new data of the <see cref="TEntity"/>.</param>
    /// <returns>The operation result.</returns>
    public UpdateResult UpdatePartially(TEntity elmIn)
    {
      return this.UpdatePartially(elmIn.Id, elmIn);
    }

    /// <summary>
    /// Updates partially (only the provided properties) an <see cref="TEntity"/>.
    /// </summary>
    /// <param name="id">The if of the <see cref="TEntity"/>.</param>
    /// <param name="elmIn">The new data of the <see cref="TEntity"/>.</param>
    /// <returns>The operation result.</returns>
    public virtual UpdateResult UpdatePartially(Guid id, TEntity elmIn)
    {
      string jsonValue = JsonSerializer.Serialize(elmIn);
      var changesDocument = BsonDocument.Parse(jsonValue);

      var filter = Builders<TEntity>.Filter.Eq("_id", id);

      UpdateDefinition<TEntity> update = null;
      foreach (var change in changesDocument)
      {
        if (update == null)
        {
          var builder = Builders<TEntity>.Update;
          update = builder.Set(change.Name, change.Value);
        }
        else
        {
          update = update.Set(change.Name, change.Value);
        }
      }

      /* Following 3 lines are for debugging purposes only
       * var registry = BsonSerializer.SerializerRegistry;
       * var serializer = registry.GetSerializer<BsonDocument>();
       * var rendered = update.Render(serializer, registry).ToJson();
      */

      return this.Entities.UpdateOne(filter, update);
    }

    /// <summary>
    /// Deletes an <see cref="TEntity"/> from the collection.
    /// </summary>
    /// <param name="elmIn">The <see cref="TEntity"/> to delete.</param>
    /// <returns>The operation result.</returns>
    public virtual DeleteResult Remove(TEntity elmIn)
    {
      return this.Entities.DeleteOne(book => book.Id == elmIn.Id);
    }

    /// <summary>
    /// Deletes an <see cref="TEntity"/> from the collection, based on it's id.
    /// </summary>
    /// <param name="id">The id of the <see cref="TEntity"/> to delete.</param>
    /// <returns>The operation result.</returns>
    public virtual DeleteResult Remove(Guid id)
    {
      return this.Entities.DeleteOne(book => book.Id == id);
    }
  }
}
