<template>
  <q-page class="q-px-md">
    <h3>{{ $t('groceriesindexpage.title') }}</h3>
    <br>
    <VueRecordAudio
      class="float-right"
      mode="press"
      :mime-type="recordingMimeType"
      @result="onAudioRecorded"
    />
    <app-typedspan
      :text="SexyTranscript"
      :repeat="repeat"
    />
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
    >
      <q-btn
        fab
        icon="fas fa-cog"
        color="accent"
        :to="{ name: 'GroceriesReferentialPage' }"
      />
    </q-page-sticky>
  </q-page>
</template>

<script>
import AudioRecorderPolyfill from 'audio-recorder-polyfill'
// import xss from 'xss'
import { NotifySuccess, NotifyFailure } from 'data/notify'

import GroceryService from 'services/GroceryService'
import TypedSpan from 'components/common/presentation/TypedSpan'

// const supportedTypes = [
//   'audio/aac',
//   'audio/ogg',
//   'audio/wav',
//   'audio/webm'
// ]

export default {
  name: 'GroceriesIndexPage',
  components: {
    'app-typedspan': TypedSpan
  },
  data: function () {
    return {
      // TODO: i18n
      // TODO: Vérifier si d'autres modes sont ok avec le reconnaissance et compatibles avec google.
      transcript: '🙊 Just say something 🙊',
      repeat: 0,
      recordingMimeType: 'audio/wav'
    }
  },
  computed: {
    SexyTranscript: function () {
      return this.transcript.charAt(0).toUpperCase() + this.transcript.slice(1)
    }
  },
  created: function () {
    // Inject polyfill for requested mime type support.
    if (window.MediaRecorder == null || !window.MediaRecorder.isTypeSupported(this.recordingMimeType)) {
      window.MediaRecorder = AudioRecorderPolyfill
    }
  },
  methods: {
    onAudioRecorded (blob) {
      const groceryService = new GroceryService()
      groceryService.doUpload(blob).then((response) => {
        // TODO: (temp) this.transcript = xss(response.message)
        console.dir(response)
        this.$q.notify({ ...NotifySuccess, message: this.$t('groceriesindexpage.success.transcriptsuccess', { transcript: response.message }) })
      }).catch((response) => {
        this.$q.notify({ ...NotifyFailure, message: this.$t('groceriesindexpage.error.transcriptfailure') })
      })
    }
  }
}
</script>
