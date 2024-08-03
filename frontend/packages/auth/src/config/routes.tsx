import { Route } from '@orbit/core'
import { SignIn } from '../components/SignIn'
import { SignUp } from '../components/SignUp'
import { ExternalAuthentication } from '../components/ExternalAuthentication'
import { Dashboard } from '../components/Dashboard'
import { Settings } from '../components/Settings'

export const routes: Route = {
  publicRoutes: [
    {path: '/signIn', component: <SignIn/>},
    {path: '/signUp', component: <SignUp/>},
    {path: '/auth', component: <ExternalAuthentication/>},
  ],
  privateRoutes: [
    {path: '/', component: <Dashboard/>},
    {path: '/profile', component: <Settings/>}
  ]
}