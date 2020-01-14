import axios from 'axios'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'GET': '/'
}

/**
 * GroceryMeaninglessWordService class.
 */
export default class GroceryMeaninglessWordService {
  /**
   * Gets all GroceryMeaninglessWords
   */
  doGet () {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.GROCERYMEANINGLESSWORD + endpoints.GET)
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
