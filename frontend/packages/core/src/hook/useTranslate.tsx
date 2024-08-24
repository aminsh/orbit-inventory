import { useContext } from 'react'
import { coreDictionaries } from '../config'
import { SharedContext } from '../context'

export const useTranslate = () => {
  const {dictionaries, defaultDictionary} = useContext(SharedContext)
  const mixedDictionary = {
    ...coreDictionaries[defaultDictionary],
    ...dictionaries[defaultDictionary],
  }

  const find = (key: string) => {
    const value = mixedDictionary[key]
    return value || key
  }

  return (...keys: string[]) => keys.map(find).join(' ')
}