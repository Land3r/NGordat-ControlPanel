# NGordat-ControlPanel Backend

The backend for the Control Panel.

## Built with

- [.Net Core](https://docs.microsoft.com/fr-fr/dotnet/core/) v3.1
- [MongoDB](https://www.mongodb.com/fr) v4.0.9
- [Recommended] [Visual Studio Community 2019](https://visualstudio.microsoft.com/fr/vs/)

## Configuration

### 1. Configuration

Project configuration can be found in `appsettings.json`.

### 2. Environment configuration

Create environment specific files :

- `appsettings.Development.json` for development variables
- `appsettings.Production.json` for production variables
You can refer to the `appsettings.json` file for variable references.
You can overwrite only the configuration variables that must change regarding your environment.

Example of `appsettings.Environment.json` :

```json
{
  "AppSettings": {
    "Environement": {
      "FrontUrl": "FRONTEND_URL",
      "BackUrl": "BACKEND_URL"
    },
    "Email": {
      "Smtp": {
        "Password": "GOOGLE_SMTP_PASSWORD"
      }
    },
    "Security": {
      "HashSalt": "PASSWORD_HASH_SALT",
      "JWT": {
        "Secret": "JWT_SECRET_KEY"
    }
  },
  "Logging": {
    "IncludeScopes": true,
    "LogLevel": {
      "Default": "Trace",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

### 3. Database

This software relies on MongoDb for storage.
You must :

- Create a MongoDb server.

>Install MongoDb server, use deamon to start it if needed.

You should then set the connection string in the `appsettings.json` file, under `AppSettings.MongoDb.ConnectionString` (mostly if you changed the default port).

- Create a MongoDb database.

```bash
use DATABASE_NAME
```

You should then set the chosen name in the `appsettings.json` file, under `AppSettings.MongoDb.DatabaseName`.

- [OPTIONAL] Populate database

>Execute the scripts in (/back/database) to initialy populate the database.
<aside class="">You can refer to the README in back/database folder in order to know more about running the scripts against the MongoDb instance.</aside>

### 4. Google SMTP account

This software uses Google SMTP for sending emails.
A Google account is required if you want to send emails.

>:warning: An unsecure method is applied for sending emails:
[https://support.google.com/cloudidentity/answer/6260879?hl=en](https://support.google.com/cloudidentity/answer/6260879?hl=en)

<br />

Note that the following [restrictions](https://support.google.com/a/answer/166852?hl=en) are applied to sending emails through Google.

### 5. Google Cloud Platform

This software uses Google Cloud Platform (GCP) for using SpeechToText features ([Cloud Speech-to-Text API](https://cloud.google.com/speech-to-text/docs/reference/rest/)).
A Google account with a configured Google Cloud Platform account configured is required.

>:warning: Note that you will need a credit card to register, despite the free credit provided.

You must create a service account under GCP, [enable the speech to text api](https://cloud.google.com/speech-to-text/) for this user, and [generate the credentials for the service account](https://todo)

Finally register the OAuth service account on the host platform
>Store the file in a secure filepath on the host filesystem.
>Register the file path in an environment variable named **GOOGLE_APPLICATION_CREDENTIALS** :
>>Example:
>>GOOGLE_APPLICATION_CREDENTIALS | C:\Users\ngordat\Documents\GitHub\NGordat-ControlPanel\back\GCP\Controlpanel-e29cb429f0d9.json
