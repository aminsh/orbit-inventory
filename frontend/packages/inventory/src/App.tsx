import { SharedContext } from '@orbit/core'
import { AppRoutes } from './AppRoutes'
import sharedConfig from './config/sharedConfig'
import { useAppSelector } from './store'

export const App = () => {
  const {user} = useAppSelector(s => s.authenticatedUserState)

  return (
    <SharedContext.Provider value={{
      ...sharedConfig,
      authenticatedUser: user
    }}>
      <AppRoutes/>
    </SharedContext.Provider>
  )
}
  
  