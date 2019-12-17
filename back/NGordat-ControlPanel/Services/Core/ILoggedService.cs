namespace NGordatControlPanel.Services.Core
{
  using Microsoft.Extensions.Logging;

  /// <summary>
  /// <see cref="ILoggedService{TService}"/> interface.
  /// Interface used to access a service with a logger.
  /// </summary>
  /// <typeparam name="TService">The underlying type of the service to log.</typeparam>
  public interface ILoggedService<TService>
  {
    /// <summary>
    /// Gets the logger.
    /// </summary>
    ILogger<TService> Logger { get; }
  }
}