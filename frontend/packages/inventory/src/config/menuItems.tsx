import { ProductOutlined } from '@ant-design/icons'
import { Link, MenuItem } from '@orbit/core'

export const menuItems: MenuItem[] = [
  {
    key: 'products',
    label: <Link to='/products' label='product'></Link>,
    icon: <ProductOutlined style={{fontSize: 18}} />
  },
]