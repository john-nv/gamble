const { default: http } = require("@services/http");

const getAll = (filter) =>
  http.get('/api/chatbot', { params: filter })

const getById = id =>
  http.get(`/api/chatbot/${id}`)

const create = input =>
  http.post('/api/chatbot', input)

const update = (id, input) =>
  http.put(`/api/chatbot/${id}`, input)

const remove = id =>
  http.delete(`/api/chatbot/${id}`)

export default {
  getAll,
  getById,
  create,
  update,
  remove
}
