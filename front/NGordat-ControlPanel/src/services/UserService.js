import axios from 'axios'
import { LocalStorage } from 'quasar'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'AUTH': '/auth',
  'FORGOTPASSWORD': '/forgotpassword',
  'RESETPASSWORD': '/resetpassword',
  'REGISTER': '/register',
  'ACTIVATE': '/activate',
  'GET': ''
}

const localStorageKeys = {
  user: 'user',
  token: 'token'
}

/**
 * UserService class.
 */
export default class UserService {
  /**
   * Authenticates the user.
   * @param {string} username The user's username.
   * @param {string} password The user's password.
   */
  doAuthenticate (username, password) {
    const requestOptions = {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.AUTH),
      data: {
        username: username,
        password: password
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Gets the current authenticated user.
   */
  getCurrentUser () {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.GET)
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Disconnects the user.
   */
  disconnect () {
    LocalStorage.remove(localStorageKeys.user)
    LocalStorage.remove(localStorageKeys.token)

    // Removes the token
    axios.defaults.headers.common['Authorization'] = null
  }

  /**
   * Connects the provided user to the application.
   * @param {User} user The user to connect.
   */
  connect (user) {
    const { token, password, ...otherUser } = user
    LocalStorage.set(localStorageKeys.user, { ...otherUser })
    LocalStorage.set(localStorageKeys.token, token)

    // Sets the token in axios calls.
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token

    console.log('Connected as ' + user.username)
  }

  /**
   * Gets whether or not the user is connected.
   */
  isConnected () {
    if (axios.defaults.headers.common['Authorization'] !== undefined && this.getUser() != null && this.getToken() != null) {
      return true
    } else {
      return false
    }
  }

  /**
   * Gets whether or not the user can be reconnected, from the stored data.
   */
  canBeReconnected () {
    if (this.getUser() != null && this.getToken() != null) {
      return true
    } else {
      return false
    }
  }

  /**
   * Gets the stored user.
   */
  getUser () {
    return LocalStorage.getItem(localStorageKeys.user)
  }

  /**
   * Gets the stored JWT Token.
   */
  getToken () {
    return LocalStorage.getItem(localStorageKeys.token)
  }

  /**
   * Asks to reset a user password.
   * @param {string} username The user's username.
   * @param {string} email The user's email.
   */
  doForgotPassword (username, email) {
    const requestOptions = {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.FORGOTPASSWORD),
      data: {
        username: username,
        email: email
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Gets whether or not the resetpasswordtoken is valid, and gets the associated user's information.
   * @param {string} resetpasswordtoken The resetpasswordtoken.
   */
  doIsResetPasswordTokenValid (resetpasswordtoken) {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.RESETPASSWORD + '/' + resetpasswordtoken)
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Reset a user's password
   * @param {string} resetpasswordtoken The reset password token generated by reset password request.
   * @param {string} email The email of the user's account to reset.
   * @param {string} username The username of the user's account to reset.
   * @param {string} password The new password to set to the account.
   */
  doResetPassword (resetpasswordtoken, email, username, password) {
    const requestOptions = {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.RESETPASSWORD),
      data: {
        resetpasswordtoken: resetpasswordtoken,
        email: email,
        username: username,
        password: password
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Register a new user.
   * @param {string} firstname The user's firstname.
   * @param {string} lastname The user's lastname.
   * @param {string} username The user's username.
   * @param {string} email The user's email.
   * @param {string} password The user's password.
   */
  doRegister (firstname, lastname, username, email, password) {
    const requestOptions = {
      method: 'post',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.REGISTER),
      data: {
        firstname: firstname,
        lastname: lastname,
        username: username,
        email: email,
        password: password
      }
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }

  /**
   * Activates an account.
   * @param {string} activationtoken The activation token.
   */
  doActivate (activationtoken) {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.USER + endpoints.ACTIVATE + '/' + activationtoken)
    }

    return axios(requestOptions)
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }
}
