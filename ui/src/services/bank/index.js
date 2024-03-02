const { default: http } = require("@services/http");

const getMyBankAccounts = () =>
  http.get('/api/gamble/bank-account/my-accounts')

const getBanks = () =>
  http.get('/api/gamble/bank')

const createBankAccount = input =>
  http.post('/api/gamble/bank-account', input)

const deleteBankAccount = id =>
  http.delete(`/api/gamble/bank-account/${id}`)

export default {
  getMyBankAccounts,
  getBanks,
  createBankAccount,
  deleteBankAccount
}