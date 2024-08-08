import { HttpRequest, HttpResponse } from '../type'
import { httpRequest } from '../service/http-request'
import { useState } from 'react'

export type HttpHookResponse<TResponse, TBody> = [
  (args: Partial<HttpRequest<TBody>>) => Promise<HttpResponse<TResponse>>,
  boolean
]

export const useHttpRequest = <TResponse extends object, TBody extends object>
(args: Partial<HttpRequest<TBody>>): HttpHookResponse<TResponse, TBody> => {
  const [loading, setLoading] = useState<boolean>(false)

  const execute = async (overrideArgs?: Partial<HttpRequest<TBody>>): Promise<HttpResponse<TResponse>> => {
    setLoading(true)
    const response = await httpRequest<TResponse, TBody>({...args, ...overrideArgs} as HttpRequest<TBody>)
    setLoading(false)
    return response
  }

  return [execute, loading]
}
