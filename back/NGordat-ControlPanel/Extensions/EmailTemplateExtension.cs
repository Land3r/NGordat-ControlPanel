namespace NGordatControlPanel.Extensions
{
  using System;
  using System.Reflection;

  using NGordatControlPanel.Entities.Emails;

  /// <summary>
  /// Collection of helpers for dealing with Emails.
  /// </summary>
  public static class EmailTemplateExtension
  {
    /// <summary>
    /// Compile an email using the provided <see cref="EmailTemplate"/> and parameters.
    /// </summary>
    /// <param name="emailTemplate">The <see cref="EmailTemplate"/> to compile.</param>
    /// <param name="values">The collection of parameters to inject into the email.</param>
    /// <returns>A <see cref="Tuple{string, string}"/> containing the compiled subject and body of the email.</returns>
    public static (string subject, string body) Compile(this EmailTemplate emailTemplate, dynamic values)
    {
      if (emailTemplate == null)
      {
        throw new ArgumentNullException(nameof(emailTemplate));
      }

      string subjectTemplate = emailTemplate.Subject;
      string bodyTemplate = emailTemplate.Body;

      // Inject token values into placeholders.
      foreach (var prop in values.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        subjectTemplate = subjectTemplate.Replace($"{{{prop.Name}}}", prop.GetValue(values, null));
        bodyTemplate = bodyTemplate.Replace($"{{{prop.Name}}}", prop.GetValue(values, null));
      }

      return (subject: subjectTemplate, body: bodyTemplate);
    }
  }
}
