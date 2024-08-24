import { Nullable, PageableRequest } from '@orbit/core'

export type Product = {
  id: number
  name: string
  upc: string
}

export type ProductDto = Omit<Product, 'id'>

export type ProductFindRequest = PageableRequest & Partial<{
  name?: string
  upc?: Nullable<string>
}>