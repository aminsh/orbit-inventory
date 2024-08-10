import { useContext } from 'react'
import { SharedContext } from '../component'

export const useTranslate = () => {
  const {dictionaries, defaultDictionary} = useContext(SharedContext)

  const find = (key: string) => {
    const value = dictionaries[defaultDictionary][key]
    return value || key
  }

  return (...keys: string[]) => keys.map(find).join(' ')
}