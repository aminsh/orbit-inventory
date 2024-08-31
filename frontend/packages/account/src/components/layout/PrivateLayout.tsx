import { useLocation, useNavigate } from 'react-router-dom'
import { useAuthentication } from '../../hook/useAuthentication'
import { useEffect } from 'react'
import { DEFAULT_PATH, ORIGINAL_PATH } from '../../constant'
import { MainLayout } from '@orbit/core'
import { menuItems } from '../../config/menuItems.tsx'
import { useAppDispatch } from '../../store/index.ts'
import { useQuery } from '@apollo/client'
import { AuthenticatedUserQueryDocument } from '../../graphql/profile.graphql'
import { setAuthenticatedUser } from '../../store/module/user/authenticatedUserSlice'

export const PrivateLayout = () => {
  const { isAuthenticated } = useAuthentication()
  const navigate = useNavigate()
  const location = useLocation()
  const dispatch = useAppDispatch()
  useQuery(AuthenticatedUserQueryDocument, {
    onCompleted: data => {
      dispatch(setAuthenticatedUser(data.authenticatedUser))
    },
    skip: !isAuthenticated()
  })

  const onOpen = async () => {
    if(isAuthenticated())
      return

    let signInPath = '/signIn'

    if (location.pathname !== DEFAULT_PATH)
      signInPath = `${signInPath}/${ORIGINAL_PATH}=${location.pathname}`

    navigate(signInPath)
  }

  useEffect(() => {
    onOpen()
  })

  return (
    <MainLayout menuItems={menuItems} />
  )
}