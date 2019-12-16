namespace NGordatControlPanel.Settings
{
  using NGordatControlPanel.Settings.Emails;
  using NGordatControlPanel.Settings.Google;

  /// <summary>
  /// <see cref="SecuritySettings"/> class.
  /// Class used to retrieve all the application related configuration.
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Gets or sets the <see cref="EnvironmentSettings"/> of the <see cref="AppSettings"/>.
    /// </summary>
    public EnvironmentSettings Environment { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="MongoDbSettings"/> of the <see cref="AppSettings"/>.
    /// </summary>
    public MongoDbSettings MongoDb { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="EmailSettings"/> of the <see cref="AppSettings"/>.
    /// </summary>
    public EmailSettings Email { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SecuritySettings"/> of the <see cref="AppSettings"/>.
    /// </summary>
    public SecuritySettings Security { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="GoogleSettings"/> of the <see cref="AppSettings"/>.
    /// </summary>
    public GoogleSettings Google { get; set; }
  }
}
