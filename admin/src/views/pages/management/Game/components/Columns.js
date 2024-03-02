import { CBadge } from "@coreui/react"

const Columns = () => {
  return [
    {
      accessor: 'name',
      Header: 'Tên',
      filterable: true
    },
    {
      accessor: 'description',
      Header: 'Mô tả',
      filterable: true
    },
    {
      accessor: 'isActive',
      Header: 'Trạng Thái',
      filterable: true,
      Cell: props => {
        return (
          props.original.isActive ? <CBadge color="success">Hoạt động</CBadge> : <CBadge color="secondary">Vô hiệu</CBadge>
        )
      }
    }
  ]
}


export default Columns