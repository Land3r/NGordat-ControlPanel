/**
 * Backend api base url.
 */
export const baseUrl = process.env.BACKEND_BASE_URL + process.env.BACKEND_API_PREFIX

/**
 * The available API on backend
 */
export const API = {
  USER: 'users',
  GROCERY: 'grocery',
  GROCERYACTION: 'groceryactions',
  GROCERYITEM: 'groceryitems',
  GROCERYQUANTITY: 'groceryquantities',
  GROCERYMEANINGLESSWORD: 'grocerymeaninglesswords',
  SPEECHTOTEXT: 'speechtotext'
}

/**
 * Gets the api endpoint url.
 * @param {API} api The API of which you want the endpoint url.
 */
export function getApiEndpoint (api) {
  return baseUrl + api
}
