import { actionTypes } from "@stores/actions/session";

const defaultState = {
  currentUser: null,
}

export default function reducer(state = defaultState, action) {
  switch (action.type) {
    case actionTypes.SET_CURRENT_USER:
      return {
        ...state,
        currentUser: action.currentUser
      }
    default:
      return state;
  }
}
