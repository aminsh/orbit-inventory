import React from 'react'
import { createRoot } from 'react-dom/client'
import { App } from './App'
import './index.css'
import { ConfigProvider, theme } from 'antd'
import './output.css'
import { Provider } from 'react-redux'
import { store } from './store'
import './output.css'

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <ConfigProvider
        theme={{ algorithm: theme.defaultAlgorithm }}
        direction='ltr'
      >
        <App />
      </ConfigProvider>
    </Provider>
  </React.StrictMode>,
)
