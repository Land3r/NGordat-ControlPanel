﻿namespace NGordatControlPanel.Controllers
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

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class GroceryController : ControllerBase
  {
    /// <summary>
    /// Le Logger utilisé par le controller.
    /// </summary>
    private readonly ILogger<GroceryController> logger;

    /// <summary>
    /// Les ressources de langue.
    /// </summary>
    private readonly IStringLocalizer<GroceryController> localizer;

    /// <summary>
    /// Le service SpeechToText.
    /// </summary>
    private readonly ISpeechToTextService speechToTextService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GroceryController"/> class.
    /// API Controller for Grocery service.
    /// </summary>
    /// <param name="speechToTextService">The SpeechToTextService to use.</param>
    public GroceryController(
      ILogger<GroceryController> logger,
      IStringLocalizer<GroceryController> localizer,
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
    /// </summary>
    /// <param name="blob">The binary value of the file.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost("upload")]
    // attention name of formfile must be equal to the key u have used for formdata
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
