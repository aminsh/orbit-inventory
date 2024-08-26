/// <reference types="vite/client" />
interface ImportMetaEnv {
  readonly VITE_BASE_URL: string
  readonly VITE_AUTH_URL: string
  readonly VITE_GRAPHQL_URL: string
  readonly VITE_GRAPHQL_WS_URL: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}