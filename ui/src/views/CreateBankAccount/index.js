import PageTitle from "@components/PageTitle"
import { yupResolver } from "@hookform/resolvers/yup"
import { Controller, useForm } from "react-hook-form"
import * as yup from "yup"
import Select from 'react-select'
import { useAsync, useAsyncFn } from "react-use"
import bank from "@services/bank"
import _ from "lodash"
import toast from "@services/toast"

const schema = yup
  .object({
    name: yup.string().nullable().required('Vui lòng nhập vào tên tài khoản!'),
    branch: yup.string().nullable(),
    bank: yup.object({
      value: yup.string().nullable().required('Vui lòng chọn ngân hàng!'),
      label: yup.string().nullable().required()
    }).required(),
    no: yup.string().nullable().required('Vui lòng nhập vào số tài khoản!')
  })
  .required()


const CreateBankAccount = () => {
  const form = useForm({ resolver: yupResolver(schema) })
  const { formState: { isValid, errors } } = form

  const { value, loading } = useAsync(bank.getBanks)

  const [{ loading: createLoading }, submitFn] = useAsyncFn(async input => {
    input.bankId = input.bank.value
    await bank.createBankAccount(input)
      .then(() => toast.sucess('Tạo tài khoản thành công'))
      .catch(() => toast.error('Tạo tài khoản thất bại'))

    form.reset()
  })


  return (
    <div className="game-create-bank-account flex-fill d-flex flex-column">
      <div className="container flex-fill">
        <div className="row mt-2 mb-4">
          <PageTitle to="/tai-khoan/cai-dat/quan-ly-the-ngan-hang" title="Thêm thẻ ngân hàng" />
        </div>
        {!createLoading && (
          <form className="row mt-2 needs-validation" onSubmit={form.handleSubmit(submitFn)}>
            <div className="col-12 mb-2 ">
              <label htmlFor="name" className="form-label">Tên chủ tài khoản (viết in hoa không dấu)</label>
              <input {...form.register('name')} type="text" className="form-control" id="name" placeholder="Viết hoa không dấu" />
              <div className="invalid-feedback d-block">
                {errors.name?.message}
              </div>
            </div>
            <div className="col-12 mb-2">
              <label htmlFor="bankId" className="form-label">Ngân hàng</label>
              <Controller
                name="bank"
                id="bank"
                control={form.control}
                render={({ field }) => (
                  <Select
                    isLoading={loading}
                    isClearable
                    filterOption={(options, input) => {
                      return _.toLower(options.data.name).includes(_.toLower(input)) || _.toLower(options.data.shortName).includes(_.toLower(input))
                    }}
                    className="bank-select"
                    placeholder="Chọn ngân hàng"
                    onChange={field.onChange}
                    value={field.value}
                    options={_.map(value?.items, item => ({ value: item.id, label: item.name, ...item }))}

                  />
                )}
              />
              <div className="invalid-feedback d-block">
                {errors.bank?.value?.message}
              </div>
            </div>
            <div className="col-12 mb-2">
              <label htmlFor="no" className="form-label">Số tài khoản</label>
              <input {...form.register('no')} type="text" className="form-control" id="no" />
              <div className="invalid-feedback d-block">
                {errors.no?.message}
              </div>
            </div>
            <div className="col-12 mb-2 ">
              <label htmlFor="branch" className="form-label">Chi nhánh</label>
              <input {...form.register('branch')} type="text" className="form-control" id="branch" />
              <div className="invalid-feedback d-block">
                {errors.branch?.message}
              </div>
            </div>
            <div className="col-12 my-3">
              <button type="submit" className="btn-account-confirm">Xác nhận</button>
            </div>
          </form>
        )}

      </div>
    </div>
  )
}

export default CreateBankAccount