export const actionTypes = {
  SET_GLOBAL_SETTINGS: '@@SET_GLOBAL_SETTINGS'
}

export const setGlobalSettings = globalSettings => ({
  type: actionTypes.SET_GLOBAL_SETTINGS,
  globalSettings
})
