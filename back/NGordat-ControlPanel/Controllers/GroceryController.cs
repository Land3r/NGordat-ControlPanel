namespace NGordatControlPanel.Controllers
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using System.Web;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using NGordatControlPanel.Services.Google;
  using NGordatControlPanel.Settings;

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class SpeechToTextController : ControllerBase
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    private readonly ILogger<UsersController> logger;

    /// <summary>
    /// Les ressources de langue.
    /// </summary>
    private readonly IStringLocalizer<UsersController> localizer;

    /// <summary>
    /// Le service SpeechToText.
    /// </summary>
    private readonly ISpeechToTextService speechToTextService;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="SpeechToTextController"/>.
    /// Controlleur API permettant d'effectuer de la conversion Speech to Text.
    /// </summary>
    /// <param name="speechToTextService">Le service Speech to Text à utiliser.</param>
    public SpeechToTextController(
      IOptions<AppSettings> appSettings,
      ILogger<UsersController> logger,
      IStringLocalizer<UsersController> localizer,
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
    /// </summary>
    /// <param name="blob">The binary value of the file.</param>
    /// <remarks>The name of the parameter (blob here) MUST match the name of the field from the form.</remarks>
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

      return Ok(new { message = result });
    }
  }
}
