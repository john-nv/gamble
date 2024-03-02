import { CBadge, CButton } from "@coreui/react"

const Columns = () => {
  return [
    {
      accessor: 'name',
      Header: 'Tên',
      filterable: true
    },
    {
      accessor: 'status',
      Header: 'Trạng Thái',
      filterable: true,
      Cell: ({ original }) => {
        return (
          original.status === 'stop' ? <CBadge color="secondary">Dừng</CBadge> : <CBadge color="success">Đang chạy</CBadge>
        )
      }
    },
    {
      accessor: 'roomId',
      Header: 'Room',
      filterable: true,
    }
  ]
}


export default Columns