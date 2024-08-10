import { SharedContext } from '@orbit/core'
import { AppRoutes } from './AppRoutes'
import './config/sharedConfig'
import { useAppSelector } from './store'
import sharedConfig from './config/sharedConfig'

export const App = () => {
  const { user } = useAppSelector(s => s.authenticatedUserState)

  return (
    <SharedContext.Provider value={{
      ...sharedConfig,
      authenticatedUser: user,
    }}>
      <AppRoutes />
    </SharedContext.Provider>
  )
}