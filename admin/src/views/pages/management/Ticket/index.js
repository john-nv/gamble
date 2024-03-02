import AppCrud from "@components/controls/AppCrud"
import { cilInfo } from "@coreui/icons"
import CIcon from "@coreui/icons-react"
import Columns from "./components/Columns"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import Resolver from "./components/Resolver"
import { CButton } from "@coreui/react"
import toast from "@services/toast"
import ticket from "@services/ticket"
import _ from "lodash"

const Ticket = () => {

  const approve = ({ entity, closeMainFormModal }) => {
    toast.confirm({
      icon: 'question',
      title: 'Duyệt yêu cầu?',
      text: 'Bạn có chắc muốn duyệt yêu cầu này',
      onConfirmed: async () => {
        try {
          await ticket.approve(entity.id)
          toast.sucess('Duyệt yêu cầu thành công')
          closeMainFormModal()
        } catch {
          toast.error('Duyệt yêu cầu thất bại')
        }
      }
    })
  }

  const reject = ({ entity, closeMainFormModal }) => {
    toast.confirm({
      icon: 'question',
      title: 'Từ chối yêu cầu?',
      text: 'Bạn có chắc muốn từ chối yêu cầu này',
      onConfirmed: async () => {
        try {
          await ticket.reject(entity.id, { note: 'Từ chối yêu cầu' })
          toast.sucess('Từ chối yêu cầu thành công')
          closeMainFormModal()
        } catch {
          toast.error('Từ chối yêu cầu thất bại')
        }
      }
    })
  }

  return (
    <>
      <AppCrud
        modalOptions={{
          size: 'lg'
        }}
        columns={Columns()}
        resolver={Resolver}
        getAllApi="/api/gamble/ticket"
        getByIdApi="/api/gamble/ticket/:id"
        updateLabel="Chi tiết"
        updateIcon={<CIcon icon={cilInfo} size="lg" className='me-1' />}
        renderFormTitle={entityModel => entityModel?.title}
        renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
        renderFormFooter={({ entity, closeMainFormModal }) => {
          if (!entity) return null
          if (entity.status !== 'New') return null
          return (
            <>
              <div className="me-1">
                <CButton color="success" onClick={() => approve({ entity, closeMainFormModal })}>Duyệt</CButton>
              </div>
              <div className="me-1">
                <CButton color="dark" onClick={() => reject({ entity, closeMainFormModal })}>Từ chối</CButton>
              </div>
            </>
          )
        }}
      />
    </>
  )
}


export default Ticket