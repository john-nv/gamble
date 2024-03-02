import ScreenInitial from '@components/ScreenInitial';
import globalSetting from '@services/globalSetting';
import { persistor, store } from '@stores';
import { setGlobalSettings } from '@stores/actions/globalSettings';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import App from './App';
import reportWebVitals from './reportWebVitals';
import auth from '@services/auth';
import user from '@services/user';
import { setCurrentUser } from '@stores/actions/session';

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(
  <ScreenInitial />
)

const getSettings = () => {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      Promise.all([
        globalSetting.getApplicationConfiguration(),
        globalSetting.getApplicationLocalization()
      ]).then(resolve)
    }, 1000)
  })
}

getSettings()
  .then(settings => {
    store.dispatch(setGlobalSettings({
      configuration: settings[0],
      localization: settings[1]
    }))
  })
  .then(async () => {
    if (auth.isLoggined()) {
      const profile = await user.getProfile()
      store.dispatch(setCurrentUser(profile))
    }
  })
  .then(() => {
    root.render(
      <Provider store={store}>
        <PersistGate loading={null} persistor={persistor}>
          <App />
        </PersistGate>
      </Provider>
    )
  })
  .finally(() => {
    reportWebVitals()
  })



