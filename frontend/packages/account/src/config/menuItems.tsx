import { Link, MenuItem } from '@orbit/core'
import { UserOutlined } from '@ant-design/icons'

export const menuItems: MenuItem[] = [
  {
    key: 'profile',
    label: <Link to='/profile' label='profile'/>,
    icon: <UserOutlined />,
  },
]