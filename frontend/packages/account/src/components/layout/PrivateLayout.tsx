import { useLocation, useNavigate } from 'react-router-dom'
import { useAuthentication } from '../../hook/useAuthentication'
import { useEffect } from 'react'
import { DEFAULT_PATH, ORIGINAL_PATH } from '../../constant'
import { MainLayout } from '@orbit/core'
import { menuItems } from '../../config/menuItems.tsx'
import {useLazyFetchUserQuery} from '../../store/module/user/authenticatedUserApi'

export const PrivateLayout = () => {
  const { isAuthenticated } = useAuthentication()
  const navigate = useNavigate()
  const location = useLocation()
  const [fetchAuthenticatedUser] = useLazyFetchUserQuery()

  const onOpen = async () => {
    if (isAuthenticated()) {
      await fetchAuthenticatedUser()
      return
    }


    let signInPath = '/signIn'

    if (location.pathname !== DEFAULT_PATH)
      signInPath = `${signInPath}/${ORIGINAL_PATH}=${location.pathname}`

    navigate(signInPath)
  }

  useEffect(() => {
    onOpen()
  }, [])

  return (
    <MainLayout menuItems={menuItems} />
  )
}