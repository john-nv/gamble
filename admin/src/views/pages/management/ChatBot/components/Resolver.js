import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    name: yup.string().nullable().required("Tên bot trống!"),
    status: yup.string().nullable().required("Trạng thái bot trống!"),
    level: yup.number().nullable().required("Level trống!"),
    avatar: yup.string().nullable()
  })
  .required()

export default yupResolver(schema)
