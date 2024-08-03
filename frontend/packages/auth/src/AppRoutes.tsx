import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { PublicLayout } from './components/layout/PublicLayout'
import { routes } from './config/routes'
import { PrivateLayout } from './components/layout/PrivateLayout'

export const AppRoutes = () => (
  <BrowserRouter>
    <Routes>
      <Route element={<PublicLayout/>}>
        {routes.publicRoutes.map((route, index) => (
          <Route
            key={`public-${index}`}
            path={route.path}
            element={route.component}
          />
        ))}
      </Route>

      <Route element={<PrivateLayout/>}>
        {routes.privateRoutes.map((route, index) => (
          <Route
            key={`private-${index}`}
            path={route.path}
            element={route.component}
          />
        ))}
      </Route>
    </Routes>
  </BrowserRouter>
)