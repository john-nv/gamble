import { CForm, CFormInput } from "@coreui/react"
import _ from "lodash"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

  return (
    <CForm onSubmit={onSubmit}>
      <CFormInput
        {...register('content')}
        type="text"
        label="Nội dung"
        invalid={_.has(errors, 'content.message')}
        feedbackInvalid={_.get(errors, 'content.message')}
      />
    </CForm>
  )
}

export default FrmCreateOrUpdate