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

  using NGordatControlPanel.Services.Google;

  /// <summary>
  /// <see cref="SpeechToTextController"/> class.
  /// API Controller for converting speech to text using cognitive services.
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class SpeechToTextController : ControllerBase
  {
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<SpeechToTextController> logger;

    /// <summary>
    /// The localized ressources.
    /// </summary>
    private readonly IStringLocalizer<SpeechToTextController> localizer;

    /// <summary>
    /// The <see cref="ISpeechToTextService"/>.
    /// </summary>
    private readonly ISpeechToTextService speechToTextService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeechToTextController"/> class.
    /// API Controller for Grocery service.
    /// </summary>
    /// <param name="logger">The logger to use.</param>
    /// <param name="localizer">The localized ressources to use.</param>
    /// <param name="speechToTextService">The SpeechToTextService to use.</param>
    public SpeechToTextController(
      ILogger<SpeechToTextController> logger,
      IStringLocalizer<SpeechToTextController> localizer,
      ISpeechToTextService speechToTextService)
    {
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
    /// POST: api/SpeechToText.
    /// <remarks>The name of the parameter (blob here) MUST match the name of the field from the form.</remarks>
    /// </summary>
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
