# NGordat-ControlPanel Frontend

The frontend for the Control Panel.

## Tools

- [NodeJS](https://nodejs.org/en/) with npm
- [Yarn](https://yarnpkg.com/en/)
- [Quasar CLI](https://quasar.dev/quasar-cli/installation)
- [Recommended] [Visual Studio Code](https://code.visualstudio.com/)

## Configuration

### 1. Dotenv  

Create environment specific files :

- `.env.development` for development variables
- `.env.production` for production variables
You can refer to the `.env.example` file for variable references.

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

### Misc

If you are using Visual Studio Code, please read [the following quasar guide](https://quasar.dev/start/vs-code-configuration#Introduction) in order to take advantage of the editor.
