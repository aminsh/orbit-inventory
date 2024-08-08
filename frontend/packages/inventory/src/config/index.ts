import { configure } from '@orbit/core'
import { dictionaryEn } from './dictionary.en.ts'

configure({
  baseUrl: import.meta.env.VITE_BASE_URL,
  dictionaries: {
    en: dictionaryEn,
  },
  defaultDictionary: 'en',
  authUrl: import.meta.env.VITE_AUTH_URL,
})