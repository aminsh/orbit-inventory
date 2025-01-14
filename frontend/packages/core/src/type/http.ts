import { Nullable } from './common'

export enum HttpStatus {
  Success = 200,
  NotFound = 404,
  BadRequest = 400,
  Unauthorized = 401,
  Unknown = 500,
}

export type HttpRequest<TBody = object> = {
  url: string
  method: 'GET' | 'PUT' | 'POST' | 'DELETE',
  body: TBody,
}

export type HttpResponse<TResponse = object> = {
  status: HttpStatus,
  response: Nullable<TResponse>,
  error: Nullable<string | string[]>
}

export type PageableRequest = {
  take: number
  skip: number
}

export type PageableResponse<TData> = {
  count: number
  data: TData[]
}
