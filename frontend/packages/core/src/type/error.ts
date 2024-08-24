import { HttpStatus } from './http'

export type BadRequestError = {
  status: HttpStatus
  data: { field?: string, message: string }[]
}