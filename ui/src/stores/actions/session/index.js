export const actionTypes = {
  SET_CURRENT_USER: '@@SET_CURRENT_USER'
}

export const setCurrentUser = currentUser => ({
  type: actionTypes.SET_CURRENT_USER,
  currentUser
})
