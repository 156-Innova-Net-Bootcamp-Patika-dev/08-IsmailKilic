import { createSlice } from '@reduxjs/toolkit'

const initialState = {
    sidebarOpened: false,
    showAddAptModal: false,
    showAddUserModal: false,
}

export const appSlice = createSlice({
    name: 'app',
    initialState,
    reducers: {
        toggleSidebar: (state) => {
            state.sidebarOpened = !state.sidebarOpened;
        },
        toggleAptModal: (state) => {
            state.showAddAptModal = !state.showAddAptModal;
        },
        toggleUserModal: (state) => {
            state.showAddUserModal = !state.showAddUserModal;
        }
    },
})

export const { toggleSidebar, toggleAptModal, toggleUserModal } = appSlice.actions

export default appSlice.reducer