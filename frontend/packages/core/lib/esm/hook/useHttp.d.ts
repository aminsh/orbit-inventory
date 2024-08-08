import { HttpRequest, HttpResponse } from '../type';
export type HttpHookResponse<TResponse, TBody> = [
    (args: Partial<HttpRequest<TBody>>) => Promise<HttpResponse<TResponse>>,
    boolean
];
export declare const useHttpRequest: <TResponse extends object, TBody extends object>(args: Partial<HttpRequest<TBody>>) => HttpHookResponse<TResponse, TBody>;
