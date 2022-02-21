import React from 'react'
import { useSelector } from 'react-redux'
import { useDispatch } from 'react-redux'
import { Link } from 'react-router-dom'
import { toggleSidebar } from '../store/app'
import { logout } from '../store/auth'
import DashiconsMenuAlt from './Icons/DashiconsMenuAlt'
import MdiMenuDown from './Icons/MdiMenuDown'

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
        <div className='flex items-center w-full h-16 px-6 shadow-2xl shadow-gray-800'>
            <button onClick={toggle} className='block text-2xl md:hidden'>
                <DashiconsMenuAlt />
            </button>

            <div className='flex-1'></div>

            <div className='relative mr-10 group'>
                <span className='flex items-center cursor-pointer'>{user.userName}
                    <MdiMenuDown />
                </span>

                <div className='
                absolute
                shadow-2xl
                shadow-black
                rounded-md 
                hidden
                z-50
                group-hover:flex
                right-0 
                p-2 
                text-white 
                bg-gray-700 
                flex-col 
                min-w-[180px]'
                >
                    <Link to={"/profile"} className="flex p-1 hover:bg-gray-600" >Profil</Link>
                    <Link to={"/change-password"} className="flex p-1 hover:bg-gray-600" >Şifre Değiştir</Link>
                    <button onClick={handleLogout} className="flex p-1 hover:bg-gray-600" >Çıkış Yap</button>
                </div>
            </div>
        </div>
    )
}

export default Header