import { gql } from '@apollo/client'
import { GQLPageableResponseDocument, GQLResponseDocument, ID } from '@orbit/core'
import { Product, ProductFindRequest } from '../../../type/product.ts'

const ProductFieldsFragment = gql`
    fragment productFields on Product {
        id
        name
        upc
    }
`

export const ProductFindByIdQuery: GQLResponseDocument<Product, 'productFindById', ID> = gql`
    ${ProductFieldsFragment}
    
    query ProductFindById($id: ID!) {
        productFindById(id: $id) {
            ...productFields
        }
    }
`

export const ProductFindQuery: GQLPageableResponseDocument<Product, 'productFind', ProductFindRequest> = gql`
    ${ProductFieldsFragment}

    query ProductFind($request: ProductFind!) {
        productFind(request: $request) {
            data {
                ...productFields
            }
            count
        }
    }
`
