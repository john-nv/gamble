import { CForm, CFormInput, CFormTextarea } from "@coreui/react"

const FrmCreateOrUpdate = ({ onSubmit, form, entity }) => {
  const { register, formState: { errors, }, control } = form
  console.log({ errors })
  return (
    <CForm onSubmit={onSubmit} className='game-editor p-1'>
      <input type="hidden" {...register('id')} />
      <input type="hidden" {...register('isActive')} />
      <input type="hidden" {...register('extraProperties.terms.lon.name')} />
      <input type="hidden" {...register('extraProperties.terms.nho.name')} />
      <input type="hidden" {...register('extraProperties.terms.chan.name')} />
      <input type="hidden" {...register('extraProperties.terms.le.name')} />
      <input type="hidden" {...register('extraProperties.terms.long.name')} />
      <input type="hidden" {...register('extraProperties.terms.phung.name')} />

      <div className="row">
        <div className="col-12">
          <CFormInput
            {...register('name')}
            className="mb-2"
            label="Tên"
            type="text"
            id="name"
            invalid={_.has(errors, 'name.message')}
            feedbackInvalid={_.get(errors, 'name.message')}
          />
        </div>
      </div>

      <div className="row">
        <div className="col-12">
          <CFormTextarea
            {...register('description')}
            className="mb-2"
            label="Mô tả"
            type="text"
            id="description"
          />
        </div>
      </div>

      <div className="row">
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.lon.rate')}
            min={0.1}
            className="mb-2"
            label="Lớn"
            type="number"
            id="extraProperties.terms.lon"
            invalid={_.has(errors, 'extraProperties.terms.lon.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.lon.rate.message')}
          />
        </div>
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.nho.rate')}
            min={0.1}
            className="mb-2"
            label="Nhỏ"
            type="number"
            id="extraProperties.terms.nho"
            invalid={_.has(errors, 'extraProperties.terms.nho.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.nho.rate.message')}
          />
        </div>
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.chan.rate')}
            min={0.1}
            className="mb-2"
            label="Chẵn"
            type="number"
            id="extraProperties.terms.chan"
            invalid={_.has(errors, 'extraProperties.terms.chan.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.chan.rate.message')}
          />
        </div>
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.le.rate')}
            min={0.1}
            className="mb-2"
            label="Lẻ"
            type="number"
            id="extraProperties.terms.le"
            invalid={_.has(errors, 'extraProperties.terms.le.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.le.rate.message')}
          />
        </div>
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.long.rate')}
            min={0.1}
            label="Long"
            className="mb-2"
            type="number"
            id="extraProperties.terms.long"
            invalid={_.has(errors, 'extraProperties.terms.long.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.long.rate.message')}
          />
        </div>
        <div className="col-6">
          <CFormInput
            {...register('extraProperties.terms.phung.rate')}
            min={0.1}
            className="mb-2"
            label="Phụng"
            type="number"
            id="extraProperties.terms.phung"
            invalid={_.has(errors, 'extraProperties.terms.phung.rate.message')}
            feedbackInvalid={_.get(errors, 'extraProperties.terms.phung.rate.message')}
          />
        </div>
      </div>
    </CForm>
  )
}

export default FrmCreateOrUpdate