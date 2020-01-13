namespace NGordatControlPanel.Controllers
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using System.Linq;

  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;

  using NGordatControlPanel.Services.Google;
    using NGordatControlPanel.Services.Groceries;
    using NGordatControlPanel.Settings;
    using MongoDB.Driver;

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
    /// The <see cref="IGroceryActionService"/>.
    /// </summary>
    private readonly IGroceryActionService groceryActionService;

    /// <summary>
    /// The <see cref="IGroceryItemService"/>.
    /// </summary>
    private readonly IGroceryItemService groceryItemService;

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
      ISpeechToTextService speechToTextService,
      IGroceryActionService groceryActionService,
      IGroceryItemService groceryItemService)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }

      if (logger == null)
      {
        throw new ArgumentNullException(nameof(logger));
      }

      if (localizer == null)
      {
        throw new ArgumentNullException(nameof(localizer));
      }

      if (speechToTextService == null)
      {
        throw new ArgumentNullException(nameof(speechToTextService));
      }

      if (groceryActionService == null)
      {
        throw new ArgumentNullException(nameof(groceryActionService));
      }

      if (groceryItemService == null)
      {
        throw new ArgumentNullException(nameof(groceryItemService));
      }

      this.appSettings = appSettings.Value;
      this.logger = logger;
      this.localizer = localizer;
      this.speechToTextService = speechToTextService;
      this.groceryActionService = groceryActionService;
      this.groceryItemService = groceryItemService;
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
      // TEMP: Bouchon

      //if (blob == null)
      //{
      //  throw new ArgumentNullException(nameof(blob));
      //}

      //var totalSize = blob.Length;
      //var fileBytes = new byte[blob.Length];

      //using (var fileStream = blob.OpenReadStream())
      //{
      //  var offset = 0;

      //  while (offset < blob.Length)
      //  {
      //    var chunkSize = totalSize - offset < 8192 ? (int)totalSize - offset : 8192;
      //    offset += await fileStream.ReadAsync(fileBytes, offset, chunkSize).ConfigureAwait(true);
      //  }
      //}

      //// Save the sound file on the filesystem.
      //string filePath = Path.GetTempFileName();
      //System.IO.File.WriteAllBytes(filePath, fileBytes);

      //// Requires transciption of the sound file.
      //string result = this.speechToTextService.SpeechToText(filePath);
      //if (string.IsNullOrEmpty(result))
      //{
      //  // TODO: Resx
      //  return this.NotFound(new { message = "Transcript not found" });
      //}

      //// TODO: Parse the results using groceryactions etc.

      //return this.Ok(new { message = result });

      string result = "Ajoute des pommes de terres";
      return Ok(Analyze(result));
    }

    private object Analyze(string input)
    {
      // Split des mots
      string[] words = input.Trim().Split(' ');

      // Process:
      // Essaie de trouver un pattern d'action sur le premier mot, l'étends au maximum.
      // Si pattern trouvé,
        // on recherche un nouveau pattern après.
        // Si pas de nouveaux pattern, on recherche un étendeur de qté ou produit.

      // Si pas de pattern trouvé, on avance d'un mot et on recherche encore.

      return words;
    }
  }
}
