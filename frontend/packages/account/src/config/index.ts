import { configure } from '@orbit/core'
import { dictionaryEn } from './dictionary.en'

configure({
  baseUrl: import.meta.env.VITE_BASE_URL,
  dictionaries: {
    en: dictionaryEn
  },
  defaultDictionary: 'en',
})
