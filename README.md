# NGordat-ControlPanel

A Control Panel for managing different aspects of your digital life.

## Features

- User managment.
- Google SpeechToText.
- Grocery list using SpeechToText.

## Built with

- [Quasar framework](https://quasar.dev) v1.5.7
- [VueJS](https://vuejs.org/) v2.x
- [.Net Core](https://docs.microsoft.com/fr-fr/dotnet/core/) v3.1
- [MongoDB](https://www.mongodb.com/fr) v4.0.9
- [Recommended] [Visual Studio Community 2019](https://visualstudio.microsoft.com/fr/vs/)
- [Recommended] [Visual Studio Code](https://code.visualstudio.com/)

## Prerequisites

- Visual studio community 2019
- Net Core 3.1+
- MongoDb
- SMTP Server (Gmail SMTP is used in this case)

## Configuration

### Backend

You can follow the backend configuration guide at `/back/README.md`

The configuration of the backend is located in `/back/NGordat-ControlPanel/appsettings.json`.
You can also override `appsettings.Development.json` and `appsettings.Production.json` that are environment specifics.

### Frontend

You can follow the frontend configuration guide at `/front/README.md`

The configuration of the frontend is done using dotenv packages.
The configuration is located at `/front/NGordat-ControlPanel/.env.development` and `/front/NGordat-ControlPanel/.env.production` depending on the environment.
