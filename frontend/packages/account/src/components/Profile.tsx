import { SaveOutlined } from '@ant-design/icons'
import { useTranslate } from '@orbit/core'
import { Card, Form, Input, Button, notification } from 'antd'
import { useEffect } from 'react'
import { useAppSelector } from '../store'
import { UserProfileDto } from '../type/user.type'
import { useUpdateProfileMutation, useLazyFetchUserQuery } from '../store/module/user/authenticatedUserApi'

export const Profile = () => {
  const [form] = Form.useForm<UserProfileDto>()
  const t = useTranslate()
  const { user } = useAppSelector(s => s.authenticatedUserState)
  const [updateProfile, { isLoading }] = useUpdateProfileMutation()
  const [fetchAuthenticatedUser] = useLazyFetchUserQuery()
  const [api, contextHolder] = notification.useNotification()

  useEffect(() => {
    form.setFieldsValue(user ?? {} as UserProfileDto)
  }, [user, form])

  const handleSave = async (dto: UserProfileDto) => {
    await updateProfile(dto)
    await fetchAuthenticatedUser()
    /* notify.success({
      title: t('profile'),
      message: t('profile_success_message'),
    }) */
    api.success({
      message: t('profile'),
      description: t('profile_success_message')
    })
  }

  return (
    <Card className='m-6'>
      <Form
        layout='vertical'
        form={form}
        onFinish={handleSave}
      >
        {contextHolder}
        <Form.Item
          name='name'
          label={t('name')}
          rules={[
            {
              required: true,
              message: t('name', 'require_message'),
            }
          ]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          name='email'
          label={t('email')}
          rules={[
            {
              required: true,
              message: t('email', 'require_message'),
            },
            {
              enum: ['email'],
              message: t('email', 'invalid_message'),

            }
          ]}
        >
          <Input
            type='email'
          />
        </Form.Item>

        <Form.Item>
          <Button
            loading={isLoading}
            type='primary'
            htmlType='submit'
            icon={<SaveOutlined />}
          >
            {t('save')}
          </Button>
        </Form.Item>
      </Form>
    </Card>
  )
}