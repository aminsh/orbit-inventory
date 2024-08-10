import { FC } from 'react'
import { Configuration } from '../../type'
import { SharedContext } from './SharedContext'

export type SharedProviderProps = {
  children: React.ReactNode
  config: Configuration
}

export const SharedProvider: FC<SharedProviderProps> = ({ config, children }: SharedProviderProps) => {
  return (
    <SharedContext.Provider value={config}>
      {children}
    </SharedContext.Provider>
  )
}