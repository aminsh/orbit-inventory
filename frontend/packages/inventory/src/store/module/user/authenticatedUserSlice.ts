import { AuthenticatedUser, Nullable } from '@orbit/core'
import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type AuthenticatedUserState = { user: Nullable<AuthenticatedUser> }

const initialState: AuthenticatedUserState = {
  user: null,
}

const authenticatedUserSlice = createSlice({
  name: 'authenticatedUserSlice',
  initialState,
  reducers: {
    setAuthenticatedUser: (state: AuthenticatedUserState, action: PayloadAction<AuthenticatedUser>) => {
      state.user = action.payload
    },
  },
})

export const authenticatedUserReducer = authenticatedUserSlice.reducer
export const { setAuthenticatedUser } = authenticatedUserSlice.actions

