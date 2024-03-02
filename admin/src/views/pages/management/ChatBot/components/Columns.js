import { CBadge } from "@coreui/react"
import BotAvatarMini from "./BotAvatar"

const Columns = () => {
  return [
    {
      accessor: 'name',
      Header: 'Tên',
      filterable: true,
      Cell: props => {
        return (
          <div className="d-flex align-items-center">
            <BotAvatarMini _id={props.original._id} w={50} h={50} className="me-2" />
            <span>{props.original.name}</span>
          </div>
        )
      }
    },
    {
      accessor: 'status',
      Header: 'Trạng Thái',
      filterable: true,
      Cell: props => {
        return (
          props.original.status === 'active' ? <CBadge color="success">Hoạt động</CBadge> : <CBadge color="secondary">Vô hiệu</CBadge>
        )
      }
    },
    {
      accessor: 'level',
      Header: 'Level',
      filterable: true,
    }
  ]
}


export default Columns