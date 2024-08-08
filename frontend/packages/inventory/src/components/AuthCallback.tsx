import { useEffect } from 'react'
import { Spin } from 'antd'
import { useAuth } from '@orbit/core'

export const AuthCallback = () => {
  const {callback} = useAuth()

  useEffect(() => {
    callback()
  }, [])

  return (
    <Spin
      spinning={true}
      fullscreen
    />
  )
}