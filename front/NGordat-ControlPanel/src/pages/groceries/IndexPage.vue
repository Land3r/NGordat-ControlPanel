<template>
  <q-page class="q-px-md">
    <h3>{{$t('groceriesindexpage.title')}}</h3>
    <br />
    <VueRecordAudio
      mode="press"
      @result="onAudioRecorded" />
  </q-page>
</template>

<script>

import SpeechToTextService from 'services/SpeechToTextService'

export default {
  name: 'GroceriesIndexPage',
  components: {
  },
  data: function () {
    return {
      result: ''
    }
  },
  methods: {
    onAudioRecorded (blob) {
      const speechToTextService = new SpeechToTextService()
      speechToTextService.doUpload(blob).then((response) => {
        console.dir(response)
      }).catch((response) => {
        // TODO: Ajouter les messages localisés.
        console.error(response)
      })
    }
  }
}
</script>
