import { Configuration, Token } from './type'

export const configure = (config: Configuration): void => {
  configuration = config
}

export const setToken = (token: Token): void => {
  configuration.token = token
}

export let configuration: Configuration



