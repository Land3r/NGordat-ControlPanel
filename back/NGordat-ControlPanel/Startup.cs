namespace NGordatControlPanel
{
  using System.Text;

  using Microsoft.AspNetCore.Authentication.JwtBearer;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.AspNetCore.HttpOverrides;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.IdentityModel.Tokens;

  using NGordatControlPanel.Security;
  using NGordatControlPanel.Services.Emails;
  using NGordatControlPanel.Services.Google;
  using NGordatControlPanel.Services.Groceries;
  using NGordatControlPanel.Services.Users;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="Startup"/> class.
  /// Class used to configure the asp.net core pipeline.
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    /// <summary>
    /// Gets the application configuration.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Configures the HTTP pipeline.
    /// </summary>
    /// <remarks>This method is called by asp.net core runtime.</remarks>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The environment builder.</param>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      CorsConfiguration.Configure(app, env);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");

        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      // Necessary for production build.
      // Allows the application to be delivered through a reverse proxy setup (with nginx and kestrel typically).
      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    /// <summary>
    /// Configures the services added to the container.
    /// </summary>
    /// <remarks>This method is called by asp.net core runtime.</remarks>
    /// <param name="services">The service collection.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      CorsConfiguration.ConfigureServices(services);

      services.AddLocalization(options => options.ResourcesPath = "Resources");
      services.AddMvc()
        .AddViewLocalization(options => options.ResourcesPath = "Resources")
        .AddDataAnnotationsLocalization();

      services.AddControllers();

      // Configure strongly typed settings objects
      var appSettingsSection = this.Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(appSettingsSection);

      // Configure jwt authentication
      var appSettings = appSettingsSection.Get<AppSettings>();
      var key = Encoding.ASCII.GetBytes(appSettings.Security.JWT.Secret);
      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x =>
      {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
        };
      });

      // Configure DI for application services
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IUserPasswordResetTokenService, UserPasswordResetTokenService>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<IEmailTemplateService, EmailTemplateService>();
      services.AddScoped<ISpeechToTextService, SpeechToTextService>();
      services.AddScoped<IGroceryActionService, GroceryActionService>();
      services.AddScoped<IGroceryItemService, GroceryItemService>();
      services.AddScoped<IGroceryQuantityService, GroceryQuantityService>();
      services.AddScoped<IGroceryMeaninglessWordService, GroceryMeaninglessWordService>();
    }
  }
}
