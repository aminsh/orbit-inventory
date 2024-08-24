import { TableColumnType } from 'antd'
import { FilterDropdown } from './FilterDropdown'
import { FilterOutlined } from '@ant-design/icons'

export type GetTextSearchPropsArgs<T> = {
  onSearch: (value?: string) => void
  dataIndex: keyof T
}
export const getTextSearchProps = <T extends object>({dataIndex, onSearch}: GetTextSearchPropsArgs<T>): TableColumnType<T> => ({
  filterDropdown: ({ confirm, clearFilters, setSelectedKeys }) => (
    <div
      className='p-3'
      onKeyDown={e => e.stopPropagation()}
    >
      <FilterDropdown
        onSearch={value => {
          setSelectedKeys([value ?? ''])
          onSearch(value)
          confirm({ closeDropdown: true })
        }}
        onClear={() => {
          clearFilters && clearFilters()
          onSearch(undefined)
          setSelectedKeys([])
          confirm({ closeDropdown: true })
        }}
      />
    </div>
  ),
  filterIcon: (filtered: boolean) => (
    <FilterOutlined style={{ color: filtered ? '#1677ff' : undefined }} />
  ),
})

