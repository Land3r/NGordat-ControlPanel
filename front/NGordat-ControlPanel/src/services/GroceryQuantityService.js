import axios from 'axios'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'GET': '/'
}

/**
 * GroceryQuantityService class.
 */
export default class GroceryQuantityService {
  /**
   * Gets all GroceryQuantities
   */
  doGet () {
    const requestOptions = {
      method: 'get',
      headers: { 'Content-Type': 'application/json' },
      url: getApiEndpoint(API.GROCERYQUANTITY + endpoints.GET)
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
