const { default: http } = require("@services/http");

const updatePassword = async (id, input) => {
  const user = await http.get('/api/identity/users/' + id)
  await http.put(`/api/identity/users/${id}`, {
    ...user,
    password: input.password
  })
}

const createDepositTransaction = input =>
  http.post('/api/gamble/transaction/deposit', input)

export default {
  updatePassword,
  createDepositTransaction
}