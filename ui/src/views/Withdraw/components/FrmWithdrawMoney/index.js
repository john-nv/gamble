import AppButton from "@components/Controls/AppButton"
import LoadingScreen from "@components/ScreenInitial"
import { yupResolver } from "@hookform/resolvers/yup"
import game from "@services/game"
import toast from "@services/toast"
import user from "@services/user"
import { setCurrentUser } from "@stores/actions/session"
import numeral from "numeral"
import { useForm } from "react-hook-form"
import { useDispatch, useSelector } from "react-redux"
import { useAsync, useAsyncFn } from "react-use"
import * as Yup from 'yup'

const schema = Yup.object({
  amount: Yup.number().typeError('Số tiền không hợp lệ').positive('Số tiền không hợp lệ').nullable().required('Vui lòng nhập số tiền muốn rút'),
  withdrawPassword: Yup.string().nullable().required('Vui lòng nhập mật khẩu rút tiền')
})

const FrmWithdrawMoney = () => {
  const disapatch = useDispatch()
  const { currentUser } = useSelector(state => state.session)
  const form = useForm({
    resolver: yupResolver(schema)
  })

  const { value: settings, loading: settingLoading } = useAsync(game.getLuckySpaceshipSetting)

  const [{ loading: refreshLoading }, refresh] = useAsyncFn(async () => {
    const currentUser = await user.getProfile()
    disapatch(setCurrentUser({ ...currentUser }))
  })

  const [{ loading }, withdraw] = useAsyncFn(async input => {
    await toast.confirm({
      icon: 'question',
      title: 'Bạn muốn thực hiện rút tiền',
      text: 'Tiền sẽ bị trừ trong tài khoản sau khi bạn thực hiện thao tác này, vui lòng kiểm tra lại số tài khoản ngân hàng bạn đã thiết lập trước đó để tránh xảy ra sai sót',
      onConfirmed: async () => {

        try {
          await user.createWithdrawTicket(input)
          const currentUser = await user.getProfile()
          disapatch(setCurrentUser({ ...currentUser }))

          toast.sucess('Tạo yêu cầu rút tiền thành công')
          form.reset()
        } catch (error) {
          if (error.message) {
            toast.error(error.message)
            return
          }
          toast.error('Tạo yêu cầu rút tiền thất bại')
        }
      }
    })
  })

  if (!currentUser?.allowCreateTicket) {
    return (
      <div className="row">
        <div className="col-12">
          <div className="alert alert-info" role="alert">
            <h4 className="alert-heading">Thông báo</h4>
            <p>Vui lòng chờ trong ít phút, yêu cầu rút tiền của bạn đang được xử lý</p>
            <AppButton className="btn btn-primary" loading={refreshLoading} type="button" onClick={refresh}>Làm mới</AppButton>
          </div>
        </div>
      </div>
    )
  }

  if (settingLoading) return <LoadingScreen />
  return (
    <form className="row galaxy-macau-form" onSubmit={form.handleSubmit(withdraw)}>
      <div className="col-12">
        <p className="text-light">Số dư: {numeral(currentUser.accountBalance).format()}$</p>
        <p className="text-light">Giá trị quy đổi 1$ = {numeral(settings?.amountInVndUnit).format()} VNĐ</p>
      </div>
      <div className="col-12 mb-2 ">
        <label htmlFor="amount" className="form-label">Số tiền rút</label>
        <input {...form.register('amount')} className="form-control" id="amount" />
        <div className="invalid-feedback d-block">
          {form.formState.errors.amount?.message}
        </div>
      </div>

      <div className="col-12 mb-2 ">
        <label htmlFor="withdrawPassword" className="form-label">Mật khẩu rút tiền</label>
        <input {...form.register('withdrawPassword')} type="password" className="form-control" id="withdrawPassword" />
        <div className="invalid-feedback d-block">
          {form.formState.errors.withdrawPassword?.message}
        </div>
      </div>

      <div className="col-12 my-3">
        <AppButton type="submit" loading={loading} className="btn-custom-primary">Rút tiền</AppButton>
      </div>
    </form>
  )
}

export default FrmWithdrawMoney