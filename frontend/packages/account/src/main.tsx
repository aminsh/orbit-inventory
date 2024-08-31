import React from 'react'
import { createRoot } from 'react-dom/client'
import { App } from './App'
import './index.css'
import { ConfigProvider, theme } from 'antd'
import './output.css'
import { Provider } from 'react-redux'
import { store } from './store'
import './output.css'
import { createApolloClient } from '@orbit/core'
import { ApolloProvider } from '@apollo/client'

const apolloClient = createApolloClient({
  GraphqlURL: import.meta.env.VITE_GRAPHQL_URL,
  GraphQLWsURL: import.meta.env.VITE_GRAPHQL_WS_URL,
})

createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <ApolloProvider client={apolloClient}>
    <Provider store={store}>
      <ConfigProvider
        theme={{ algorithm: theme.defaultAlgorithm }}
        direction='ltr'
      >
        <App />
      </ConfigProvider>
    </Provider>
  </ApolloProvider>
  </React.StrictMode >,
)
