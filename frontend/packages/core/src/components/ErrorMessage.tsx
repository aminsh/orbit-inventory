import { Alert } from 'antd'

export type ErrorMessageProps =  {
  title: string
  message: string | string[]
  onClose?: () => void
}

export const ErrorMessage = ({ message, title, onClose }: ErrorMessageProps) => {
  const canShow = (): boolean => {
    if (!message)
      return false

    return !(Array.isArray(message) && message.length === 0)
  }

  return (
    <>
      {
        canShow() &&
        <Alert
          className='mb-2'
          message={ title }
          description={
            Array.isArray(message) ?
              message.length > 1 ?
                <ul>
                  { message.map(msg => <li>{ msg }</li>) }
                </ul>
                : message[0]
              : message
          }
          type="error"
          showIcon
          closable
          onClose={ onClose }
        />
      }
    </>
  )
}

