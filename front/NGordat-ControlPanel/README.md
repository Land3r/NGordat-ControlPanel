# NGordat-ControlPanel

A Control Panel for managing different aspects of your digital life.

## Features
- User managment.
- Google SpeechToText.
- Grocery list using SpeechToText.

## Prepare the environment
### 1. Dotenv  
Create environment specific files :
- `.env.development` for development variables
- `.env.production` for production variables
You can refer to the `.env.example` file for variable references.

### 2. Google Cloud Platform
In order to use SpeechToText features, a GCP account is necessary.
TODO: Document the process to request a service cloud account with .Json generation and adding it to the system variables.

## Install the dependencies
```bash
yarn install
```

### Start the app in development mode (hot-code reloading, error reporting, etc.)
```bash
quasar dev
```

### Lint the files
```bash
yarn run lint
```

### Build the app for production
```bash
quasar build
```

### Customize the configuration
See [Configuring quasar.conf.js](https://quasar.dev/quasar-cli/quasar-conf-js).
