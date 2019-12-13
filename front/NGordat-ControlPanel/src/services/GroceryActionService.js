import axios from 'axios'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'GET': '/'
}

/**
 * GroceryActionService class.
 */
export default class GroceryActionService {
  /**
   * Gets all GroceryActions
   */
  doGet () {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.GROCERYACTION + endpoints.GET)
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
