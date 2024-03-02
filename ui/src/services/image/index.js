import _ from "lodash"

const getRemoteImage = (key, options = {}) => {
  const { w, h } = options
  let srcs = [
    process.env.REACT_APP_FILE_SERVER_URL,
    'api/storage/photo',
    key
  ]

  if (w && h) {
    srcs.push('resize')
    srcs.push(w)
    srcs.push(h)
  }

  return _.join(srcs, '/')
}

export default {
  getRemoteImage
}