namespace NGordatControlPanel.Settings
{
  /// <summary>
  /// <see cref="MongoDbSettings"/> class.
  /// Class used to retrieve configuration for database interaction.
  /// </summary>
  public class MongoDbSettings
  {
    /// <summary>
    /// Gets or sets the database connection string of the <see cref="MongoDbSettings"/>.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the database name of the <see cref="MongoDbSettings"/>.
    /// </summary>
    public string DatabaseName { get; set; }
  }
}
