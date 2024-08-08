import { configuration } from './configure.ts'

export const translate = (...keys: string[]) => {
  return keys.map(resolve).join(' ')
}

export const setDefaultDictionary = (name: string) => {
  configuration.defaultDictionary = name
}

const resolve = (key: string) => {
  const value = configuration.dictionaries[configuration.defaultDictionary][key]
  return value || key
}
