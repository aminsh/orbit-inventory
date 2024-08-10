import { AuthenticatedUser, memory, Token } from '@orbit/core'
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { setAuthenticatedUser } from './authenticatedUserSlice'

const token = memory.get<Token>('authentication_token')

export default createApi({
  reducerPath: 'authenticatedUserApi',
  baseQuery: fetchBaseQuery({
    
    baseUrl: import.meta.env.VITE_BASE_URL,
    prepareHeaders: headers => {
      headers.set('Content-Type', 'application/json')
      headers.set('Authorization', `${token?.tokenType} ${token?.accessToken}`)
      return headers
    }
  }),
  endpoints: (builder) => ({
    fetchUser: builder.query<AuthenticatedUser, void>({
      query: () => 'v1/me',

      onQueryStarted: async (_, { dispatch, queryFulfilled }) => {
        const { data } = await queryFulfilled
        dispatch(setAuthenticatedUser(data))
      },
    }),
  }),
})