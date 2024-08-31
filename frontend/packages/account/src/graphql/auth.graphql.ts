import { gql } from '@apollo/client'
import { GQLInput, GQLResponseDocument } from '@orbit/core'
import { SignInResponse, SignInDto, SignUpDto } from 'src/type/user.type'

export const SignInMutationDocument: GQLResponseDocument<SignInResponse, 'signIn', GQLInput<SignInDto>> = gql`
  mutation SignInMutation ($input: SignInInput) {
    signIn(input: $input) {
      tokenType
      accessToken
    }
  }
`

export const SignUpMutationDocument: GQLResponseDocument<void, '', GQLInput<SignUpDto>> = gql`
  mutation SignUpMutation($input: SignUpInput) {
    signUp(input: $input)
  }
`