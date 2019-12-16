namespace NGordatControlPanel
{
  using System;

  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;

  using NLog.Web;

  /// <summary>
  /// <see cref="Program"/> class.
  /// Application main entry point class.
  /// </summary>
  public static class Program
  {
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">The arguments passed to the application.</param>
    public static void Main(string[] args)
    {
      var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      try
      {
        logger.Debug("Application Initialisation.");
        CreateHostBuilder(args).Build().Run();
      }
      catch (Exception exception)
      {
        // NLog: catch setup errors
        logger.Error(exception, "Impossible to build the host.");
        throw;
      }
      finally
      {
        logger.Info("Application stopped.");
        NLog.LogManager.Shutdown();
      }
    }

    /// <summary>
    /// Creates the server host.
    /// </summary>
    /// <param name="args">The arguments passed to the application.</param>
    /// <returns>The host to build.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        }).ConfigureLogging(logging =>
        {
          logging.ClearProviders();
        })
      .UseNLog();
    }
  }
}
