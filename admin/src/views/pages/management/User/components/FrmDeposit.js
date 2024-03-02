import AppModal from "@components/AppModal"
import AppButton from "@components/controls/AppButton"
import { CForm, CFormCheck, CFormInput } from "@coreui/react"
import { yupResolver } from "@hookform/resolvers/yup"
import toast from "@services/toast"
import user from "@services/user"
import { useEffect } from "react"
import { useForm } from "react-hook-form"
import { useAsyncFn } from "react-use"
import * as yup from 'yup'


const schema = yup
  .object({
    userId: yup.string().required(),
    isRevoke: yup.boolean().default(false).required(),
    amount: yup.number()
      .positive('Giá trị lớn hơn 0!')
      .typeError('Giá trị không hợp lệ!')
      .nullable()
      .required("Vui lòng nhập vào giá trị!"),
  })
  .required()

const FrmDeposit = ({ open, onClose, userInput }) => {
  const { register, handleSubmit, formState: { errors }, reset } = useForm({ resolver: yupResolver(schema) })

  useEffect(() => {
    reset({ amount: null, userId: userInput?.id })
  }, [open, userInput])

  const [{ loading }, onSubmit] = useAsyncFn(async input => {
    const prefix = !input.isRevoke ? 'Tăng điểm' : 'Giảm điểm'
    try {
      await user.createDepositTransaction(input)
      toast.sucess(prefix + ' thành công')
      onClose?.()
    } catch (error) {
      toast.error(prefix + ' thất bại')
    }
  }, [userInput])

  return (
    <AppModal
      visible={open}
      onClose={onClose}
      renderHeader={() => <div>Tăng điểm cho {userInput?.userName}</div>}
      renderBody={() => (
        <CForm onSubmit={handleSubmit(onSubmit)}>
          <input type="hidden" {...register('userId')} />
          <CFormInput
            {...register('amount')}
            label="Giá trị"
            type="number"
            className="mb-2"
            invalid={_.has(errors, 'amount.message')}
            feedbackInvalid={_.get(errors, 'amount.message')}
          />
          <CFormCheck  {...register('isRevoke')} label="Thu hồi (Trừ tiền)" />
        </CForm>
      )}
      renderFooter={() => <AppButton onClick={handleSubmit(onSubmit)} loading={loading} type="primary" >Đồng ý</AppButton>}
    />

  )
}

export default FrmDeposit