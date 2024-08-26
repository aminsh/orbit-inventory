import { gql } from '@apollo/client'
import { GQLPageableResponse, GQLPageableResponseDocument, GQLResponseDocument, ID } from '@orbit/core'
import { Product, ProductFindRequest } from '../../../type/product.ts'

const ProductFieldsFragment = gql`
    fragment productFields on ProductView {
        id
        name
        upc
    }
`

export const ProductFindByIdQuery: GQLResponseDocument<Product, 'productFindById', ID> = gql`
    ${ProductFieldsFragment}
    
    query ProductFindById($id: Int!) {
        productFindById(id: $id) {
            ...productFields
        }
    }
`

export const ProductFindQuery: GQLPageableResponseDocument<Product, 'productFind', ProductFindRequest> = gql`
    ${ProductFieldsFragment}

    query ProductFind($request: ProductFindRequestInput!) {
        productFind(request: $request) {
            data {
                ...productFields
            }
            count
        }
    }
`
