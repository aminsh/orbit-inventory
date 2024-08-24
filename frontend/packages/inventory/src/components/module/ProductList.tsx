import { Button, Card, Space, Table } from 'antd'
import { FC, useEffect, useState } from 'react'
import { useFetchProductsQuery } from '../../store/module/product/productApi'
import { Product, ProductFindRequest } from '../../type/product'
import { EditOutlined, PlusOutlined } from '@ant-design/icons'
import { Outlet, useLocation, useNavigate } from 'react-router-dom'
import { getTextSearchProps, useTranslate } from '@orbit/core'

export const ProductList: FC = () => {
  const navigate = useNavigate()
  const location = useLocation()
  const t = useTranslate()
  const [currentPage, setCurrentPage] = useState<number>(0)
  const [pageSize, setPageSize] = useState<number>(10)
  const [findRequest, setFindRequest] = useState<ProductFindRequest>()

  const { data: response, isLoading, refetch } = useFetchProductsQuery({
    take: pageSize,
    skip: currentPage * pageSize,
    ...findRequest,
  })

  useEffect(() => {
    setTimeout(refetch, 500)
  }, [location, refetch])

  const handleSearch = (findRequestIndex: keyof ProductFindRequest, value?: string) => {
    setFindRequest({
      ...findRequest ?? {} as ProductFindRequest,
      [findRequestIndex]: value,
    })
  }

  return (
    <>
      <Card className='m-3'>
        <Space className='mb-3'>
          <Button
            icon={<PlusOutlined />}
            onClick={() => navigate('new')}
          >
            {t('add')}
          </Button>
        </Space>

        <Table<Product>
          loading={isLoading}
          dataSource={response?.data}
          columns={[
            {
              title: 'Upc',
              dataIndex: 'upc',
              key: 'upc',
              width: 200,
              ...getTextSearchProps({
                dataIndex: 'upc',
                onSearch: value => handleSearch('upc', value),
              })
            },
            {
              title: 'Name',
              dataIndex: 'name',
              key: 'name',
              ...getTextSearchProps({
                dataIndex: 'name',
                onSearch: value => handleSearch('name', value),
              })
            },
            {
              title: '',
              key: 'actions',
              width: 200,
              render: (_, record) => (
                <Space>
                  <Button
                    shape='circle'
                    type='text'
                    icon={<EditOutlined />}
                    onClick={() => navigate(`${record.id}/edit`)}
                  />
                </Space>
              )
            }
          ]}
          onChange={e => {
            if (e.current)
              setCurrentPage(e.current - 1)

            if (e.pageSize)
              setPageSize(e.pageSize)
          }}
          pagination={{ total: response?.count, position: ['bottomCenter'] }}
          size='middle'
        />
      </Card>

      <Outlet />
    </>
  )
}