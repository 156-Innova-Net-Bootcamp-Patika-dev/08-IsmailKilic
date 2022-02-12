import React, { useRef } from 'react'
import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { NavLink, useLocation } from 'react-router-dom'
import { toggleSidebar } from '../store/app'
import VaadinClose from './Icons/VaadinClose'

const Sidebar = () => {
    const { sidebarOpened } = useSelector(state => state.app)
    const dispatch = useDispatch()
    const location = useLocation()
    const isInitialMount = useRef(true)


    const toggle = () => {
        dispatch(toggleSidebar())
    }

    useEffect(() => {
        if (isInitialMount.current) {
            isInitialMount.current = false
        } else {
            toggle()
        }
    }, [location.pathname])


    const links = [
        { name: "Ana Sayfa", href: "/" },
        { name: "Daireler", href: "/apartments" },
        { name: "Kişiler", href: "/users" },
        { name: "Mesajlar", href: "/messages" },
    ]

    return (
        <div
            className={`${sidebarOpened ? 'translate-x-0' : 'md:translate-x-0 -translate-x-full'} bg-slate-600 fixed flex-col left-0 top-0 h-full  flex w-64  
            duration-150 transition-all p-4`}>

            <div className='flex text-white justify-between shadow-lg shadow-black p-3'>
                <h2 className='uppercase text-lg'>Apartman Yönetim Sistemi</h2>
                <button className='md:hidden' onClick={toggle}>
                    <VaadinClose />
                </button>
            </div>

            <ul className='mt-10'>
                {
                    links.map((link, index) => (
                        <NavLink className={`${location.pathname === link.href ? "bg-gray-900" : "hover:bg-gray-700"} 
                            text-white block p-2 rounded-sm mb-2`} to={link.href} key={index}>
                            {link.name}
                        </NavLink>
                    ))
                }
            </ul>
        </div>
    )
}

export default Sidebar