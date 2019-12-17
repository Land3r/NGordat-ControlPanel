namespace NGordatControlPanel.Helpers
{
  using System;
  using System.Globalization;
  using System.Linq;
  using System.Security.Cryptography;
  using System.Text;

  /// <summary>
  /// <see cref="CryptographicHelper"/> class.
  /// Collection of helpers for dealing with cryptography.
  /// </summary>
  public static class CryptographicHelper
  {
    /// <summary>
    /// The alphabet used (base62).
    /// </summary>
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-.";

    /// <summary>
    /// Gets a token that can be safely provided to URI.
    /// It can be transmitted over a GET parameter without any re-encoding.
    /// </summary>
    /// <param name="length">The length of the token to generate.</param>
    /// <returns>The generated token.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1055:Uri return values should not be strings", Justification = "This is not meant to produce Uri.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0063:Use simple 'using' statement", Justification = "Simple design is too concive.")]
    public static string GetUrlSafeToken(int length)
    {
      if (length == 0)
      {
        // TODO
        throw new ArgumentException("Length must be greater than 0", nameof(length));
      }

      using (RandomNumberGenerator rnd = new RNGCryptoServiceProvider())
      {
        byte[] tokenBytes = new byte[length];
        rnd.GetBytes(tokenBytes);
        var token = Enumerable
                      .Range(0, length)
                      .Select(i => Alphabet[tokenBytes[i] % Alphabet.Length])
                      .ToArray();

        return new string(token);
      }
    }

    /// <summary>
    /// Gets the hashed value of a byte array.
    /// </summary>
    /// <param name="clear">The byte array to hash.</param>
    /// <returns>The hashed value.</returns>
    public static byte[] GetHash(byte[] clear)
    {
      if (clear == null)
      {
        throw new ArgumentNullException(nameof(clear));
      }

      byte[] result;
      using (SHA512 shaM = new SHA512Managed())
      {
        result = shaM.ComputeHash(clear);
      }

      return result;
    }

    /// <summary>
    /// Gets the hashed value of a string in UTF8 format.
    /// </summary>
    /// <param name="clear">The string to hash (in UTF8).</param>
    /// <returns>The hashed value.</returns>
    public static string GetHash(string clear)
    {
      if (string.IsNullOrEmpty(clear))
      {
        throw new ArgumentNullException(nameof(clear));
      }

      byte[] data = Encoding.UTF8.GetBytes(clear);
      byte[] result = GetHash(data);

      return ByteArrayToString(result);
    }

    /// <summary>
    /// Gets the hashed value of a byte array, using a salt.
    /// </summary>
    /// <param name="clear">The byte array to hash.</param>
    /// <param name="salt">The salt byte array.</param>
    /// <returns>The hashed value.</returns>
    public static byte[] GetHash(byte[] clear, byte[] salt)
    {
      byte[] data = clear.Concat(salt).ToArray();
      byte[] result = GetHash(data);

      return result;
    }

    /// <summary>
    /// Gets the hashed value of a string in UTF8 format, using a salt.
    /// </summary>
    /// <param name="clear">The string to hash (in UTF8).</param>
    /// <param name="salt">The string to use as a salt.</param>
    /// <returns>The hashed value.</returns>
    public static string GetHash(string clear, string salt)
    {
      string saltedclear = string.Concat(salt, clear);
      byte[] data = Encoding.UTF8.GetBytes(saltedclear);
      byte[] result = GetHash(data);

      return ByteArrayToString(result);
    }

    /// <summary>
    /// Converts a byte array into an UTF8 string.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>The UTF8 string equivalent to the byte array.</returns>
    private static string ByteArrayToString(byte[] bytes)
    {
      StringBuilder hex = new StringBuilder(bytes.Length * 2);
      foreach (byte b in bytes)
      {
        hex.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", b);
      }

      return hex.ToString();
    }
  }
}
