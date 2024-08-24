import type { FC } from 'react'
import { Alert } from 'antd'

export type ErrorMessageProps = {
  title: string
  message: { message: string }[]
  onClose?: () => void
}

export const ErrorMessage: FC<ErrorMessageProps> = ({ message, title, onClose }: ErrorMessageProps) => {
  const canShow = (): boolean => {
    if (!message?.length)
      return false

    return !(Array.isArray(message) && message.length === 0)
  }

  return (
    <>
      {
        canShow() &&
        <Alert
          className='mb-2'
          message={title}
          description={
            [message].flat().length > 1 ?
              <ul>
                {message.map(msg => <li>{msg.message}</li>)}
              </ul>
              : message[0].message
          }
          type="error"
          showIcon
          closable
          onClose={onClose}
        />
      }
    </>
  )
}

