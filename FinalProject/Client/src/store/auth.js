import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    token: JSON.parse(localStorage.getItem("user"))?.token || "",
    roles: JSON.parse(localStorage.getItem("user"))?.roles || [],
    authenticated: JSON.parse(localStorage.getItem("user"))?.token ? true : false
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        loginSuccess: (state, action) => {
            state.authenticated = true;
            state.roles = action.payload.roles;
            state.token = action.payload.token;
        },
        logout: (state) => {
            state.authenticated = false;
            state.roles = [];
            state.token = "";
            localStorage.removeItem("user");
        }
    },
})

export const { loginSuccess, logout } = authSlice.actions

export default authSlice.reducer