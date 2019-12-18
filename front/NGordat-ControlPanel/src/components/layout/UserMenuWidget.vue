<template>
<!-- <q-btn-dropdown text-bold(:align="align", flat, no-caps, stretch, :label="`v${$q.version}`", auto-close)
    q-list(dense padding)
      q-item-label(header) Latest (v{{ $q.version }})
      q-item(clickable, to="/start/release-notes")
        q-item-section.text-teal(avatar)
          q-icon(name="assignment")
        q-item-section Release notes

      q-item(clickable, tag="a", href="https://github.com/quasarframework/quasar/issues", target="_blank", rel="noopener")
        q-item-section.text-purple(avatar)
          q-icon(name="bug_report")
        q-item-section Report a bug

      q-item(clickable, tag="a", href="https://github.com/quasarframework/quasar", target="_blank", rel="noopener")
        q-item-section(avatar)
          q-icon(name="fab fa-github")
        q-item-section Repository

      q-separator.q-mb-sm.q-mt-md

      q-item-label(header) Older Releases
      q-item(
        v-for="version in ['17', '16', '15', '14', '13']"
        :key="version"
        clickable
        tag="a"
        :href="`https://v0-${version}.quasar-framework.org/`"
        target="_blank"
        rel="noopener"
      )
        q-item-section {{ `v0.${version}` }} docs -->
<div>
  <q-btn class="q-btn q-btn-item non-selectable no-outline text-bold q-btn--flat q-btn--rectangle q-btn--actionable q-focusable q-hoverable q-btn--no-uppercase no-border-radius self-stretch q-btn-dropdown q-btn-dropdown--simple">
    <q-avatar>
      <q-icon name="perm_identity" />
    </q-avatar>
    <span style="vertical-align:middle">Test</span>
  </q-btn>

    <q-menu
      transition-show="jump-down"
      transition-hide="jump-up"
    >
      <q-list style="min-width: 100px">
        <q-item clickable>
          <q-item-section avatar>
            <q-icon color="primary" name="perm_identity" />
          </q-item-section>
          <q-item-section>{{$t('mainlayout.user.profile')}}</q-item-section>
        </q-item>
        <q-separator />
        <q-item clickable @click="doDisconnect">
          <q-item-section avatar>
            <q-icon color="negative" name="close" />
          </q-item-section>
          <q-item-section>{{$t('mainlayout.user.disconnect')}}</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
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
