import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    userName: yup.string().nullable().required("Tên người dùng trống!"),
    name: yup.string().nullable().required("Tên người dùng trống!"),
    email: yup.string().nullable().required("Email người dùng trống!"),
    isActive: yup.boolean()
  })
  .required()

export default yupResolver(schema)
