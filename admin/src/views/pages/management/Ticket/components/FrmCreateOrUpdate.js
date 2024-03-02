import { CCard, CCardBody, CCardText, CCardTitle, CCol, CForm, CFormInput, CFormSelect, CListGroup, CListGroupItem, CRow } from "@coreui/react"
import bankAccount from "@services/bankAccount"
import _ from "lodash"
import moment from "moment"
import numeral from "numeral"
import { Controller } from "react-hook-form"
import { useAsync } from "react-use"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

  const { value: bankAccountResult } = useAsync(() => {
    if (entity)
      return bankAccount.getBankAccountByUser(entity.creatorId)

    return Promise.resolve({ items: [] })
  }, [entity])

  return (
    <CForm>
      <CRow>
        <CCol xs={12} className="mb-3">
          <CFormInput
            {...register('title')}
            type="text"
            readOnly
            label="Tiêu đề"
          />
        </CCol>

        <CCol xs={6} className="mb-3">
          <CFormInput
            readOnly
            {...register('creatorUsername')}
            label="TK người dùng"
          />
        </CCol>
        <CCol xs={6} className="mb-3">
          <CFormInput
            {...register('creatorEmail')}
            type="text"
            readOnly
            label="Email người dùng"
          />
        </CCol>
        <CCol xs={6} className="mb-3">
          <Controller
            name="amount"
            control={form.control}
            render={({ field }) => (
              <CFormInput
                type="text"
                readOnly
                label="Số tiền rút trong game"
                value={numeral(field.value).format()}
              />
            )}
          />
        </CCol>
        <CCol xs={6} className="mb-3">
          <Controller
            name="amountInVnd"
            control={form.control}
            render={({ field }) => (
              <CFormInput
                type="text"
                readOnly
                label="Số tiền rút mệnh giá VNĐ"
                value={numeral(field.value).format()}
              />
            )}
          />
        </CCol>
        <CCol xs={12} className="mb-3">
          <CFormInput
            {...register('amountInVndText')}
            type="text"
            readOnly
            label="Số tiền rút mệnh giá VNĐ (Bằng chữ)"
          />
        </CCol>

        <CCol xs={12} className="mb-3">
          <h5>Tài khoản ngân hàng</h5>
          {_.map(bankAccountResult?.items, item => (
            <CCard style={{ width: '50%' }} key={item.id}>
              <CCardBody>
                <CCardTitle>{item.bank.name} - {item.bank.shortName}</CCardTitle>
                <CCardText>
                  <div>Người thụ hưởng: {item.name}</div>
                  <div>Số TK: {item.no}</div>
                  <div>Chi nhánh: {item.branch}</div>
                </CCardText>
              </CCardBody>
            </CCard>
          ))}

        </CCol>

        <CCol xs={12}>
          <h5>Lịch sử</h5>
          <CListGroup>
            {_.map(_.sortBy(entity?.ticketLogs, 'creationTime desc'), item => (
              <CListGroupItem key={item.id}>
                {_.join(_.filter([moment(item.creationTime).format('DD/MM/YYYY HH:mm'), item.creatorUsername, item.statusName, item.note]), ' - ')}
              </CListGroupItem>
            ))}
          </CListGroup>
        </CCol>
      </CRow>
    </CForm>
  )
}

export default FrmCreateOrUpdate