import { LockOutlined, UserOutlined } from '@ant-design/icons'
import { useMutation } from '@apollo/client'
import { ErrorMessage, useTranslate } from '@orbit/core'
import { Button, Form, Input, Space } from 'antd'
import classNames from 'classnames'
import { useEffect, useState } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import logo from '../asset/orbit.svg'
import { DEFAULT_PATH, UNAUTHORIZED_ERROR_CODE } from '../constant'
import { SignInMutationDocument } from '../graphql/auth.graphql'
import { useAuthentication } from '../hook/useAuthentication'
import { SignUpDto } from '../type/user.type'

export const SignIn = () => {
  const [form] = Form.useForm<SignUpDto>()
  const [errors, setErrors] = useState<{message: string}[]>([])
  const [signIn, { loading }] = useMutation(SignInMutationDocument, {
    errorPolicy: 'all',
  })

  const { saveToken, isAuthenticated, getUrlWithToken, getCallbackUrl, getOriginalPath } = useAuthentication()
  const navigate = useNavigate()
  const t = useTranslate()

  useEffect(() => {
    onOpen()
  }, [])

  const onOpen = () => {
    if (!isAuthenticated())
      return
    whenAuthenticated()
  }

  const handleSignIn = async (dto: SignUpDto) => {
    setErrors([])

    const { data, errors } = await signIn({
      variables: {
        input: dto,
      },
    })

    if (!errors) {
      data?.signIn && saveToken(data?.signIn)
      return whenAuthenticated()
    }

    if(errors[0].extensions?.code === UNAUTHORIZED_ERROR_CODE)
      setErrors([{message: t('unauthorized_error_message')}])
  }

  const whenAuthenticated = () => {
    const callbackUrl = getCallbackUrl()

    if (callbackUrl)
      return location.href = getUrlWithToken()

    const originalPath = getOriginalPath()

    if (originalPath)
      return navigate(originalPath)

    return navigate(DEFAULT_PATH)
  }

  return (
    <>
      <Space
        direction='vertical'
        className='flex justify-center items-center mb-4'
      >
        <img
          src={logo}
          alt='logo'
          className='h-40 flex justify-center'
        />
        <span
          className={classNames('text-2xl font-bold')}
        >
          {t('sign_in_title')}
        </span>
      </Space>

      <ErrorMessage
        title={t('error_message_title')}
        message={errors}
        onClose={() => setErrors([])}
      />

      <Form
        form={form}
        onFinish={handleSignIn}
      >
        <Form.Item
          className='mt-2'
          name='email'
          rules={[
            {
              required: true,
              message: t('auth_required_error_message', 'email')
            }
          ]}
        >
          <Input
            className='w-full'
            size='large'
            prefix={<UserOutlined className='site-form-item-icon' />}
            placeholder={t('email')}
          />
        </Form.Item>

        <Form.Item
          name='password'
          rules={[{ required: true, message: t('auth_required_error_message', 'password') }]}
        >
          <Input.Password
            size='large'
            prefix={<LockOutlined />}
            type='password'
            placeholder={t('password')}
          />
        </Form.Item>

        <Form.Item className='mt-2'>
          <Button
            loading={loading}
            size='large'
            block
            type='primary'
            htmlType='submit'
            className='flex w-full justify-center'>
            {t('sign_in')}
          </Button>

          <p className="mt-10 text-center text-sm text-gray-500">
            {t('not_a_member', '?')}
            <NavLink to='/signup' className='text-indigo-500 ml-2 font-bold'>
              {t('sign_up', 'now')}
            </NavLink>
          </p>

        </Form.Item>
      </Form>
    </>
  )
}
