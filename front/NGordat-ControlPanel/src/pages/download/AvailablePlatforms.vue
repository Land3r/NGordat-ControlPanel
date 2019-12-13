<template>
  <q-page class="q-px-md">
    <h3>{{$t('downloadavailableplatforms.title')}}</h3>
    <p>{{$t('downloadavailableplatforms.description')}}</p>
    <br />
    <p class="text-h4">Desktop</p>
    <div class="row">
      <div class="col q-mx-sm">
        <q-card>
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">Windows</div>
          </q-card-section>
          <q-separator />
          <q-card-section>
            <br />
            <img src="~assets/images/platforms/windows.png" class="platform" />
            <br />
            <q-btn class="full-width bg-primary text-white" @click="downloadElectron">Télécharger</q-btn>
          </q-card-section>
          <q-card-section class="text-center">
            Version {{electron.version}}
          </q-card-section>
        </q-card>
      </div>
      <div class="col q-mx-sm">
        <q-card>
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">MacOS</div>
          </q-card-section>
          <q-separator />
          <q-card-section>
            <br />
            <img src="~assets/images/platforms/macos.png" class="platform" />
            <br />
            <q-btn class="full-width text-white bg-secondary">Télécharger</q-btn>
          </q-card-section>
          <q-card-section class="text-center">
            Version {{electron.version}}
          </q-card-section>
        </q-card>
      </div>
      <div class="col q-mx-sm">
        <q-card>
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">Linux</div>
          </q-card-section>
          <q-separator />
          <q-card-section>
            <br />
            <img src="~assets/images/platforms/linux.png" class="platform" />
            <br />
            <q-btn class="full-width text-white bg-info">Télécharger</q-btn>
          </q-card-section>
          <q-card-section class="text-center">
            Version {{electron.version}}
          </q-card-section>
        </q-card>
      </div>
    </div>
    <br />
    <p class="text-h4">Mobile</p>
    <div class="row">
      <div class="col q-mx-sm">
        <q-card>
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">Android</div>
          </q-card-section>
          <q-separator />
          <q-card-section>
            <br />
            <img src="~assets/images/platforms/android.png" class="platform" />
            <br />
            <q-btn class="full-width bg-primary text-white">Télécharger</q-btn>
          </q-card-section>
          <q-card-section class="text-center">
            Version {{cordova.version}}
          </q-card-section>
        </q-card>
      </div>
      <div class="col q-mx-sm">
        <q-card>
          <q-card-section class="bg-primary text-white">
            <div class="text-h6">iOS</div>
          </q-card-section>
          <q-separator />
          <q-card-section>
            <br />
            <img src="~assets/images/platforms/ios.png" class="platform" />
            <br /><br /><br />
            <q-btn class="full-width text-white bg-secondary">Télécharger</q-btn>
          </q-card-section>
          <q-card-section class="text-center">
            Version {{cordova.version}}
          </q-card-section>
        </q-card>
      </div>
      <div class="col q-mx-sm">
      </div>
    </div>
  </q-page>
</template>

<script>
import { electronVersions, cordovaVersions } from 'data/builds'

export default {
  name: 'DownloadAvailablePlatformsPage',
  components: {
  },
  data: function () {
    return {
      electron: {
        version: electronVersions[0]
      },
      cordova: {
        version: cordovaVersions[0]
      },
      result: ''
    }
  },
  methods: {
    downloadElectron: function () {
      const link = document.createElement('a')
      link.href = 'statics/builds/' + this.electronFile
      link.setAttribute('download', this.electronFile)
      document.body.appendChild(link)
      link.click()
    }
  },
  computed: {
    electronFile: function () {
      return 'NGordat-ControlPanel Setup ' + this.electron.version + '.exe'
    }
  }
}
</script>

<style>
img.platform {
  width: 50%;
  display: block;
  margin-left: auto;
  margin-right: auto;
}
</style>
