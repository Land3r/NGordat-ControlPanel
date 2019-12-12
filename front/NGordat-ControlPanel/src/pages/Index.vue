<template>
  <q-page class="q-px-md">
    <h3>{{$t('indexpage.title')}}</h3>
    <p>{{$t('indexpage.description')}}</p>
    <q-btn color="white" text-color="black" :label="$t('indexpage.btn.testApi')" @click="doTestApi" />
    <br />
    <br />
    Result:
    <div>
      <code>
      {{result}}
      </code>
    </div>
  </q-page>
</template>

<script>
// TODO (mal placé): Lors du login, on est authentifié, alors que les headers JWT ne sont pas mis.
import UserService from 'services/UserService'

export default {
  name: 'IndexPage',
  components: {
  },
  data: function () {
    return {
      result: ''
    }
  },
  methods: {
    doTestApi: function () {
      const userservice = new UserService()
      userservice.getCurrentUser().then((response) => {
        this.result += JSON.stringify(response)
      }).catch((response) => {
        this.result = JSON.stringify(response)
      })
    }
  }
}
</script>
