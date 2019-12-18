<template>
<div>
<q-btn-dropdown text-bold align="center" icon="perm_identity" flat no-caps stretch label="TODO Username" auto-close>
    <q-list dense padding>
      <q-item-label header>
         Latest (v{{ $q.version }})
      </q-item-label>
      <q-item clickable to="/start/release-notes">
        <q-item-section avatar text-teal>
          <q-icon name="assignment" />
        </q-item-section>
        <q-item-section>
          Release notes
        </q-item-section>
      </q-item>
      <q-item clickable>
        <q-item-section avatar>
          <q-icon color="primary" name="perm_identity" />
        </q-item-section>
        <q-item-section>{{$t('mainlayout.user.profile')}}</q-item-section>
      </q-item>
      <q-item clickable @click="doDisconnect">
        <q-item-section avatar>
          <q-icon color="negative" name="close" />
        </q-item-section>
        <q-item-section>{{$t('mainlayout.user.disconnect')}}</q-item-section>
      </q-item>
    </q-list>

      <q-item clickable tag="a" href="https://github.com/quasarframework/quasar/issues" target="_blank" rel="noopener">
        <q-item-section avatar>
          <q-icon name="bug_report" />
        </q-item-section>
        <q-item-section>
          Report a bug
        </q-item-section>
      </q-item>

      <q-item clickable tag="a" href="https://github.com/quasarframework/quasar" target="_blank" rel="noopener">
        <q-item-section avatar>
          <q-icon name="fab fa-github" />
        </q-item-section>
        <q-item-section>
          Repository
        </q-item-section>
      </q-item>
      <q-separator class="q-mb-sm q-mt-md" />

      <q-item-label header>
        Older Releases
      </q-item-label>
      <q-item
        v-for="version in ['17', '16', '15', '14', '13']"
        :key="version"
        clickable
        tag="a"
        :href="`https://v0-${version}.quasar-framework.org/`"
        target="_blank"
        rel="noopener"
      >
        <q-item-section>
          {{ `v0.${version}` }} docs
        </q-item-section>
      </q-item>
  </q-btn-dropdown>
  </div>
</template>

<script>

export default {
  name: 'UserMenu',
  data: function () {
    return {
    }
  }
}
</script>
