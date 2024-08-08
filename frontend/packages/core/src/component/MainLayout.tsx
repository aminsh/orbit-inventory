import { Outlet } from 'react-router-dom'
import { Avatar, Button, Flex, Layout, Menu, theme } from 'antd'
import { MenuOutlined } from '@ant-design/icons'
import { MenuItem } from '../type'
import Logo from '../asset/orbit.svg'
import { CSSProperties, useState, FC } from 'react'

export type MainLayoutProps = {
  menuItems: MenuItem[]
}

const headerStyle: CSSProperties = {
  padding: '0 20px',
  height: '6%',
  borderBlockEnd: '1px solid rgba(5, 5, 5, 0.06)'
}

const {Header, Sider, Content} = Layout

export const MainLayout: FC<MainLayoutProps> = ({menuItems}: MainLayoutProps) => {
  const {token} = theme.useToken()
  const [collapsed, setCollapsed] = useState<boolean>(false)
  const toggle = () => setCollapsed(!collapsed)

  return <Layout style={{height: '100vh'}}>
    <Header style={{...headerStyle, backgroundColor: token.colorBgContainer}}>
      <Flex align='center' style={{height: '100%'}}>
        <Flex align='center' justify='start' style={{width: '100%'}}>
          <Flex align='center' justify='start' style={{width: 180}}>
            <Logo
              style={{width: '60px', maxHeight: '70px'}}
            />
          </Flex>

          <Button
            onClick={toggle}
            style={{color: 'blue'}}
            type='text'
            shape='circle'
            icon={<MenuOutlined/>}
          />
        </Flex>
        <Flex align='center' justify='end' style={{width: '100%'}}>
          <Avatar style={{backgroundColor: '#8795de'}}>K</Avatar>
        </Flex>
      </Flex>
    </Header>
    <Layout>
      <Sider width='13%' collapsed={collapsed}>
        <Menu
          style={{height: '100%'}}
          defaultSelectedKeys={['1']}
          defaultOpenKeys={['sub1']}
          mode='inline'
          items={menuItems}
        />
      </Sider>
      <Content>
        <Outlet/>
      </Content>
    </Layout>
  </Layout>
}