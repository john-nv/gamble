import { yupResolver } from "@hookform/resolvers/yup"
import * as yup from "yup"

const schema = yup
  .object({
    name: yup.string().nullable().required(),
    isActive: yup.string().nullable().required(),
    description: yup.string().nullable(),
    image: yup.string().nullable(),
    extraProperties: yup.object({
      terms: yup.object({
        lon: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().required().default(.1)
        }),
        nho: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().required().default(.1)
        }),
        chan: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().required().default(.1)
        }),
        le: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().required().default(.1)
        }),
        long: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().required().default(.1)
        }).required(),
        phung: yup.object({
          name: yup.string().required().nullable(),
          rate: yup.number().nullable().required().default(.1)
        }).required()
      }).required()

    }).required()
  })
  .required()

export default yupResolver(schema)
