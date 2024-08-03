let _dictionary: Record<string, string> = {}

export const translate = (...keys: string[]) => {
  return keys.map(resolve).join(' ')
}

export const setDictionary =  (dictionary: Record<string, string>) => {
  _dictionary = dictionary
}

const resolve = (key: string) => {
  const value = _dictionary[key]
  return value || key
}
