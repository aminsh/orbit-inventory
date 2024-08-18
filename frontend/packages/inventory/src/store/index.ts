import { configureStore } from '@reduxjs/toolkit'
import authenticatedUserApi from './module/user/authenticatedUserApi'
import { setupListeners } from '@reduxjs/toolkit/query'
import { authenticatedUserReducer } from './module/user/authenticatedUserSlice'
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'

export const store = configureStore({
  reducer: {
    [authenticatedUserApi.reducerPath]: authenticatedUserApi.reducer,
    authenticatedUserState: authenticatedUserReducer,
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware().concat([
      authenticatedUserApi.middleware,
    ]),
})


setupListeners(store.dispatch)

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>()
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector