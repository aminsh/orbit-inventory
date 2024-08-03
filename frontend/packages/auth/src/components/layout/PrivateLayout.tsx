import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { Layout } from 'antd'
import { useAuthentication } from '../../hook/useAuthentication'
import { useEffect } from 'react'
import { DEFAULT_PATH, ORIGINAL_PATH } from '../../constant'

const {Header, Footer, Sider, Content} = Layout

export const PrivateLayout = () => {
  const {isAuthenticated} = useAuthentication()
  const navigate = useNavigate()
  const location = useLocation()

  const onOpen = () => {
    if(isAuthenticated())
      return

    let signInPath = '/signIn'

    if(location.pathname !== DEFAULT_PATH)
      signInPath = `${signInPath}/${ORIGINAL_PATH}=${location.pathname}`

    navigate(signInPath)
  }

  useEffect(() => {
    onOpen()
  }, [])

  return (
    <Layout>
      <Sider width='25%'>
        Sider
      </Sider>
      <Layout>
        <Header>Header</Header>
        <Content>
          <Outlet/>
        </Content>
        <Footer>Footer</Footer>
      </Layout>
    </Layout>
  )
}