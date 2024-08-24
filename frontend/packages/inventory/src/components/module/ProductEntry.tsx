import { BadRequestError, ErrorMessage, notify, useTranslate } from '@orbit/core'
import { Form, Input, Modal } from 'antd'
import { FC, useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { useCreateProductMutation, useFetchProductQuery, useUpdateProductMutation } from '../../store/module/product/productApi'
import { Product, ProductDto } from 'src/type/product'

export const ProductEntry: FC = () => {
  const params = useParams<{ id?: string }>()
  const id = params.id ? Number(params.id) : null
  const navigate = useNavigate()
  const [open, setOpen] = useState<boolean>(true)
  const [form] = Form.useForm<Product>()
  const t = useTranslate()
  const { data: entity } = useFetchProductQuery({ id: id as number }, )
  const [create, { isLoading: creating }] = useCreateProductMutation()
  const [update, { isLoading: updating }] = useUpdateProductMutation()
  const [errors, setErrors] = useState<BadRequestError['data']>([])

  useEffect(() => {
    form.setFieldsValue(entity ?? {})
  }, [entity, form])

  const save = async (data: Product) => {
    const dto: ProductDto = {
      name: data.name,
      upc: data.upc,
    }

    const { error } = id
      ? await update({
        id: Number(id),
        dto,
      })
      : await create(dto)

    if (error)
      return setErrors((error as BadRequestError).data)

    handleClose()
  }

  const handleClose = () => {
    setOpen(false)
    navigate('..')
  }

  return (
    <Modal
      open={open}
      closable
      maskClosable={false}
      title={t(id ? 'edit_product' : 'add_product')}
      onOk={form.submit}
      onCancel={handleClose}
      okButtonProps={{ loading: creating || updating }}
    >
      <ErrorMessage
        title={t('error_message_title', 'product')}
        message={errors}
      />

      <Form
        className='mt-2'
        layout='vertical'
        form={form}
        onFinish={save}
      >
        <Form.Item
          name='upc'
          label={t('upc')}
        >
          <Input />
        </Form.Item>

        <Form.Item
          name='name'
          label={t('name')}
        >
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  )
}