import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { routes } from './config/routes'
import { AuthCallback } from './components/AuthCallback'
import { PrivateLayout } from './components/PrivateLayout'

export const AppRoutes = () => <BrowserRouter>
  <Routes>
    <Route
      path='/callback'
      element={<AuthCallback />}
    />

    <Route element={<PrivateLayout />}>
      {routes.privateRoutes.map((route, index) => (
        <Route
          key={`private-${index}`}
          path={route.path}
          element={route.component}
        >
          {route.children?.map((child, childIndex) => (
            <Route
              key={`private-${index}-${childIndex}`}
              path={child.path}
              element={child.component}
            />
          ))}
        </Route>
      ))}
    </Route>
  </Routes>
</BrowserRouter>