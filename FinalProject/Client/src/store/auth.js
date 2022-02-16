import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    user: null,
    authenticated: null,
    roles: []
}

export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        loginSuccess: (state, action) => {
            state.authenticated = true;
            state.user = action.payload.user;
            state.roles = action.payload.user.roles;
        },
        updateUser: (state, action) => {
            state.user = action.payload;
        },
        updateRoles:(state,action)=>{
            state.roles = action.payload.roles;
        },
        logout: (state) => {
            state.authenticated = false;
            state.user = null;
            localStorage.removeItem("user");
        },
        setAuth: (state, action) => {
            state.authenticated = action.payload;
        }
    },
})

export const { loginSuccess, logout, updateUser, setAuth,updateRoles } = authSlice.actions

export default authSlice.reducer