<template>
  <q-layout view="hHh lpR fff">
    <q-header
      elevated
      class="app-header"
    >
      <q-toolbar>
        <q-btn
          flat
          dense
          round
          icon="menu"
          aria-label="Menu"
          @click="leftDrawerOpen = !leftDrawerOpen"
        />

        <q-toolbar-title>
          {{ title }}
        </q-toolbar-title>

        <div class="self-stretch row no-wrap">
          <app-usermenuwidget :username="username" />
          <app-languageselectorwidget />
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
        <q-item-label header>
          CP
        </q-item-label>
        <q-item
          clickable
          :to="{ name: 'IndexPage' }"
          :exact="true"
        >
          <q-item-section avatar>
            <q-icon name="home" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Accueil</q-item-label>
          </q-item-section>
        </q-item>
        <q-item
          clickable
          :to="{ name: 'GroceriesIndexPage' }"
        >
          <q-item-section avatar>
            <q-icon name="shopping_cart" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Courses</q-item-label>
          </q-item-section>
        </q-item>
        <q-item-label header>
          Admin
        </q-item-label>
        <q-expansion-item
          expand-separator
          icon="build"
          label="Pages"
          caption="Technical pages"
        >
          <q-item
            clickable
            :to="{ name: 'SpeechToTextPage' }"
          >
            <q-item-section avatar>
              <q-icon name="keyboard_voice" />
            </q-item-section>
            <q-item-section>
              <q-item-label>SpeechToText</q-item-label>
            </q-item-section>
          </q-item>
        </q-expansion-item>
        <q-separator />
        <q-item
          clickable
          tag="a"
          target="_blank"
          href="https://github.com/Land3r/NGordat-ControlPanel/issues"
          rel="noopener"
        >
          <q-item-section avatar>
            <q-icon name="bug_report" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Report a bug</q-item-label>
          </q-item-section>
        </q-item>
        <q-item
          clickable
          tag="a"
          target="_blank"
          href="https://github.com/Land3r/NGordat-ControlPanel"
          rel="noopener"
        >
          <q-item-section avatar>
            <q-icon name="fab fa-github" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Github</q-item-label>
            <q-item-label caption>
              github.com/Land3r/NGordat-ControlPanel
            </q-item-label>
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

import LanguageSelectorWidget from 'components/layout/widgets/LanguageSelectorWidget'
import UserMenuWidget from 'components/layout/widgets/UserMenuWidget'

export default {
  name: 'MainLayout',
  components: {
    'app-languageselectorwidget': LanguageSelectorWidget,
    'app-usermenuwidget': UserMenuWidget
  },
  data () {
    return {
      leftDrawerOpen: false,
      username: null
    }
  },
  computed: {
    title: function () {
      return process.env.WEBSITE_NAME
    }
  },
  created: function () {
    const userService = new UserService()
    this.username = userService.getUser().username
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
