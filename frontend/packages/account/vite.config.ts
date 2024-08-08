import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr'
import dts from 'vite-plugin-dts'

// https://vitejs.dev/config/
export default defineConfig(({mode}) => {
  const env = loadEnv(mode, process.cwd())

  return {
    plugins: [
      react(),
      svgr({svgrOptions: {icon: true}}),
      dts(),
    ],
    server: {
      port: parseInt(env.VITE_PORT as string),
    }
  }
})