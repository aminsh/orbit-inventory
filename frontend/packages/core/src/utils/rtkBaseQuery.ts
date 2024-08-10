import { fetchBaseQuery, FetchBaseQueryArgs } from '@reduxjs/toolkit/query/react'
import { memory } from '../service'
import { Token } from '../type'
import { AUTHENTICATION_TOKEN } from '../constant'

export type RtkBaseQueryArgs = {
  baseUrl: string
}

export const rtkBaseQuery = ({ baseUrl }: RtkBaseQueryArgs) => {
  const token = memory.get<Token>(AUTHENTICATION_TOKEN)

  return (args?: FetchBaseQueryArgs) => {
    const finalArgs: FetchBaseQueryArgs = {
      prepareHeaders: headers => {
        headers.set('Content-Type', 'application/json')
        headers.set('Authorization', `${token?.tokenType} ${token?.accessToken}`)
        return headers
      },
      ...args,
      baseUrl: [
        baseUrl,
        args?.baseUrl,
      ]
        .filter(Boolean)
        .join('/'),
    }

    return fetchBaseQuery(finalArgs)
  }
}
