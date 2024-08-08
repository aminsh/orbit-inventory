import { MainLayout, useAuth } from '@orbit/core'
import { menuItems } from '../config/menuItems'
import { useEffect } from 'react'

export const PrivateLayout = () => {
  const {check} = useAuth()

  useEffect(() => {
    check()
  }, [])

  return (
    <MainLayout
      menuItems={menuItems}
    />
  )
}