import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    authenticated: true
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        loginSuccess: (state) => {
            state.authenticated = true;
        },
    },
})

export const { loginSuccess } = authSlice.actions

export default authSlice.reducer