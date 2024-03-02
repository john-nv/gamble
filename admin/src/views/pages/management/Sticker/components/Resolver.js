import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    image: yup.string().nullable().required("Chưa chọn hình ảnh!"),
  })
  .required()

export default yupResolver(schema)
