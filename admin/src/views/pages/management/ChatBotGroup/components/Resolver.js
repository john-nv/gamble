import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    name: yup.string().nullable().required("Tên bot trống!"),
    status: yup.string().nullable().required("Trạng thái trống!"),
    roomId: yup.string().nullable().required("Room trống!"),
  })
  .required()

export default yupResolver(schema)
