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

export type UserProfileDto = {
  name: string
  email: string
}