import { createStore } from 'redux'

const initialState = {
  sidebarShow: true,
  imgVersion: '1.1.0'
}

const changeState = (state = initialState, { type, ...rest }) => {
  switch (type) {
    case 'set':
      return {
        ...state,
        ...rest
      }

    case 'SET_IMAGE_VERSION': {
      return {
        ...state,
        ...rest
      }
    }
    default:
      return state
  }
}

const store = createStore(changeState)
export default store
