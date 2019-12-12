namespace NGordatControlPanel.Services.Google
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;
  using global::Google.Cloud.Speech.V1;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;

  /// <summary>
  /// Classe <see cref="SpeechToTextService"/>.
  /// Classe de service permettant de contacter google speech-to-text API.
  /// </summary>
  public class SpeechToTextService : ALoggedLocalizedService<SpeechToTextService>, ISpeechToTextService
  {
    /// <summary>
    /// La configuration de l'application.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// Instancie une nouvelle instance de la classe <see cref="SpeechToTextService"/>.
    /// </summary>
    /// <param name="localizer">Les ressources localisées.</param>
    /// <param name="appSettings">La configuration de l'application.</param>
    /// <param name="logger">Le logger.</param>
    public SpeechToTextService(
      [FromServices]IStringLocalizer<SpeechToTextService> localizer,
      IOptions<AppSettings> appSettings,
      [FromServices] ILogger<SpeechToTextService> logger)
      : base(logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }

      this.appSettings = appSettings.Value;
    }

    // TODO
    /// <summary>
    /// Demande la transcription d'un fichier audio (format WAV ou FLAC).
    /// </summary>
    /// <param name="filepath">Le chemin du fichier audio.</param>
    public void SpeechToText(string filepath)
    {
      if (string.IsNullOrEmpty(filepath))
      {
        throw new ArgumentNullException(nameof(filepath));
      }

      if (!File.Exists(filepath))
      {
        throw new ArgumentException((this as ILocalizedService<SpeechToTextService>).GetLocalized("FileNotFoundError", filepath), nameof(filepath));
      }

      SpeechClient speech = SpeechClient.Create();
      RecognizeResponse response = speech.Recognize(
        new RecognitionConfig()
        {
          Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
          SampleRateHertz = 16000,
          LanguageCode = this.appSettings.Google.SpeechToText.LanguageCode,
        },
        RecognitionAudio.FromFile(filepath));

      foreach (SpeechRecognitionResult result in response.Results)
      {
        foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
        {
          Console.WriteLine($"Phrase: {alternative.Transcript}; Confiance: {alternative.Confidence}");
          Console.Write($"Mots: ");
          foreach (WordInfo word in alternative.Words)
          {
            Console.Write(word.Word);
          }

          Console.WriteLine(string.Empty);
        }
      }
    }
  }
}
