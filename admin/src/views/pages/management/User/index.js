import AppCrud from "@components/controls/AppCrud"
import { CButton } from "@coreui/react"
import { useState } from "react"
import Columns from "./components/Columns"
import FrmCreateOrUpdate from "./components/FrmCreateOrUpdate"
import FrmDeposit from "./components/FrmDeposit"
import FrmResetPassword from "./components/FrmResetPassword"
import Resolver from "./components/Resolver"
import { v4 } from 'uuid'
import FrmBankAccount from "./components/FrmBankAccount"

const User = () => {
  const [resetPasswordShow, setResetPasswordShow] = useState(false)
  const [depositShow, setDepositShow] = useState(false)
  const [bankAccountShow, setBankAccountShow] = useState(false)
  const [entity, setEntity] = useState(null)
  const [key, setKey] = useState(v4())

  return (
    <>
      <AppCrud
        key={key}
        modalOptions={{
          size: 'lg'
        }}
        columns={Columns()}
        resolver={Resolver}
        getAllApi="/api/identity/users"
        getByIdApi="/api/identity/users/:id"
        updateApi="/api/identity/users/:id"
        // createApi="/api/identity/users"
        deleteApi="/api/identity/users/:id"
        renderForm={formProps => <FrmCreateOrUpdate {...formProps} />}
        renderFormFooter={({ entity, closeMainFormModal }) => (
          <>
            <div className="me-1">
              <CButton onClick={() => {
                setEntity({ ...entity })
                setBankAccountShow(true)
                closeMainFormModal()
              }}
              >Tài khoản</CButton>
            </div>
            <div className="me-1">
              <CButton
                onClick={() => {
                  setEntity({ ...entity })
                  setDepositShow(true)
                  closeMainFormModal()
                }}
              >Tăng/Giảm điểm</CButton>
            </div>
            <div className="me-1">
              <CButton
                onClick={() => {
                  setEntity({ ...entity })
                  setResetPasswordShow(true)
                  closeMainFormModal()
                }}
              >Đặt lại mật khẩu</CButton>
            </div>
          </>
        )}
      />

      <FrmBankAccount
        userInput={entity}
        open={bankAccountShow}
        onClose={() => {
          setBankAccountShow(false)
        }}
      />

      <FrmResetPassword
        userInput={entity}
        open={resetPasswordShow}
        onClose={() => {
          setResetPasswordShow(false)
        }}
      />

      <FrmDeposit
        userInput={entity}
        open={depositShow}
        onClose={() => {
          setDepositShow(false)
          setKey(v4())
        }}
      />
    </>
  )
}


export default User