import { AuthenticatedUser } from '@orbit/core'
import { createApi } from '@reduxjs/toolkit/query/react'
import { setAuthenticatedUser } from './authenticatedUserSlice'
import { apiBaseQuery } from '../apiBaseQuery'
import { SignInDto, SignInResponse, SignUpDto, UserProfileDto } from 'src/type/user.type'

export const authenticatedUserApi =  createApi({
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
    signIn: builder.mutation<SignInResponse, SignInDto>({
      query: dto => ({
        url: 'signIn',
        method: 'POST',
        body: dto,
      })
    }),
    signUp: builder.mutation<void, SignUpDto>({
      query: dto => ({
        url: 'signUp',
        method: 'POST',
        body: dto,
      })
    }),
    updateProfile: builder.mutation<void, UserProfileDto>({
      query: dto => ({
        url: 'profile',
        method: 'PUT',
        body: dto,
      }),
    })
  }),
})

export const {
  useSignInMutation,
  useSignUpMutation,
  useFetchUserQuery,
  useLazyFetchUserQuery,
  useUpdateProfileMutation,
} = authenticatedUserApi