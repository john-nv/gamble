import AppButton from "@components/Controls/AppButton"
import { yupResolver } from "@hookform/resolvers/yup"
import toast from "@services/toast"
import user from "@services/user"
import { useForm } from "react-hook-form"
import { useNavigate } from "react-router-dom"
import { useAsyncFn } from "react-use"
import * as yup from "yup"

const schema = yup
  .object({
    appName: yup.string().nullable().required(),
    username: yup.string().nullable().required().max(25),
    password: yup.string().nullable().required().max(128).min(6),
    confirmPassword: yup.string().nullable().oneOf([yup.ref('password'), null], 'Mật khẩu không khớp').required().max(128).min(6),
    emailAddress: yup.string().email().nullable().required().max(25)
  })
  .required()

const Registration = () => {
  const navigation = useNavigate()
  const { register, formState: { errors, isValid }, handleSubmit } = useForm({
    resolver: yupResolver(schema),
    defaultValues: {
      appName: 'MegaLuck',
      username: null,
      password: null,
      confirmPassword: null,
      emailAddress: null
    }
  })

  const [{ loading }, onSubmit] = useAsyncFn(input => {
    return user.register({ ...input, appName: 'MegaLuck' })
      .then(() => {
        toast.sucess('Đăng ký tài khoản thành công')
        navigation('/dang-nhap')
      })
      .catch(error => {
        return toast.error(error.message)
      })
  })

  return (
    <form className="login-form" onSubmit={handleSubmit(onSubmit)}>
      <input type="hidden" {...register('appName')} />
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-regular fa-envelope"></i></span>
        <input  {...register("emailAddress")} type="email" className="form-control form-control-lg" placeholder="Vui lòng nhập email" />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-solid fa-user"></i></span>
        <input {...register("username")} type="text" className="form-control form-control-lg" placeholder="Vui lòng nhập tên đăng nhập" />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-solid fa-key"></i></span>
        <input  {...register("password")} type="password" className="form-control form-control-lg" placeholder="Vui lòng nhập mật khẩu" />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-solid fa-key"></i></span>
        <input  {...register("confirmPassword")} type="password" className="form-control form-control-lg" placeholder="Vui lòng nhập lại mật khẩu" />
      </div>

      <AppButton type="submit" className="w-100 btn btn-primary btn-lg" disabled={!isValid} loading={loading}>Đăng ký tài khoản</AppButton>
      <div className="d-flex justify-content-between my-3">
        <a role="button" className="text-light text-decoration-none" onClick={() => navigation('/dang-nhap')}>Đăng nhập</a>
        <a role="button" className="text-light text-decoration-none">Liên hệ CSKH</a>
      </div>
    </form>
  )
}

export default Registration