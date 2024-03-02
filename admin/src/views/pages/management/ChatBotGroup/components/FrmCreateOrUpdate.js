import { CForm, CFormInput, CFormSelect } from "@coreui/react"
import _ from "lodash"
import BotSelector from "./BotSelector"
import { Controller } from "react-hook-form"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form

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
          {...register('roomId')}
          type="text"
          label="Room"
          invalid={_.has(errors, 'roomId.message')}
          feedbackInvalid={_.get(errors, 'roomId.message')}
        />
      </div>

      {entity?._id && (
        <div className="my-3">
          <CFormSelect
            label="Trạng thái"
            invalid={_.has(errors, 'status.message')}
            feedbackInvalid={_.get(errors, 'status.message')}
            {...register('status')}
            options={[
              { label: 'Đang chạy', value: 'running' },
              { label: 'Dừng', value: 'stop' }
            ]}
          />
        </div>
      )}

      <div className="my-3">
        <Controller
          name="bots"
          control={control}
          render={({ field }) => (
            <BotSelector value={field.value} onChange={field.onChange} />
          )}
        />
      </div>

    </CForm>
  )
}

export default FrmCreateOrUpdate