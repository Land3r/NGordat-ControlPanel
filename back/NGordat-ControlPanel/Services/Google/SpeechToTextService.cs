namespace NGordatControlPanel.Services.Google
{
  using System;
  using System.IO;
  using global::Google.Cloud.Speech.V1;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Options;
  using NGordatControlPanel.Services.Core;
  using NGordatControlPanel.Settings;
  using static global::Google.Cloud.Speech.V1.RecognitionConfig.Types;

  /// <summary>
  /// <see cref="SpeechToTextService"/> class.
  /// Class service for google speech-to-text API.
  /// </summary>
  public class SpeechToTextService : ALoggedLocalizedService<SpeechToTextService>, ISpeechToTextService
  {
    /// <summary>
    /// The application configuration.
    /// </summary>
    private readonly AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeechToTextService"/> class.
    /// </summary>
    /// <param name="appSettings">The application configuration.</param>
    /// <param name="localizer">The localized ressources.</param>
    /// <param name="logger">The logger.</param>
    public SpeechToTextService(
      IOptions<AppSettings> appSettings,
      [FromServices]IStringLocalizer<SpeechToTextService> localizer,
      [FromServices] ILogger<SpeechToTextService> logger)
      : base(logger, localizer)
    {
      if (appSettings == null)
      {
        throw new ArgumentNullException(nameof(appSettings));
      }

      this.appSettings = appSettings.Value;
    }

    /// <summary>
    /// Transcripts the provided audio file.
    /// </summary>
    /// <remarks>WAV format is currently required.</remarks>
    /// <param name="filepath">The path to the audio file.</param>
    /// <returns>The transcript retrieved, if any.</returns>
    public string SpeechToText(string filepath)
    {
      if (string.IsNullOrEmpty(filepath))
      {
        throw new ArgumentNullException(nameof(filepath));
      }

      if (!File.Exists(filepath))
      {
        throw new ArgumentException((this as ILocalizedService<SpeechToTextService>).GetLocalized("FileNotFoundError", filepath), nameof(filepath));
      }

      // TODO: Voir maintenant que le front a un polyfill pour le support, si un format plus léger serait tout aussi efficace.
      SpeechClient speech = SpeechClient.Create();
      RecognizeResponse response = speech.Recognize(
        new RecognitionConfig()
        {
          Encoding = AudioEncoding.Linear16,
          SampleRateHertz = 48000,
          LanguageCode = this.appSettings.Google.SpeechToText.LanguageCode,
        },
        RecognitionAudio.FromFile(filepath));

      foreach (SpeechRecognitionResult result in response.Results)
      {
        foreach (SpeechRecognitionAlternative alternative in result.Alternatives)
        {
          return alternative.Transcript;
        }
      }

      return null;
    }
  }
}
