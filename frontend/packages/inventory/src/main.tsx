import React from 'react'
import { createRoot } from 'react-dom/client'
import { App } from './App'
import './index.css'
import { ConfigProvider, theme } from 'antd'
import './config'

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <ConfigProvider
      theme={{algorithm: theme.defaultAlgorithm}}
      direction='ltr'
    >
      <App />
    </ConfigProvider>
  </React.StrictMode>,
)
