import { useMemory } from './useMemory'
import {
  ACCESS_TOKEN,
  AUTHENTICATION_TOKEN,
  CALLBACK,
  CALLBACK_URL,
  DEFAULT_PATH,
  STATE,
  TOKEN_TYPE
} from '../constant'
import { Token } from '../type'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { isTokenExpired } from '../service'
import { useContext } from 'react'
import { SharedContext } from '../context'

export const useAuth = () => {
  const memory = useMemory()
  const [searchParams] = useSearchParams()
  const navigate = useNavigate()
  const { authUrl } = useContext(SharedContext)

  const getToken = () => memory.get<Token>(AUTHENTICATION_TOKEN)
  const saveToken = (token: Token) => memory.set(AUTHENTICATION_TOKEN, token)

  return {
    saveToken: (token: Token) => memory.set(AUTHENTICATION_TOKEN, token),
    check: () => {
      const token = getToken()

      if (token && !isTokenExpired(token))
        return

      const url = new URL(authUrl as string)
      const state = {
        originalPath: location.pathname,
      }

      url.searchParams.append(CALLBACK_URL, `${location.origin}/${CALLBACK}`)
      url.searchParams.append(STATE, encodeURIComponent(JSON.stringify(state)))
      location.href = url.toString()
    },
    callback: () => {
      const accessToken = searchParams.get(ACCESS_TOKEN)
      const tokenType = searchParams.get(TOKEN_TYPE)

      if (!accessToken)
        return

      if (!tokenType)
        return

      const token: Token = {
        accessToken,
        tokenType,
      }

      saveToken(token)

      const encodedState = searchParams.get(STATE)
      const state = encodedState
        ? JSON.parse(decodeURIComponent(encodedState))
        : {}

      const destinationPath = state.originalPath ?? DEFAULT_PATH
      navigate(destinationPath)
    },
  }
}
