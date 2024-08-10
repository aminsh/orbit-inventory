import { AuthenticatedUser } from '@orbit/core'
import { createApi } from '@reduxjs/toolkit/query/react'
import { setAuthenticatedUser } from './authenticatedUserSlice'
import { apiBaseQuery } from '../apiBaseQuery'

export default createApi({
  reducerPath: 'authenticatedUserApi',
  baseQuery: apiBaseQuery({baseUrl: 'v1'}),
  endpoints: (builder) => ({
    fetchUser: builder.query<AuthenticatedUser, void>({
      query: () => 'me',

      onQueryStarted: async (_, { dispatch, queryFulfilled }) => {
        const { data } = await queryFulfilled
        dispatch(setAuthenticatedUser(data))
      },
    }),
  }),
})