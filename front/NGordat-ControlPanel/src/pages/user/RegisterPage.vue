
<template>
  <q-page class="flex flex-center">
    <app-transition>
      <app-publiccard>
        <q-card-section class="bg-primary text-white">
          <div class="text-h6">
            <q-icon
              name="account_circle"
              size="md"
              left
            />{{ $t('registerpage.title') }}
          </div>
        </q-card-section>

        <q-separator />
        <q-card-section>
          <div class="text-center">
            {{ $t('registerpage.text.description') }}
          </div>
          <q-form>
            <q-input
              v-model="form.username"
              color="primary"
              type="text"
              :label="$t('registerpage.form.username')"
              clearable
              clear-icon="close"
              autofocus
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('registerpage.form.username')}),
                val => val.length >= 3 || $t('validationerror.minlength', {field: $t('registerpage.form.username'), length: 3})
              ]"
            >
              <template v-slot:prepend>
                <q-icon name="perm_identity" />
              </template>
            </q-input>
            <q-input
              v-model="form.firstname"
              color="primary"
              type="text"
              :label="$t('registerpage.form.firstname')"
              clearable
              clear-icon="close"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('registerpage.form.firstname')})
              ]"
            />
            <q-input
              v-model="form.lastname"
              color="primary"
              type="text"
              :label="$t('registerpage.form.lastname')"
              clearable
              clear-icon="close"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('registerpage.form.lastname')})
              ]"
            />
            <br>
            <q-input
              v-model="form.email"
              color="primary"
              type="text"
              :label="$t('registerpage.form.email')"
              clearable
              clear-icon="close"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: 'Email'}),
                val => emailValidator.validate(val) || $t('validationerror.validemail'),
              ]"
            >
              <template v-slot:prepend>
                <q-icon name="mail" />
              </template>
            </q-input>
            <q-input
              v-model="form.email2"
              color="primary"
              type="text"
              :label="$t('registerpage.form.email2')"
              clearable
              clear-icon="close"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: 'Email'}),
                val => emailValidator.validate(val) || $t('validationerror.validemail'),
                val => val == form.email || $t('validationerror.mustmatch', {field: 'Email'})
              ]"
            >
              <template v-slot:prepend>
                <q-icon name="mail" />
              </template>
            </q-input>
            <br>
            <q-input
              v-model="form.password"
              color="primary"
              :label="$t('registerpage.form.password')"
              :type="showPassword ? 'text' : 'password'"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('registerpage.form.password')}),
                val => val.length >= 6 || $t('validationerror.minlength', {field: $t('registerpage.form.password'), length: 6})
              ]"
            >
              <template v-slot:append>
                <q-icon
                  :name="showPassword ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="showPassword = !showPassword"
                />
              </template>
            </q-input>
            <q-input
              v-model="form.password2"
              color="primary"
              :label="$t('registerpage.form.password2')"
              :type="showPassword2 ? 'text' : 'password'"
              lazy-rules
              :rules="[
                val => !!val || $t('validationerror.required', {field: $t('registerpage.form.password')}),
                val => val.length >= 6 || $t('validationerror.minlength', {field: $t('registerpage.form.password'), length: 6}),
                val => val == form.password || $t('validationerror.mustmatch', {field: $t('registerpage.form.password')})
              ]"
            >
              <template v-slot:append>
                <q-icon
                  :name="showPassword2 ? 'visibility_off' : 'visibility'"
                  class="cursor-pointer"
                  @click="showPassword2 = !showPassword2"
                />
              </template>
            </q-input>
            <br>
            <q-btn
              class="bg-primary text-white full-width"
              type="submit"
              :loading="isLoading"
              :disable="isLoading || !isFormValid"
              @click="doRegister"
            >
              {{ $t('registerpage.btn.register') }}
            </q-btn>
          </q-form>
          <br>
          <q-btn
            class="bg-secondary text-white full-width"
            :to="{ name: 'LoginPage' }"
          >
            {{ $t('registerpage.btn.cancel') }}
          </q-btn>
        </q-card-section>
      </app-publiccard>
    </app-transition>
  </q-page>
</template>

<script>
import xss from 'xss'
import EmailValidator from 'email-validator'
import { NotifySuccess, NotifyFailure } from 'data/notify'

import UserService from 'services/UserService'
import PublicCard from 'components/layout/public/PublicCard'
import Transition from 'components/common/presentation/Transition'

export default {
  name: 'RegisterPage',
  components: {
    'app-transition': Transition,
    'app-publiccard': PublicCard
  },
  data: function () {
    return {
      emailValidator: EmailValidator,
      isLoading: false,
      showPassword: false,
      showPassword2: false,
      form: {
        username: '',
        firstname: '',
        lastname: '',
        email: '',
        email2: '',
        password: '',
        password2: ''
      }
    }
  },
  computed: {
    isFormValid: function () {
      return this.form.username != null && this.form.username.length !== 0 &&
        this.form.firstname != null && this.form.firstname.length !== 0 &&
        this.form.lastname != null && this.form.lastname.length !== 0 &&
        this.form.email != null && this.form.email.length !== 0 &&
        this.form.email2 != null && this.form.email2.length !== 0 &&
        this.form.email === this.form.email2 &&
        this.form.password != null && this.form.password.length !== 0 &&
        this.form.password2 != null && this.form.password2.length !== 0 &&
        this.form.password === this.form.password2
    }
  },
  mounted: function () {
    // // If the user hits this page and is already loggedin, we should set the token for requests and redirects him to his homepage.
    // const userservice = new UserService()
    // if (userservice.isConnected()) {
    //   const user = userservice.getUser()
    //   const token = userservice.getToken()

    //   userservice.connect({ ...user, token: token, password: null })
    //   this.$router.push({ name: 'IndexPage' })
    // }
  },
  methods: {
    doRegister: function () {
      this.isLoading = true

      const userservice = new UserService()
      userservice.doRegister(this.form.firstname, this.form.lastname, this.form.username, this.form.email, this.form.password).then((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifySuccess, message: this.$t('registerpage.success.registersuccess', { username: xss(response.username) }), html: true })
        this.$router.push({ name: 'LoginPage' })
      }).catch((response) => {
        this.isLoading = false
        this.$q.notify({ ...NotifyFailure, message: this.$t('registerpage.error.registerfailure') })
      })
    }
  }
}
</script>
