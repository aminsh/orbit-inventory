import { SearchOutlined } from '@ant-design/icons'
import { Input, Space, Button } from 'antd'
import { FC, useState } from 'react'
import { useTranslate } from '../../hook'

export const FilterDropdown: FC<{
  onSearch: (value?: string) => void
  onClear: () => void
}> = ({ onSearch, onClear }) => {
  const [searchText, setSearchText] = useState<string>()
  const t = useTranslate()

  return (
    <>
      <Input
        value={searchText}
        onChange={e => setSearchText(e.target.value)}
        placeholder={t('type_something')}
        onPressEnter={() => onSearch(searchText)}
      />
      <Space className='mt-2'>
        <Button
          onClick={() => onSearch(searchText)}
          type='primary'
          icon={<SearchOutlined />}
        >
          {t('search')}
        </Button>
        <Button
          onClick={onClear}
        >
          {t('clear_filters')}
        </Button>
      </Space>
    </>
  )
}