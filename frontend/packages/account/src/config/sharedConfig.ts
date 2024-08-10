import { Configuration } from '@orbit/core'
import { dictionaryEn } from './dictionary.en'

export default <Configuration> {
  baseUrl: import.meta.env.VITE_BASE_URL,
  dictionaries: {
    en: dictionaryEn
  },
  defaultDictionary: 'en',
  authenticatedUser: null,
}
