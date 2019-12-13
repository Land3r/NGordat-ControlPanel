import axios from 'axios'

import { handleResponse, handleError } from 'services/ServiceHelper'
import { API, getApiEndpoint } from 'data/backend'

const endpoints = {
  'UPLOAD': '/upload'
}

/**
 * SpeechToTextService class.
 */
export default class SpeechToTextService {
  /**
   * Upload a sound blob.
   * @param {bytes} blob The blob to upload.
   */
  doUpload (blob) {
    let formData = new FormData()
    formData.append('blob', blob)

    return axios.post(
      getApiEndpoint(API.SPEECHTOTEXT + endpoints.UPLOAD),
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      })
      .then(function (response) {
        return handleResponse(response)
      })
      .catch(function (error) {
        return handleError(error)
      })
  }
}
