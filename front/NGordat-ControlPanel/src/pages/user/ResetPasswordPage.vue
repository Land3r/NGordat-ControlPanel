<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-publiccard>
        <q-card-section class="bg-primary text-white">
          <div class="text-h6">
            <q-icon
              name="security"
              size="md"
              left
            />{{ $t('resetpasswordpage.title') }}
          </div>
        </q-card-section>

        <q-separator />
        <q-card-section>
          <q-form>
            <q-input
              v-model="form.username"
              color="primary"
              type="text"
              :label="$t('resetpasswordpage.form.username')"
              clear-icon="close"
              readonly
            >
              <template v-slot:prepend>
                <q-icon name="perm_identity" />
              </template>
            </q-input>
            <br>
            <q-input
              v-model="form.email"
              color="primary"
              :label="$t('resetpasswordpage.form.email')"
              type="email"
              clear-icon="close"
              readonly
            >
              <template v-slot:prepend>
                <q-icon name="mail" />
              </template>
            </q-input>
            <br>
            <q-input
              v-model="form.newpassword"
              color="primary"
              :label="$t('resetpasswordpage.form.newpassword')"
              :type="showNewPassword ? 'text' : 'password'"
              autofocus
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('resetpasswordpage.form.newpassword')}),
                val => val.length >= 6 || $t('validationerror.minlength', {field: $t('resetpasswordpage.form.newpassword'), length: 6})
              ]"
            >
              <template v-slot:append>
                <q-icon
                  :name="showNewPassword ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="showNewPassword = !showNewPassword"
                />
              </template>
            </q-input>
            <q-input
              v-model="form.newpassword2"
              color="primary"
              :label="$t('resetpasswordpage.form.newpassword2')"
              :type="showNewPassword2 ? 'text' : 'password'"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('resetpasswordpage.form.newpassword')}),
                val => val.length >= 6 || $t('validationerror.minlength', {field: $t('resetpasswordpage.form.newpassword'), length: 6}),
                val => val == form.newpassword || $t('validationerror.mustmatch', {field: $t('resetpasswordpage.form.newpassword')})
              ]"
            >
              <template v-slot:append>
                <q-icon
                  :name="showNewPassword2 ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="showNewPassword2 = !showNewPassword2"
                />
              </template>
            </q-input>
            <br>
            <br>
            <q-btn
              class="bg-primary text-white full-width"
              type="submit"
              :loading="isLoading"
              :disable="isLoading || !isFormValid"
              @click="doResetPassword"
            >
              {{ $t('resetpasswordpage.btn.confirm') }}
            </q-btn>
          </q-form>
          <q-btn
            class="bg-secondary text-white full-width q-mt-md q-mb-xs"
            :to="{ name: 'LoginPage' }"
          >
            {{ $t('resetpasswordpage.btn.login') }}
          </q-btn>
        </q-card-section>
      </app-publiccard>
    </app-transition>
  </q-page>
</template>

<script>
import { NotifySuccess, NotifyFailure } from 'data/notify'

import UserService from 'services/UserService'
import PublicCard from 'components/layout/public/PublicCard'
import Transition from 'components/common/presentation/Transition'

export default {
  name: 'ResetPasswordPage',
  components: {
    'app-transition': Transition,
    'app-publiccard': PublicCard
  },
  data: function () {
    return {
      resetpasswordtoken: '',
      showNewPassword: false,
      showNewPassword2: false,
      isLoading: true,
      form: {
        username: '',
        email: '',
        newpassword: '',
        newpassword2: ''
      }
    }
  },
  computed: {
    isFormValid: function () {
      return (this.form.newpassword != null && this.form.newpassword.length !== 0) &&
              (this.form.newpassword2 != null && this.form.newpassword2.length !== 0) &&
              (this.form.newpassword === this.form.newpassword2)
    }
  },
  created: function () {
    if (this.$route.params.resetpasswordtoken != null) {
      this.resetpasswordtoken = this.$route.params.resetpasswordtoken

      const userservice = new UserService()
      userservice.doIsResetPasswordTokenValid(this.resetpasswordtoken).then((response) => {
        this.form.username = response.username
        this.form.email = response.email

        this.isLoading = false
      }).catch((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifyFailure, message: this.$t('resetpasswordpage.error.tokenisvaliderror') })
      })
    } else {
      this.$q.notify({ ...NotifyFailure, message: this.$t('resetpasswordpage.error.tokennotfound') })
      this.$router.push({ name: 'LoginPage' })
    }
  },
  methods: {
    doResetPassword: function () {
      this.isLoading = true

      const userservice = new UserService()
      userservice.doResetPassword(this.resetpasswordtoken, this.form.email, this.form.username, this.form.password).then((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifySuccess, message: this.$t('resetpasswordpage.success.resetpasswordsuccess') })
        this.$router.push({ name: 'LoginPage' })
      }).catch((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifyFailure, message: this.$t('resetpasswordpage.error.resetpasswordfailure') })
      })
    }
  }
}
</script>
