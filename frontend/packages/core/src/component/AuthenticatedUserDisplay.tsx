import { FC, useContext } from 'react'
import { Avatar, Tooltip } from 'antd'
import { Configuration } from '../type'
import { SharedContext } from './SharedContext.tsx'

export const AuthenticatedUserDisplay: FC = () => {
  const {authenticatedUser} = useContext<Configuration>(SharedContext)

  return (
    <Tooltip title={authenticatedUser?.name}>
      <Avatar style={{backgroundColor: '#8795de'}}>
        {authenticatedUser?.name.charAt(0)?.toUpperCase()}
      </Avatar>
    </Tooltip>
  )
}