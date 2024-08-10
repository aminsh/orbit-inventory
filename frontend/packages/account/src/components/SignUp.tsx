import { Button, Form, Input, Space } from 'antd'
import React, { useState } from 'react'
import { NavLink } from 'react-router-dom'
import { useTranslate } from '@orbit/core'
import logo from '../asset/orbit.svg'
import classNames from 'classnames'
import { SignUpDto } from '../type/user.type'

export const SignUp = () => {
  const [loading] = useState<boolean>(false)
  const [form] = Form.useForm<SignUpDto>()
  const t = useTranslate()

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
              {t('sign_up_title')}
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
              message: t('name', 'is_required')
            }
          ]}
        >
          <Input
            className='w-full'
            size='large'
            placeholder={t('name')}
          />
        </Form.Item>

        <Form.Item
          className='mt-2'
          name='email'
          rules={[
            {
              required: true,
              message: t('name', 'is_required')
            },
            {
              type: 'email',
              message: t('email', 'invalid_message')
            }
          ]}
        >
          <Input
            className='w-full'
            size='large'
            placeholder={t('email')}
          />
        </Form.Item>

        <Form.Item
          name='password'
          rules={[
            {
              required: true,
              message: t('password', 'is_required')
            }
          ]}
        >
          <Input.Password
            size='large'
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
            {t('sign_up')}
          </Button>

          <p className="mt-10 text-center text-sm text-gray-500">
            {t('you_are_a_member', '?')}
            <NavLink to='/signIn' className='text-indigo-500 ml-2 font-bold'>
              {t('sign_in')}
            </NavLink>
          </p>

        </Form.Item>
      </Form>
    </>
  )
}
