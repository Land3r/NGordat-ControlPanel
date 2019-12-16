namespace NGordatControlPanel.Security
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

  /// <summary>
  /// <see cref="CorsConfiguration"/> class.
  /// Collection of methods for enabling and configuring CORS policies.
  /// </summary>
  public static class CorsConfiguration
  {
    /// <summary>
    /// Allow all CORS policy.
    /// </summary>
    public static readonly string CorsPolicyAllowAll = "CorsAllowAll";

    /// <summary>
    /// Allow frontend CORS policy.
    /// </summary>
    public static readonly string CorsPolicyFrontend = "CorsFrontend";

    /// <summary>
    /// Sample other CORS policy.
    /// </summary>
    public static readonly string CorsPolicyOther = "CorsOther";

    /// <summary>
    /// Gets the list of enabled CORS policies for production mode.
    /// </summary>
    private static readonly string[] ProductionCorsPolicies = { CorsPolicyFrontend, CorsPolicyOther };

    /// <summary>
    /// Gets the list of enabled CORS policies for development mode.
    /// </summary>
    private static readonly string[] DevelopmentCorsPolicies = { CorsPolicyAllowAll };

    /// <summary>
    /// Configures the CORS policies on the services collection.
    /// </summary>
    /// <param name="services">The collection of services used by the application.</param>
    internal static void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyAllowAll,
          builder =>
          {
            builder.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyFrontend,
          builder =>
          {
            builder.WithOrigins("http://*.ngordat.net", "https://*.ngordat.net")
              .AllowAnyHeader()
              .WithMethods(HttpMethods.Get, HttpMethods.Post, HttpMethods.Put, HttpMethods.Patch, HttpMethods.Delete, HttpMethods.Options)
              .AllowCredentials()
              .SetIsOriginAllowedToAllowWildcardSubdomains();
          });
      });

      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyOther,
          builder =>
          {
            builder.WithOrigins("www.other-url.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
          });
      });
    }

    /// <summary>
    /// Configures the access policies for CORS on the application, depending on the execution environment.
    /// </summary>
    /// <param name="app">The application builer.</param>
    /// <param name="env">The environment builder.</param>
    internal static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        foreach (string corsPolicy in DevelopmentCorsPolicies)
        {
          app.UseCors(corsPolicy);
        }
      }
      else
      {
        foreach (string corsPolicy in ProductionCorsPolicies)
        {
          app.UseCors(corsPolicy);
        }
      }
    }
  }
}
