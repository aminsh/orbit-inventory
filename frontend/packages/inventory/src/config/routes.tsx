import { Route } from '../../../core'
import { Dashboard } from '../components/Dashboard.tsx'

export const routes: Route = {
  publicRoutes: [],
  privateRoutes: [
    {path: '/', component: <Dashboard/>},
  ]
}