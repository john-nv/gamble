import AppModal from "@components/AppModal"
import AppButton from "@components/controls/AppButton"
import { CForm, CFormInput } from "@coreui/react"
import { yupResolver } from "@hookform/resolvers/yup"
import bankAccount from "@services/bankAccount"
import toast from "@services/toast"
import _ from "lodash"
import { useEffect } from "react"
import { useForm } from "react-hook-form"
import { useAsync, useAsyncFn } from "react-use"
import * as yup from 'yup'


const schema = yup
  .object({
    items: yup.array().of(
      yup.object({
        id: yup.string().nullable().required(),
        no: yup.string().nullable().required(),
        name: yup.string().nullable().required(),
        bankId: yup.number().nullable().required(),
        branch: yup.string().nullable()
      }))
      .required()
  })
  .required()

const FrmBankAccount = ({ open, onClose, userInput }) => {
  const { register, handleSubmit, formState: { errors }, reset } = useForm({ resolver: yupResolver(schema) })

  const { loading: bankAccountLoading, value: bankAccountRsult } = useAsync(() => {
    if (open && userInput) {
      return bankAccount.getBankAccountByUser(userInput.id)
    }

    return Promise.resolve({
      items: [],
      totalCount: 0
    })

  }, [open, userInput])

  useEffect(() => {
    reset(bankAccountRsult)
  }, [bankAccountRsult])

  const [{ loading }, onSubmit] = useAsyncFn(async input => {
    try {

      if (!_.some(input.items))
        return toast.error('Thông tin tài khoản chưa có')

      const cmd = _.map(input.items, item => bankAccount.updateBankAccount(item.id, {
        no: item.no,
        name: item.name,
        bankId: item.bankId,
        branch: item.branch
      }))

      await Promise.all(cmd)
      toast.sucess('Cập nhật thành công')
      onClose?.()
    } catch {
      toast.error('Cập nhật thất bại')
    }


  }, [userInput])

  return (
    <AppModal
      size="lg"
      visible={open}
      onClose={onClose}
      renderHeader={() => <div>Tài khoản ngân hàng</div>}
      renderBody={() => (
        <CForm onSubmit={handleSubmit(onSubmit)}>
          {_.map(bankAccountRsult?.items, (item, idx) => (
            <div className="row" key={item.id}>
              <div className="col-4" >
                <input type="hidden" {...register(`items[${idx}].id`)} />
                <input type="hidden" {...register(`items[${idx}].bankId`)} />
                <input type="hidden" {...register(`items[${idx}].branch`)} />
                <CFormInput label={`Ngân hàng ${idx + 1}`} {...register(`items[${idx}].bank.name`)} readOnly />
              </div>
              <div className="col-4" key={item.id}>
                <CFormInput
                  label={`Tên người thụ hưởng ${idx + 1}`}
                  {...register(`items[${idx}].name`)}
                  invalid={_.has(errors, ['items', idx, 'name', 'message'])}
                  feedbackInvalid={_.get(errors, ['items', idx, 'name', 'message'])}
                />
              </div>
              <div className="col-4" key={item.id}>
                <CFormInput
                  {...register(`items[${idx}].no`)}
                  label={`Số tài khoản ${idx + 1}`}
                  invalid={_.has(errors, ['items', idx, 'no', 'message'])}
                  feedbackInvalid={_.get(errors, ['items', idx, 'no', 'message'])}
                />
              </div>
            </div>
          ))}
        </CForm>
      )}
      renderFooter={() => <AppButton onClick={handleSubmit(onSubmit)} loading={loading} type="primary">Cập nhật</AppButton>}
    />

  )
}

export default FrmBankAccount