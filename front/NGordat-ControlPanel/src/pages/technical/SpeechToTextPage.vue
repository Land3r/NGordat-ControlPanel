<template>
  <q-page class="q-px-md">
    <h3>{{$t('speechtotextpage.title')}}</h3>
    <br />
    <VueRecordAudio
      class="float-right"
      mode="press"
      :mimeType="recordingMimeType"
      @result="onAudioRecorded" />
      <app-typedspan :text="SexyTranscript" :repeat="repeat" />
  </q-page>
</template>

<script>
import AudioRecorderPolyfill from 'audio-recorder-polyfill'
import xss from 'xss'
import { NotifySuccess, NotifyFailure } from 'data/notify'

import SpeechToTextService from 'services/SpeechToTextService'
import TypedSpan from 'components/common/presentation/TypedSpan'

// const supportedTypes = [
//   'audio/aac',
//   'audio/ogg',
//   'audio/wav',
//   'audio/webm'
// ]

export default {
  name: 'SpeechToTextPage',
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
  methods: {
    onAudioRecorded (blob) {
      const speechToTextService = new SpeechToTextService()
      speechToTextService.doUpload(blob).then((response) => {
        this.transcript = xss(response.message)
        this.$q.notify({ ...NotifySuccess, message: this.$t('speechtotextpage.success.transcriptsuccess', { transcript: xss(response.message) }) })
      }).catch((response) => {
        this.$q.notify({ ...NotifyFailure, message: this.$t('speechtotextpage.error.transcriptfailure') })
      })
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
  }
}
</script>
