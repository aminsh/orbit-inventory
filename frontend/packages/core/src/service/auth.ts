import { Nullable, Token } from '../type'

export const isTokenExpired = (token: Nullable<Token>): boolean => {
  if(!token)
    return true
  
  const {accessToken} = token
  const [_, payload]= accessToken.split('.')
  const {exp} = JSON.parse(window.atob(payload))
  const expireDate = new Date(exp * 1000)
  return new Date >= expireDate
}