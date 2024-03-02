import _ from "lodash"
import { useForm } from "react-hook-form"
import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"
import { useNavigate } from "react-router-dom"
import { useAsyncFn } from "react-use"
import auth from "@services/auth"
import AppButton from "@components/Controls/AppButton"
import toast from "@services/toast"
import { useDispatch } from "react-redux"
import { setCurrentUser } from "@stores/actions/session"

const schema = yup
  .object({
    username: yup.string().nullable().required(),
    password: yup.string().nullable().required()
  })
  .required()

const Login = () => {
  const navigation = useNavigate()
  const dispatch = useDispatch()
  const { register, formState: { isValid }, handleSubmit } = useForm({ resolver: yupResolver(schema) })

  const [{ loading }, onSubmit] = useAsyncFn(input => {
    return auth.login(input)
      .then(result => {
        auth.saveAccessToken(result.accessToken)
        dispatch(setCurrentUser(result.profile))
        navigation('/')
      })
      .catch(({ details }) => {
        toast.error(details)
      })
  })

  return (
    <form className="login-form" onSubmit={handleSubmit(onSubmit)}>
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-solid fa-user"></i></span>
        <input {...register("username")} type="text" className="form-control form-control-lg" placeholder="Vui lòng nhập tên đăng nhập" />
      </div>
      <div className="input-group mb-3">
        <span className="input-group-text"><i className="fa-solid fa-key"></i></span>
        <input  {...register("password")} type="password" className="form-control form-control-lg" placeholder="Vui lòng nhập mật khẩu" />
      </div>
      <AppButton type="submit" className="w-100 btn btn-primary btn-lg" loading={loading} disabled={!isValid}>Đăng nhập</AppButton>
      <div className="d-flex justify-content-between my-3">
        <a role="button" className="text-light text-decoration-none" onClick={() => navigation('/dang-ky-tai-khoan')}>Đăng ký</a>
        <a role="button" className="text-light text-decoration-none">Liên hệ CSKH</a>
      </div>
    </form>
  )
}

export default Login