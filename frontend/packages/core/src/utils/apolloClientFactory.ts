import { ApolloClient, ApolloLink, createHttpLink, InMemoryCache, split } from '@apollo/client'
import { setContext } from '@apollo/client/link/context'
import { Token } from '../type'
import { onError } from '@apollo/client/link/error'
import { GraphQLWsLink } from '@apollo/client/link/subscriptions'
import { createClient } from 'graphql-ws'
import { getMainDefinition } from '@apollo/client/utilities'
import { memory } from '../service'
import { AUTHENTICATION_TOKEN } from '../constant.ts'

const getToken = () => {
  const token = memory.get<Token>(AUTHENTICATION_TOKEN)
  return token ? `${token.tokenType} ${token.accessToken}` : ''
}

export type CreateApolloClientArgs = {
  GraphqlURL: string
  GraphQLWsURL: string
}

export const createApolloClient = ({GraphqlURL, GraphQLWsURL}: CreateApolloClientArgs) => {
  const httpLink = createHttpLink({
    uri: GraphqlURL
  })

  const authLink = setContext((_, {headers}) => ({
    headers: {
      ...headers,
      authorization: getToken()
    }
  }))

  const errorLink = onError(({graphQLErrors, operation, forward,networkError}) => {
    if (graphQLErrors)
      graphQLErrors.forEach(({message}) => {
        if (message === 'Unauthorized') {
          memory.remove(AUTHENTICATION_TOKEN)
          /* eslint-disable */
          location.reload()
        }
      })
  })

  const wsLink = new GraphQLWsLink(createClient({
    url: GraphQLWsURL,
  }))

  const splitLink = split(
    ({query}) => {
      const definition = getMainDefinition(query);
      return (
        definition.kind === 'OperationDefinition' &&
        definition.operation === 'subscription'
      );
    },
    wsLink,
    ApolloLink.from([authLink, errorLink, httpLink])
  )

  return new ApolloClient({
    link: splitLink,
    cache: new InMemoryCache(),
  })
} 


