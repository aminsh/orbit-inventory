import { ID, PageableResponse } from '@orbit/core'
import { createApi } from '@reduxjs/toolkit/query/react'
import { apiBaseQuery } from '../apiBaseQuery'
import { Product, ProductDto, ProductFindRequest } from '../../../type/product'

export const productApi = createApi({
  reducerPath: 'productApi',
  baseQuery: apiBaseQuery({ baseUrl: 'v1/products' }),
  endpoints: builder => ({
    fetchProducts: builder.query<PageableResponse<Product>, ProductFindRequest>({
      query: args => ({
        url: '',
        params: args,
      }),
    }),
    fetchProduct: builder.query<Product, ID>({
      query: ({ id }) => ({
        url: id.toString(),
      })
    }),
    createProduct: builder.mutation<void, ProductDto>({
      query: dto => ({
        url: '',
        method: 'POST',
        body: dto,
      })
    }),
    updateProduct: builder.mutation<void, ID & { dto: ProductDto }>({
      query: ({ id, dto }) => ({
        url: id.toString(),
        method: 'PUT',
        body: dto,
      })
    }),
    deleteProduct: builder.mutation<void, ID>({
      query: ({ id }) => ({
        url: id.toString(),
        method: 'DELETE',
      })
    })
  }),
})

export const {
  endpoints,
  useFetchProductsQuery,
  useFetchProductQuery,
  useCreateProductMutation,
  useUpdateProductMutation,
  useDeleteProductMutation,
} = productApi