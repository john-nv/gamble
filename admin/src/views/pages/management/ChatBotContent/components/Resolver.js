import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    content: yup.string().nullable().required("Nội dung bot trống!")
  })
  .required()

export default yupResolver(schema)
