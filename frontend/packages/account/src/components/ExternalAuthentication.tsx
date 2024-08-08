import { LoadingOutlined } from '@ant-design/icons'
import { Spin } from 'antd'
import { useAuthentication } from '../hook/useAuthentication'
import { useNavigate } from 'react-router-dom'
import { useEffect } from 'react'

export const ExternalAuthentication = () => {
  const {isAuthenticated, getCallbackUrl, getUrlWithToken} = useAuthentication()
  const navigate = useNavigate()

  const onOpen = () => {
    const callbackUrl = getCallbackUrl()

    if(isAuthenticated())
      return location.href = getUrlWithToken()

    navigate(`/signIn?callbackUrl=${callbackUrl}`)
  }

  useEffect(() => {
   onOpen()
  }, [])

  return (
    <Spin fullscreen indicator={<LoadingOutlined style={{ fontSize: 70 }} spin />} />
  )
}