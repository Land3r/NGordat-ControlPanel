namespace NGordatControlPanel.Entities.Emails
{
  using MongoDB.Bson;
  using MongoDB.Bson.Serialization.Attributes;

  using NGordatControlPanel.Entities.Db;

  /// <summary>
  /// <see cref="EmailTemplate"/> class.
  /// Class representing an email template. It is used to generate dynamically emails (subject and content).
  /// </summary>
  public class EmailTemplate : ADbTrackedEntity
  {
    /// <summary>
    /// Gets or sets the name of the <see cref="EmailTemplate"/>.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the subject of the <see cref="EmailTemplate"/>.
    /// Note that token represented by {tokenName} will be replaced by the actual value during email compilation process.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the body of the <see cref="EmailTemplate"/>.
    /// Note that token represented by {tokenName} will be replaced by the actual value during email compilation process.
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string Body { get; set; }
  }
}
