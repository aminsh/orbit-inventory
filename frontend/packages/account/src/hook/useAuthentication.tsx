import { isTokenExpired, useMemory } from '@orbit/core'
import { SignInResponse } from '../type/user.type'
import { ACCESS_TOKEN, AUTHENTICATION_TOKEN, CALLBACK_URL, ORIGINAL_PATH, TOKEN_TYPE } from '../constant'
import { useSearchParams } from 'react-router-dom'

export const useAuthentication = () => {
  const memory = useMemory()
  const [searchParams] = useSearchParams()

  const getToken = () => memory.get<SignInResponse>(AUTHENTICATION_TOKEN)
  const getCallbackUrl = () => searchParams.get(CALLBACK_URL) ?? ''
  const getOriginalPath = () => searchParams.get(ORIGINAL_PATH)

  return {
    saveToken: (token: SignInResponse) => memory.set(AUTHENTICATION_TOKEN, token),
    getToken,
    isAuthenticated: () => {
      const token = getToken()
      if(!token)
        return false

      const expired = isTokenExpired(token)

      if(expired)
        return false
      
      return true
    },
    getCallbackUrl,
    getOriginalPath,
    getUrlWithToken: () => {
      const token = getToken()
      const url = new URL(getCallbackUrl())
      url.searchParams.append(ACCESS_TOKEN, token?.accessToken ?? '')
      url.searchParams.append(TOKEN_TYPE, token?.tokenType ?? '')
      return url.toString()
    }
  }
}