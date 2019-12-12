namespace NGordat_ControlPanel.Controllers
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Google.Cloud.Speech.V1;
    using Google.Protobuf.Collections;

    [Route("api/[controller]")]
  [ApiController]
  public class SpeechToTextController : ControllerBase
  {
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

      return Ok();
    }
  }
}
