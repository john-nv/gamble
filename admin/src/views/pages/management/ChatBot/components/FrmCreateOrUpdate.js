import { CForm, CFormInput, CFormLabel, CFormSelect } from "@coreui/react"
import _ from "lodash"
import { Controller } from "react-hook-form"
import { v4 as uuid } from 'uuid'
import BotAvatarMini from "./BotAvatar"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

  const encodeImageFileAsURL = (element) => {
    return new Promise((resolve, reject) => {
      const file = element.files[0]
      const reader = new FileReader()

      reader.onloadend = function () {
        resolve(reader.result)
      }

      reader.readAsDataURL(file)
    })
  }

  return (
    <CForm onSubmit={onSubmit}>
      <CFormInput
        {...register('name')}
        type="text"
        label="Tên"
        invalid={_.has(errors, 'name.message')}
        feedbackInvalid={_.get(errors, 'name.message')}
      />

      <div className="my-3">
        <CFormInput
          {...register('level')}
          type="text"
          label="Level"
          invalid={_.has(errors, 'level.message')}
          feedbackInvalid={_.get(errors, 'level.message')}
        />
      </div>

      <div className="my-3">
        <CFormSelect
          label="Trạng thái"
          invalid={_.has(errors, 'status.message')}
          feedbackInvalid={_.get(errors, 'status.message')}
          {...register('status')}
          options={[
            { label: 'Hoạt động', value: 'active' },
            { label: 'Vô hiệu', value: 'deactive' }
          ]}
        />
      </div>

      <Controller
        name="avatar"
        control={control}
        render={({ field }) => (
          <>
            <div className="my-3 d-flex align-items-center">
              <BotAvatarMini _id={entity?._id} w={100} h={100} />
              <CFormInput
                type="file"
                className="ms-2"
                onChange={async event => {
                  const base64 = await encodeImageFileAsURL(event.target)
                  field.onChange(base64)
                }}
              />
            </div>
          </>
        )}
      />
    </CForm>
  )
}

export default FrmCreateOrUpdate