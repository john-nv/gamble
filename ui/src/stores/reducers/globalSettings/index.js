import { actionTypes } from "@stores/actions/globalSettings";

const defaultState = {
  socketUrl: null,
  apiGatewayUrl: null,
  configuration: null,
  localization: null
}

export default function reducer(state = defaultState, action) {
  switch (action.type) {
    case actionTypes.SET_GLOBAL_SETTINGS:
      return {
        ...state,
        ...action.globalSettings
      }
    default:
      return state;
  }
}
