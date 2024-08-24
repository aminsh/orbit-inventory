import { ProductList } from '../components/module/ProductList'
import { Route } from '../../../core'
import { Dashboard } from '../components/Dashboard'
import { ProductEntry } from '../components/module/ProductEntry'

export const routes: Route = {
  publicRoutes: [],
  privateRoutes: [
    { path: '/', component: <Dashboard /> },
    {
      path: 'products',
      component: <ProductList />,
      children: [
        { path: ':id/edit', component: <ProductEntry /> },
        { path: 'new', component: <ProductEntry /> },
      ]
    },
  ]
}