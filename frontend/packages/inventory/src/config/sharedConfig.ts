import type { Configuration } from '@orbit/core'
import { dictionaryEn } from './dictionary.en.ts'

export default <Configuration>{
  baseUrl: import.meta.env.VITE_BASE_URL,
  dictionaries: {
    en: dictionaryEn,
  },
  defaultDictionary: 'en',
  authUrl: import.meta.env.VITE_AUTH_URL,
  authenticatedUser: null,
}