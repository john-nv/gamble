const { default: http } = require("@services/http");

const approve = ticketId =>
  http.post(`/api/gamble/ticket/${ticketId}/approve`)

const reject = (ticketId, input) =>
  http.post(`/api/gamble/ticket/${ticketId}/reject`, input)

export default {
  approve,
  reject
}