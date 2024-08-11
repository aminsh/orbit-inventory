import { notification } from 'antd'

export type NotifyArgs = {
  title?: string
  message?: string
}

export const notify = {
  success: ({ title, message }: NotifyArgs) =>
    notification.success({
      message: title,
      description: message
    }),
  error: ({ title, message }: NotifyArgs) =>
    notification.error({
      message: title,
      description: message
    }),
  info: ({ title, message }: NotifyArgs) =>
    notification.info({
      message: title,
      description: message
    }),
}

