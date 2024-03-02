import http from "@services/http"
import _ from "lodash"

const login = input =>
  http.post('/api/gamble/token', input)

const clearToken = () => {
  localStorage.clear()
}

const saveAccessToken = input => {
  localStorage.setItem('okvip.gamble.auth.accessToken', input)
}

const getAccessToken = () =>
  _.trim(localStorage.getItem('okvip.gamble.auth.accessToken'))

const isLoggined = () => {
  return Boolean(getAccessToken())
}

const isStaticGuest = currentUser => {
  return _.trim(currentUser.userName) === 'Guest' && Boolean(_.trim(currentUser.userId))
}

const hasEndUserUse = (currentUser) =>
  !isStaticGuest(currentUser) || isLoggined()

const logout = () => localStorage.removeItem('okvip.gamble.auth.accessToken')

export default {
  clearToken,
  login,
  logout,
  saveAccessToken,
  isLoggined,
  getAccessToken,
  isStaticGuest,
  hasEndUserUse
}