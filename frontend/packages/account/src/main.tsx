import React from 'react'
import { createRoot } from 'react-dom/client'
import { App } from './App'
import './index.css'
import { ConfigProvider, theme } from 'antd'
import './output.css'

console.log(import.meta.env.VITE_BASE_URL)
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
