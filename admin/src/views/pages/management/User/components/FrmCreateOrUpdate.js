import { CCol, CForm, CFormInput, CFormSelect, CRow } from "@coreui/react"
import _ from "lodash"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

  return (
    <CForm onSubmit={onSubmit}>
      <CRow>
        <CCol xs={6} className="mb-3">
          <CFormInput
            {...register('userName')}
            type="text"
            label="Tên"
            invalid={_.has(errors, 'userName.message')}
            feedbackInvalid={_.get(errors, 'userName.message')}
          />
        </CCol>

        <CCol xs={6} className="mb-3">
          <CFormInput
            {...register('name')}
            label="Họ và tên"
            invalid={_.has(errors, 'name.message')}
            feedbackInvalid={_.get(errors, 'name.message')}
          />
        </CCol>
        <CCol xs={6} className="mb-3">
          <CFormInput
            {...register('email')}
            type="text"
            label="Email"
            invalid={_.has(errors, 'email.message')}
            feedbackInvalid={_.get(errors, 'email.message')}
          />
        </CCol>
        <CCol xs={6} className="mb-3">
          <CFormSelect
            label="Trạng thái"
            invalid={_.has(errors, 'isActive.message')}
            feedbackInvalid={_.get(errors, 'isActive.message')}
            {...register('isActive')}
            options={[
              { label: 'Hoạt động', value: true },
              { label: 'Vô hiệu', value: false }
            ]}
          />
        </CCol>
      </CRow>
    </CForm>
  )
}

export default FrmCreateOrUpdate