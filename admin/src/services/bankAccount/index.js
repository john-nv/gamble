const { default: http } = require("@services/http");

const getBankAccountByUser = userId =>
  http.get('/api/gamble/bank-account', {
    params: {
      creatorId: userId
    }
  })

const updateBankAccount = (id, input) =>
  http.put('/api/gamble/bank-account/' + id, input)

export default {
  getBankAccountByUser,
  updateBankAccount
}