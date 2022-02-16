import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    roles: JSON.parse(localStorage.getItem("user"))?.roles || [],
    user: null,
    token: JSON.parse(localStorage.getItem("user"))?.token || "",
    authenticated: null
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        loginSuccess: (state, action) => {
            state.authenticated = true;
            state.user = action.payload.user;
            state.roles = action.payload.roles;
        },
        updateUser: (state, action) => {
            state.user = action.payload;
        },
        logout: (state) => {
            state.authenticated = false;
            state.roles = [];
            state.user = {};
            localStorage.removeItem("user");
        },
        setAuth: (state, action) => {
            state.authenticated = action.payload;
        }
    },
})

export const { loginSuccess, logout, updateUser, setAuth } = authSlice.actions

export default authSlice.reducer