import { combineReducers } from 'redux';
import globalSettings from './globalSettings'
import session from './session'

export default () =>
  combineReducers({
    globalSettings,
    session
  })