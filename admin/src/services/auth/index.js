import http from "@services/http"

const login = input =>
  http.post('/api/gamble/token', input)

const saveAccessToken = input =>
  localStorage.setItem('okvip.gamble.admin.auth.accessToken', input)

const getAccessToken = () =>
  localStorage.getItem('okvip.gamble.admin.auth.accessToken')

const isLoggined = () => {
  return Boolean(localStorage.getItem('okvip.gamble.admin.auth.accessToken'))
}

const logout = () => localStorage.removeItem('okvip.gamble.admin.auth.accessToken')

export default {
  login,
  logout,
  saveAccessToken,
  isLoggined,
  getAccessToken
}