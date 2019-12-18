<template>
  <q-layout view="hHh lpR fff">
    <q-header elevated class="app-header">
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          @click="leftDrawerOpen = !leftDrawerOpen"
          icon="menu"
          aria-label="Menu"
        />

        <q-toolbar-title>
          {{title}}
        </q-toolbar-title>

        <div class="self-stretch row no-wrap">
          <div>
            <app-usermenuwidget />
          </div>
          <div>
            <app-languageselectorwidget />
          </div>
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      content-class="app-menu"
    >
      <q-list>
        <q-item-label header>CP</q-item-label>
        <q-item clickable :to="{ name: 'IndexPage' }" :exact="true">
          <q-item-section avatar>
            <q-icon name="home" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Accueil</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable :to="{ name: 'GroceriesIndexPage' }">
          <q-item-section avatar>
            <q-icon name="shopping_cart" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Courses</q-item-label>
          </q-item-section>
        </q-item>
        <q-item-label header>Admin</q-item-label>
        <q-expansion-item
          expand-separator
          icon="build"
          label="Pages"
          caption="Technical pages"
        >
          <q-item clickable :to="{ name: 'SpeechToTextPage' }">
            <q-item-section avatar>
              <q-icon name="keyboard_voice" />
            </q-item-section>
            <q-item-section>
              <q-item-label>SpeechToText</q-item-label>
            </q-item-section>
          </q-item>
        </q-expansion-item>
        <q-separator />
        <q-item clickable tag="a" target="_blank" href="https://quasar.dev">
          <q-item-section avatar>
            <q-icon name="school" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Docs</q-item-label>
            <q-item-label caption>quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://github.quasar.dev">
          <q-item-section avatar>
            <q-icon name="code" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Github</q-item-label>
            <q-item-label caption>github.com/quasarframework</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://chat.quasar.dev">
          <q-item-section avatar>
            <q-icon name="chat" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Discord Chat Channel</q-item-label>
            <q-item-label caption>chat.quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://forum.quasar.dev">
          <q-item-section avatar>
            <q-icon name="record_voice_over" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Forum</q-item-label>
            <q-item-label caption>forum.quasar.dev</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://twitter.quasar.dev">
          <q-item-section avatar>
            <q-icon name="rss_feed" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Twitter</q-item-label>
            <q-item-label caption>@quasarframework</q-item-label>
          </q-item-section>
        </q-item>
        <q-item clickable tag="a" target="_blank" href="https://facebook.quasar.dev">
          <q-item-section avatar>
            <q-icon name="public" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Facebook</q-item-label>
            <q-item-label caption>@QuasarFramework</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import UserService from 'services/UserService'

import LanguageSelectorWidget from 'components/layout/LanguageSelectorWidget'
import UserMenuWidget from 'components/layout/UserMenuWidget'

export default {
  name: 'MainLayout',
  components: {
    'app-languageselectorwidget': LanguageSelectorWidget,
    'app-usermenuwidget': UserMenuWidget
  },
  data () {
    return {
      leftDrawerOpen: false
    }
  },
  computed: {
    title: function () {
      return process.env.WEBSITE_NAME
    }
  },
  methods: {
    doDisconnect: function () {
      var userService = new UserService()
      userService.disconnect()
      this.$router.push({ name: 'LoginPage' })
    }
  }
}
</script>

<style>
.app-header {
  background: linear-gradient(145deg,#027be3 11%,#014a88 75%);
}

.app-menu {
  background-image: linear-gradient(rgba(245, 245, 245, 1), rgba(245, 245, 245, 0.75)), url("~assets/space.jpg");
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
}
</style>
