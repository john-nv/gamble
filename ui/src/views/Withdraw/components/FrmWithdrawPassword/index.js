import AppButton from "@components/Controls/AppButton"
import { yupResolver } from "@hookform/resolvers/yup"
import toast from "@services/toast"
import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import { useForm } from "react-hook-form"
import { useDispatch } from "react-redux"
import { useAsyncFn } from "react-use"
import * as Yup from 'yup'

const schema = Yup.object({
  withdrawPassword: Yup.string().nullable().min(6, 'Mật khẩu tối thiểu 6 ký tự').required('Vui lòng nhập mật khẩu'),
  confirmPassword: Yup.string().nullable()
    .oneOf([Yup.ref('withdrawPassword'), null], 'Mật khẩu không khớp')
    .required('Vui lòng nhập lại mật khẩu'),
})

const FrmWithdrawPassword = () => {
  const dispatch = useDispatch()
  const form = useForm({
    resolver: yupResolver(schema)
  })

  const [{ loading }, createWithdrawPassword] = useAsyncFn(async input => {
    try {
      const userProfile = await user.createWithdrawPassword(input)
      toast.sucess('Tạo mật khẩu rút tiền thành công')
      dispatch(setCurrentUser(userProfile))
    } catch (error) {
      toast.error('Tạo mật khẩu rút tiền thất bại')
    }
  })

  return (
    <>
      <div className="row">
        <div className="col-12 warning">
          <div className="alert alert-warning" role="alert">
            <h4 className="alert-heading">Cảnh báo</h4>
            <p>Bạn chưa thiết lập mật khẩu rút tiền, vui lòng thiết lập mật khẩu rút tiền để tiếp tục</p>
          </div>
        </div>
      </div>
      <form className="row galaxy-macau-form" onSubmit={form.handleSubmit(createWithdrawPassword)}>
        <div className="col-12 mb-2 ">
          <label htmlFor="withdrawPassword" className="form-label">Mật khẩu</label>
          <input {...form.register('withdrawPassword')} type="password" className="form-control" id="withdrawPassword" placeholder="Vui lòng nhập vào mật khẩu" />
          <div className="invalid-feedback d-block">
            {form.formState.errors.withdrawPassword?.message}
          </div>
        </div>
        <div className="col-12 mb-2 ">
          <label htmlFor="confirmPassword" className="form-label">Xác nhận mật khẩu</label>
          <input {...form.register('confirmPassword')} type="password" className="form-control" id="confirmPassword" placeholder="Vui lòng nhập lại mật khẩu" />
          <div className="invalid-feedback d-block">
            {form.formState.errors.confirmPassword?.message}
          </div>
        </div>
        <div className="col-12 my-3">
          <AppButton type="submit" loading={loading} className="btn-custom-primary">Xác nhận</AppButton>
        </div>
      </form>
    </>
  )
}


export default FrmWithdrawPassword