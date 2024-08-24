import { MainLayout, useAuth } from '@orbit/core'
import { authenticatedUserApi } from '../store/module/user/authenticatedUserApi'
import { menuItems } from '../config/menuItems'
import { useEffect } from 'react'

export const PrivateLayout = () => {
  authenticatedUserApi.useFetchUserQuery()
  const { check } = useAuth()
  

  useEffect(() => {
    check()
  }, [])

  return (
    <>
      <MainLayout
        menuItems={menuItems}
      />
    </>
  )
}