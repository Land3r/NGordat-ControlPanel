namespace NGordatControlPanel.Controllers
{
  using System;
  using System.IO;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Services.Google;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// <see cref="GroceryController"/> class.
  /// API controller for interacting with the groceries.
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryController : ControllerBase
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<GroceryController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<GroceryController> localizer;

    /// <summary>
    /// The <see cref="ISpeechToTextService"/>.
    /// </summary>
    private readonly ISpeechToTextService speechToTextService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryController"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="speechToTextService">The <see cref="ISpeechToTextService"/> to use.</param>
    public GroceryController(
      IOptions<AppSettings> appSettings,
      ILogger<GroceryController> logger,
      IStringLocalizer<GroceryController> localizer,
      ISpeechToTextService speechToTextService)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }
      else
      {
        this.appSettings = appSettings.Value;
      }

      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }
      else
      {
        this.logger = logger;
      }

      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }
      else
      {
        this.localizer = localizer;
      }

      if (speechToTextService == null)
      {
        throw new ArgumentNullException(nameof(speechToTextService));
      }
      else
      {
        this.speechToTextService = speechToTextService;
      }
    }

    /// <summary>
    /// Upload a sound file, for speech recognision.
    /// POST: api/Grocery.
    /// </summary>
    /// <remarks>The name of the parameter (blob here) MUST match the name of the field from the form.</remarks>
    /// <param name="blob">The binary value of the file.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile blob)
    {
      if (blob == null)
      {
        throw new ArgumentNullException(nameof(blob));
      }

      var totalSize = blob.Length;
      var fileBytes = new byte[blob.Length];

      using (var fileStream = blob.OpenReadStream())
      {
        var offset = 0;

        while (offset < blob.Length)
        {
          var chunkSize = totalSize - offset < 8192 ? (int)totalSize - offset : 8192;
          offset += await fileStream.ReadAsync(fileBytes, offset, chunkSize).ConfigureAwait(true);
        }
      }

      // now save the file on the filesystem
      string filePath = Path.GetTempFileName();
      System.IO.File.WriteAllBytes(filePath, fileBytes);

      string result = this.speechToTextService.SpeechToText(filePath);
      if (string.IsNullOrEmpty(result))
      {
        // TODO: Resx
        return this.NotFound(new { message = "Transcript not found" });
      }

      return this.Ok(new { message = result });
    }
  }
}
