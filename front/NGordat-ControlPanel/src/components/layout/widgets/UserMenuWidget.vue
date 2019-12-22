<template>
  <q-btn-dropdown
    text-bold
    align="center"
    icon="perm_identity"
    flat
    no-caps
    stretch
    :label="username"
    auto-close
  >
    <q-list>
      <q-item-label header>
        Using quasar (v{{ $q.version }})
      </q-item-label>
      <q-item
        clickable
        tag="a"
        :to="{name: 'UserProfile' }"
      >
        <q-item-section avatar>
          <q-icon
            color="primary"
            name="perm_identity"
          />
        </q-item-section>
        <q-item-section>{{ $t('mainlayout.user.profile') }}</q-item-section>
      </q-item>
      <q-separator class="q-mb-sm q-mt-sm" />
      <q-item
        clickable
        tag="a"
        @click="doDisconnect"
      >
        <q-item-section avatar>
          <q-icon
            color="negative"
            name="close"
          />
        </q-item-section>
        <q-item-section>{{ $t('mainlayout.user.disconnect') }}</q-item-section>
      </q-item>
    </q-list>
  </q-btn-dropdown>
</template>

<script>
import UserService from 'services/UserService'

export default {
  name: 'UserMenu',
  props: {
    username: {
      type: String,
      default: 'Unknown user'
    }
  },
  data: function () {
    return {
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
