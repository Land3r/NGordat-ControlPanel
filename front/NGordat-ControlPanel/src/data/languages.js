/**
 * Available languages on the application.
 */
export const availableLanguages = [
  {
    label: 'English',
    value: 'en-us',
    icon: 'en.png'
  },
  {
    label: 'French',
    value: 'fr',
    icon: 'fr.png'
  }
]

export const defaultLanguage = {
  ...availableLanguages.find(lang => lang.value === 'en-us')
}
