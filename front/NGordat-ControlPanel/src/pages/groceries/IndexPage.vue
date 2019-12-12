<template>
  <q-page class="q-px-md">
    <h3>{{$t('groceriesindexpage.title')}}</h3>
    <br />
    <VueRecordAudio
      mode="press"
      :mimeType="recordingMimeType"
      @result="onAudioRecorded" />
  </q-page>
</template>

<script>
import AudioRecorderPolyfill from 'audio-recorder-polyfill'
import xss from 'xss'
import { NotifySuccess, NotifyFailure } from 'data/notify'

import SpeechToTextService from 'services/SpeechToTextService'

// const supportedTypes = [
//   'audio/aac',
//   'audio/ogg',
//   'audio/wav',
//   'audio/webm'
// ]

export default {
  name: 'GroceriesIndexPage',
  components: {
  },
  data: function () {
    return {
      recordingMimeType: 'audio/wav'
    }
  },
  methods: {
    onAudioRecorded (blob) {
      const speechToTextService = new SpeechToTextService()
      speechToTextService.doUpload(blob).then((response) => {
        this.$q.notify({ ...NotifySuccess, message: this.$t('groceriesindexpage.success.transcriptsuccess', { transcript: xss(response.message) }) })
      }).catch((response) => {
        this.$q.notify({ ...NotifyFailure, message: this.$t('groceriesindexpage.error.transcriptfailure') })
      })
    }
  },
  created: function () {
    // Inject polyfill for requested mime type support.
    if (window.MediaRecorder == null || !window.MediaRecorder.isTypeSupported(this.recordingMimeType)) {
      window.MediaRecorder = AudioRecorderPolyfill
    }
  }
}
</script>
