import React from 'react'

export type Route = {
  publicRoutes: RouteItem[]
  privateRoutes: RouteItem[]
}

export type RouteItem = {
  path: string
  component: React.ReactNode
  children?: RouteItem[]
}