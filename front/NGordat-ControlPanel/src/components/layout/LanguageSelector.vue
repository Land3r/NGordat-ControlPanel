<template>
  <q-select v-model="lang" map-options :options="langs">
    <template v-slot:selected-item="scope">
      <q-item>
        <q-item-section aside>
          <q-avatar :src="selectedLangFlag" />
        </q-item-section>
      </q-item>
    </template>
    <template v-slot:option="scope">
      <q-item
        v-bind="scope.itemProps"
        v-on="scope.itemEvents"
      >
        <q-item-section aside>
          <q-img :src="'statics/flags/'+scope.opt.icon" />
        </q-item-section>
        <!-- <q-item-section>
          <q-item-label v-html="scope.opt.label" />
        </q-item-section> -->
      </q-item>
    </template>
  </q-select>
</template>

<script>
import { availableLanguages } from 'data/languages'

export default {
  name: 'LanguageSelector',
  data: function () {
    return {
      langs: [
        ...availableLanguages
      ],
      lang: null
    }
  },
  mounted: function () {
    this.lang = this.langs.find(lang => lang.value === this.$i18n.locale)
  },
  computed: {
    selectedLangFlag: function () {
      // console.log('flag updated')
      // console.log('current lang:', this.lang)
      // console.log('lang found', JSON.stringify(this.langs.find(lang => lang.value === this.lang)))
      // let icon = this.langs.find(lang => lang.value === this.lang).icon
      return 'statics/flags/' + this.lang.icon
    }
  },
  watch: {
    // Watch for language change.
    lang (lang) {
      // Set vue-i18n language.
      this.$i18n.locale = lang.value

      // Set quasar's language.
      import(`quasar/lang/${lang.value}`).then(language => {
        this.$q.lang.set(language.default)
      })
    }
  }
}
</script>
