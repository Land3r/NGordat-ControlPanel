namespace NGordatControlPanel.Services.Core
{
  using System;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  /// <summary>
  /// <see cref="ALoggedService{TService}"/> abstract class.
  /// Class to implement a service using a logger.
  /// </summary>
  /// <typeparam name="TService">The underlying type of the service to log.</typeparam>
  public abstract class ALoggedService<TService> : ILoggedService<TService>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ALoggedService{TService}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ALoggedService([FromServices] ILogger<TService> logger)
    {
      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }
      else
      {
        this.Logger = logger;
      }
    }

    /// <summary>
    /// Gets the logger.
    /// </summary>
    public ILogger<TService> Logger { get; private set; }
  }
}
