export type SignInDto = {
  email: string
  password: string
}

export type SignUpDto = {
  name: string
  email: string
  password: string
}

export type SignInResponse = {
  accessToken: string
  tokenType: string
}