import { gql } from '@apollo/client'
import { AuthenticatedUser, GQLResponseDocument } from '@orbit/core'

export const AuthenticatedUserQueryDocument: GQLResponseDocument<AuthenticatedUser,'authenticatedUser'> = gql`
  query AuthenticatedUserQuery {
    authenticatedUser {
      id,
      email
      name
    }
  }
`