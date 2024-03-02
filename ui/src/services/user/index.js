import http from "@services/http"
import numeral from "numeral"

const lookup = userName =>
  http.post('/api/user/lookup', { userName })

const getProfile = () =>
  http.get('/api/gamble/profile')

const register = input =>
  http.post('/api/account/register', input)

const createWithdrawPassword = input =>
  http.post('/api/gamble/profile/withdraw-password', input)

const createWithdrawTicket = input =>
  http.post('/api/gamble/ticket/withdraw', input)

const getTransactions = input =>
  http.get('/api/gamble/transaction', {
    params: input,
    paramsSerializer: {
      indexes: true, // use brackets with indexes
    }
  })


export default {
  lookup,
  getProfile,
  register,
  createWithdrawPassword,
  createWithdrawTicket,
  getTransactions
}