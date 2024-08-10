import { FC, useEffect, useState } from 'react'
import { Avatar, Tooltip } from 'antd'
import { AuthenticatedUser } from '../type'
import { useHttpRequest } from '../hook/useHttp.tsx'

export const AuthenticatedUserDisplay: FC = () => {
  const [currentUser, setCurrentUser] = useState<AuthenticatedUser>()
  const [execute] = useHttpRequest<AuthenticatedUser, null>({
    url: 'me',
    method:'GET',
  })

  const fetch = async () => {
    const user = await execute()
    setCurrentUser(user.response ?? undefined)
  }

  useEffect(() => {
    fetch()
  }, [])

  return (
    <Tooltip title={currentUser?.name}>
      <Avatar style={{backgroundColor: '#8795de'}}>
        {currentUser?.name.charAt(0)?.toUpperCase()}
      </Avatar>
    </Tooltip>
  )
}