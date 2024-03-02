import AppModal from "@components/AppModal"
import AppButton from "@components/controls/AppButton"
import { CForm, CFormInput } from "@coreui/react"
import { yupResolver } from "@hookform/resolvers/yup"
import toast from "@services/toast"
import user from "@services/user"
import { useEffect } from "react"
import { useForm } from "react-hook-form"
import { useAsyncFn } from "react-use"
import * as yup from 'yup'


const schema = yup
  .object({
    password: yup.string().nullable().required("Mật khẩu đang trống!").min(6, 'Mật khẩu tối thiếu 6 ký tự'),
  })
  .required()

const FrmResetPassword = ({ open, onClose, userInput }) => {
  const { register, handleSubmit, formState: { errors }, reset } = useForm({ resolver: yupResolver(schema) })

  useEffect(() => {
    reset({ password: null })
  }, [open])

  const [{ loading }, onSubmit] = useAsyncFn(input => {
    return user.updatePassword(userInput.id, input)
      .then(() => {
        toast.sucess('Cập nhật mật khẩu thành công')
        onClose?.()
      })
      .catch(() => {
        toast.sucess('Cập nhật mật khẩu thất bại')
      })
  }, [userInput])

  return (
    <AppModal
      visible={open}
      onClose={onClose}
      renderHeader={() => <div>Cập nhật mật khẩu</div>}
      renderBody={() => (
        <CForm onSubmit={handleSubmit(onSubmit)}>
          <CFormInput
            {...register('password')}
            label="Mật khẩu mới"
            type="password"
            invalid={_.has(errors, 'password.message')}
            feedbackInvalid={_.get(errors, 'password.message')}
          />

        </CForm>
      )}
      renderFooter={() => <AppButton onClick={handleSubmit(onSubmit)} loading={loading} type="primary" >Cập nhật</AppButton>}
    />

  )
}

export default FrmResetPassword