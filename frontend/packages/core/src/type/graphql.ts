import { TypedDocumentNode } from '@apollo/client'

export type GQLPageableResponse<TView, TKey extends string> = {
  [key in TKey]: {
    data: TView[]
    count: number
  }
}

export type GQLResponse<TView, TKey extends string> = {
  [key in TKey]: TView
}

export type GQLPageableRequest<TExtraParameters = object> = {
  request: {
    take: number
    skip: number
  } & TExtraParameters
}

type KeyValue = {
  [key: string]: string | number | boolean | Date
}
export type GQLResponseDocument<TView, TKey extends string, TVariables = KeyValue> = TypedDocumentNode<GQLResponse<TView, TKey>, TVariables>
export type GQLPageableResponseDocument<TView, TKey extends string, TExtraRequest = KeyValue> = TypedDocumentNode<GQLPageableResponse<TView, TKey>, GQLPageableRequest<TExtraRequest>>
