import _ from 'lodash';
import { applyMiddleware, compose, createStore } from 'redux';
import { persistReducer, persistStore } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import createSagaMiddleware from 'redux-saga';
import thunkMiddleware from 'redux-thunk';
import rootReducer from './reducers';
import rootSaga from './sagas';

// const reduxDevTools = process?.env?.REACT_APP_DEV_ENV === "YES" ? true : false;
const reduxDevTools = true;
const persistConfig = {
  key: 'okvip.chatui.storage',
  storage,
  whitelist: ['globalSettings', 'session'],
}

const persistedReducer = persistReducer(persistConfig, rootReducer())

const sagaMiddleware = createSagaMiddleware();
const composeEnhancers = _.get(window, '__REDUX_DEVTOOLS_EXTENSION_COMPOSE__', compose);

let store = createStore(persistedReducer, applyMiddleware(sagaMiddleware, thunkMiddleware))
if (reduxDevTools)
  store = createStore(persistedReducer, composeEnhancers(applyMiddleware(sagaMiddleware, thunkMiddleware)))

sagaMiddleware.run(rootSaga)

const persistor = persistStore(store)

export {
  store,
  persistor,
}

