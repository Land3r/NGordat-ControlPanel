<template>
  <q-btn-dropdown
    text-bold
    align="center"
    flat
    no-caps
    stretch
    auto-close
  >
    <template v-slot:label>
      <q-img
        :src="selectedLangFlag"
        :ratio="16/9"
        style="width:48px;"
        transition="flip-down"
      />
    </template>
    <q-list>
      <q-item
        v-for="optionLang in langs"
        :key="optionLang.value"
        clickable
        tag="a"
        @click="setLang(optionLang)"
      >
        <q-item-section>
          <q-img
            :ratio="16/9"
            :src="'statics/flags/' + optionLang.icon"
            style="width:60%;margin-left:auto;margin-right:auto;"
          />
        </q-item-section>
      </q-item>
    </q-list>
  </q-btn-dropdown>
</template>

<script>
import { availableLanguages } from 'data/languages'

export default {
  name: 'LanguageSelectorWidget',
  data: function () {
    return {
      langs: [
        ...availableLanguages
      ],
      lang: null
    }
  },
  computed: {
    selectedLangFlag: function () {
      return 'statics/flags/' + this.lang.icon
    },
    selectedLangValue: function () {
      return this.lang.icon
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
  },
  created: function () {
    this.lang = this.langs.find(lang => lang.value === this.$i18n.locale)
  },
  methods: {
    setLang (lang) {
      this.lang = lang
    }
  }
}
</script>
