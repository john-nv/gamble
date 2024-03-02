import { CBadge } from "@coreui/react"
import moment from "moment/moment"

const ticketStatusColor = {
  'Approved': 'success',
  'New': 'info',
  'Rejected': 'danger'
}

const Columns = () => {
  return [
    {
      accessor: 'title',
      Header: 'Tài khoản',
    },
    {
      accessor: 'statusName',
      Header: 'Trạng thái',
      Cell: props => {
        return (
          <div className="text-center">
            <CBadge color={ticketStatusColor[props.original.status]}>{props.original.statusName}</CBadge>
          </div>
        )
      }
    },
    {
      accessor: 'ticketTypeName',
      Header: 'Loại yêu cầu',
      Cell: props => {
        return (
          <div className="text-center">
            {<CBadge color="info">{props.original.ticketTypeName}</CBadge>}
          </div>
        )
      }
    },
    {
      accessor: 'creatorUsername',
      Header: 'TK Người dùng',
    },
    {
      accessor: 'creationTime',
      Header: 'Ngày tạo',
      Cell: props => {
        return (
          <div className="text-center">
            {moment(props.original.creationTime).format('DD/MM/YYYY HH:mm')}
          </div>
        )
      }
    },
    {
      accessor: 'note',
      Header: 'Ghi chú',
    }
  ]
}


export default Columns