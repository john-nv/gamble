const { default: http } = require("@services/http");

const getList = () =>
  http.get('/api/gamble/chip')

export default {
  getList
}