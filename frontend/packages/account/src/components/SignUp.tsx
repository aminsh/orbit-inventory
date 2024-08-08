import { Button, Form, Input, Space } from 'antd'
import React, { useState } from 'react'
import { NavLink } from 'react-router-dom'
import { translate } from '@orbit/core'
import logo from '../asset/orbit.svg'
import classNames from 'classnames'
import { SignUpDto } from '../type/user.type'

export const SignUp = () => {
  const [loading] = useState<boolean>(false)
  const [form] = Form.useForm<SignUpDto>()

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
        <span className={classNames('text-2xl font-bold')}>
              {translate('sign_up_title')}
        </span>
      </Space>
      <Form
        form={form}
      >
        <Form.Item
          className='mt-2'
          name='name'
          rules={[
            {
              required: true,
              message: translate('name', 'is_required')
            }
          ]}
        >
          <Input
            className='w-full'
            size='large'
            placeholder={translate('name')}
          />
        </Form.Item>

        <Form.Item
          className='mt-2'
          name='email'
          rules={[
            {
              required: true,
              message: translate('name', 'is_required')
            },
            {
              type: 'email',
              message: translate('email', 'invalid_message')
            }
          ]}
        >
          <Input
            className='w-full'
            size='large'
            placeholder={translate('email')}
          />
        </Form.Item>

        <Form.Item
          name='password'
          rules={[
            {
              required: true,
              message: translate('password', 'is_required')
            }
          ]}
        >
          <Input.Password
            size='large'
            type='password'
            placeholder={translate('password')}
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
            {translate('sign_up')}
          </Button>

          <p className="mt-10 text-center text-sm text-gray-500">
            {translate('you_are_a_member', '?')}
            <NavLink to='/signIn' className='text-indigo-500 ml-2 font-bold'>
              {translate('sign_in')}
            </NavLink>
          </p>

        </Form.Item>
      </Form>
    </>
  )
}
