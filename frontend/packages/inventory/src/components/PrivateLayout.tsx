import { MainLayout, useAuth } from '@orbit/core'
import { menuItems } from '../config/menuItems'
import { useEffect } from 'react'
import authenticatedUser from '../store/module/user/authenticatedUserApi'

export const PrivateLayout = () => {
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