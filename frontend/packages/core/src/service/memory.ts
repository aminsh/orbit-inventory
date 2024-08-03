const JSON_START = /^\[|^\{(?!\{)/
const JSON_ENDS: any = {
  '[': /]$/,
  '{': /}$/
}

export const memory = {
  set: (key: string, value: unknown) => {
    const correctedValue = typeof value === 'object'
      ? JSON.stringify(value)
      : value

    localStorage.setItem(key, correctedValue as string)
  },
  get: <T>(key: string): T | null => {
    const result = localStorage.getItem(key)

    if (!result)
      return null

    return isJsonLike(result)
      ? JSON.parse(result)
      : result
  },
  remove: (key: string) => {
    localStorage.removeItem(key)
  }
}

const isJsonLike = (value: string) => {
  const jsonStart = value.match(JSON_START)
  return jsonStart && JSON_ENDS[jsonStart[0]].test(value)
}
