import { Route } from '@orbit/core'
import { SignIn } from '../components/SignIn.tsx'
import { SignUp } from '../components/SignUp.tsx'
import { ExternalAuthentication } from '../components/ExternalAuthentication.tsx'
import { Dashboard } from '../components/Dashboard.tsx'
import { Settings } from '../components/Settings.tsx'

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