import { AuthenticatedUser, Token } from './auth'
import { Nullable } from './common'

export type Configuration = {
  dictionaries: Record<string, Record<string, string>>
  defaultDictionary: string
  baseUrl: string
  authUrl?: string
  token?: Token
  authenticatedUser: Nullable<AuthenticatedUser>
}