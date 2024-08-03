import React from 'react'

export type Route = {
  publicRoutes: RouteItem[]
  privateRoutes: RouteItem[]
}

type RouteItem = {
  path: string
  component: React.ReactNode
}