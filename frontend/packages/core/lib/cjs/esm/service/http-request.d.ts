import { HttpRequest, HttpResponse } from '../type';
export declare const httpRequest: <TResponse extends object | null, TBody extends object | null>(args: HttpRequest<TBody>) => Promise<HttpResponse<TResponse>>;
