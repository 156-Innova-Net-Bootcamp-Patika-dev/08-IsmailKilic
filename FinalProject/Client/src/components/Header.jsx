import React from 'react'
import { useDispatch } from 'react-redux'
import { toggleSidebar } from '../store/app'
import DashiconsMenuAlt from './Icons/DashiconsMenuAlt'

const Header = () => {
    const dispatch = useDispatch()

    const toggle = () => {
        dispatch(toggleSidebar());
    }

    return (
        <div className='flex h-16 items-center px-6 w-full shadow-2xl shadow-gray-700'>
            <button onClick={toggle} className='text-2xl block md:hidden'>
                <DashiconsMenuAlt />
            </button>

            <div className='flex-1'></div>

            <button className='button'>Çıkış Yap</button>
        </div>
    )
}

export default Header