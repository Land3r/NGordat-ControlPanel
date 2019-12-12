namespace NGordatControlPanel.Services.Google
{
  /// <summary>
  /// Interface <see cref="ISpeechToTextService"/>.
  /// Interface permettant d'intéragir avec les fonctionnalitées SpeechToText de Google.
  /// </summary>
  public interface ISpeechToTextService
  {
    /// <summary>
    /// Demande la transcription d'un fichier audio (format WAV ou FLAC).
    /// </summary>
    /// <param name="filepath">Le chemin du fichier audio.</param>
    /// <returns>Le transcript detecté.</returns>
    string SpeechToText(string filepath);
  }
}