import { CBadge } from "@coreui/react"
import moment from "moment/moment"
import numeral from 'numeral'

const Columns = () => {
  return [
    {
      accessor: 'userName',
      Header: 'Tài khoản',
      filterable: true,
    },
    {
      accessor: 'name',
      Header: 'Tên',
      filterable: true,
      minWidth: 100
    },
    {
      accessor: 'email',
      Header: 'Email',
      filterable: true,
    },
    {
      accessor: 'extraProperties.AccountBalance',
      Header: 'Số dư',
      filterable: true,
      Cell: props => {
        return numeral(props.original.extraProperties.AccountBalance).format()
      }
    },
    {
      accessor: 'creationTime',
      Header: 'Ngày đăng ký',
      filterable: true,
      Cell: props => {
        return (
          <div className="text-center">
            {moment(props.original.creationTime).format('DD/MM/YYYY HH:mm')}
          </div>
        )
      }
    },
    {
      accessor: 'isActive',
      Header: 'Trạng Thái',
      filterable: true,
      width: 100,
      Cell: props => {
        return (
          <div className="text-center">
            {props.original.isActive ? <CBadge color="success">Hoạt động</CBadge> : <CBadge color="secondary">Vô hiệu</CBadge>}
          </div>
        )
      }
    }
  ]
}


export default Columns