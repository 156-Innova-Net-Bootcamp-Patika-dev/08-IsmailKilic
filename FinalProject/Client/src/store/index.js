import { configureStore } from '@reduxjs/toolkit'
import appReducer from './app'
import authReducer from './auth'

export const store = configureStore({
    reducer: {
        app: appReducer,
        auth: authReducer
    },
})