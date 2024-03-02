import Avatar from "@components/Avatar"
import { CForm, CFormInput } from "@coreui/react"
import image from "@services/image"
import { Controller } from "react-hook-form"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

  return (
    <CForm onSubmit={onSubmit}>
      <Controller
        name="image"
        control={control}
        render={({ field }) => (
          <div className="my-3 d-flex align-items-center">
            <div style={{ width: 150, height: 150 }}><Avatar _id={entity?._id} /></div>
            <div className="ms-2 d-flex flex-column">
              <CFormInput
                type="file"
                onChange={async event => {
                  field.onChange(await image.encodeImageFileAsURL(event.target))
                }}
                invalid={_.has(errors, 'image.message')}
                feedbackInvalid={_.get(errors, 'image.message')}
              />
            </div>
          </div>
        )}
      />
    </CForm>
  )
}

export default FrmCreateOrUpdate