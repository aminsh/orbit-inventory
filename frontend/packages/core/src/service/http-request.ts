import { HttpRequest, HttpResponse, HttpStatus } from '../type'

export const httpRequest = async <TResponse extends object | null, TBody extends object | null>(args: HttpRequest<TBody>)
  : Promise<HttpResponse<TResponse>> => {
  const result = await fetch(`${process.env.REACT_APP_API_BASE_URL}/v1/${args.url}`, {
    headers: {
      'Content-Type': 'application/json',
    },
    method: args.method,
    body: JSON.stringify(args.body),
  })

  const response: HttpResponse<TResponse> = {
    status: result.status,
    response: null,
    error: null,
  }

  try {
    response[response.status === HttpStatus.Success ? 'response' : 'error'] = await result.json()
    return response
  } catch (e) {
    return response
  }
}
