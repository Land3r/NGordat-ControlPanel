namespace NGordatControlPanel.Controllers
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using System.Web;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using NGordatControlPanel.Services.Google;

  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class SpeechToTextController : ControllerBase
  {
    /// <summary>
    /// Le service SpeechToText.
    /// </summary>
    private readonly ISpeechToTextService speechToTextService;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="SpeechToTextController"/>.
    /// Controlleur API permettant d'effectuer de la conversion Speech to Text.
    /// </summary>
    /// <param name="speechToTextService">Le service Speech to Text à utiliser.</param>
    public SpeechToTextController(ISpeechToTextService speechToTextService)
    {
      if (speechToTextService == null)
      {
        throw new ArgumentNullException(nameof(speechToTextService));
      }
      else
      {
        this.speechToTextService = speechToTextService;
      }
    }

    [HttpPost("upload")]
    // attention name of formfile must be equal to the key u have used for formdata
    public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile blob)
    {
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
        return this.NotFound(new { message = "Transcript not found" });
      }

      return Ok(new { message = result });
    }
  }
}
