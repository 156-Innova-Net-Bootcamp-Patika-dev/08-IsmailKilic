import React from 'react'
import { useSelector } from 'react-redux'
import { useDispatch } from 'react-redux'
import { toggleSidebar } from '../store/app'
import { logout } from '../store/auth'
import DashiconsMenuAlt from './Icons/DashiconsMenuAlt'

const Header = () => {
    const dispatch = useDispatch()
    const { user } = useSelector(state => state.auth)

    const toggle = () => {
        dispatch(toggleSidebar());
    }

    const handleLogout = () => {
        dispatch(logout());
    }

    return (
        <div className='flex items-center w-full h-16 px-6 shadow-2xl shadow-gray-700'>
            <button onClick={toggle} className='block text-2xl md:hidden'>
                <DashiconsMenuAlt />
            </button>

            <div className='flex-1'></div>

            <span>{user.userName}</span>
            <button onClick={handleLogout} className='ml-5 button'>Çıkış Yap</button>
        </div>
    )
}

export default Header