namespace NGordatControlPanel.Services.Google
{
  /// <summary>
  /// <see cref="ISpeechToTextService"/> interface.
  /// Interface service for google speech-to-text API.
  /// </summary>
  public interface ISpeechToTextService
  {
    /// <summary>
    /// Transcripts the provided audio file.
    /// </summary>
    /// <remarks>WAV format is currently required.</remarks>
    /// <param name="filepath">The path to the audio file.</param>
    /// <returns>The transcript retrieved, if any.</returns>
    string SpeechToText(string filepath);
  }
}