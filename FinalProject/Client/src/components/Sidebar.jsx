import React, { useRef } from 'react'
import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { NavLink, useLocation } from 'react-router-dom'
import { toggleSidebar } from '../store/app'
import VaadinClose from './Icons/VaadinClose'

const Sidebar = () => {
    const { sidebarOpened } = useSelector(state => state.app)
    const { roles } = useSelector(state => state.auth)

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
        { name: "Ana Sayfa", href: "/", admin: false },
        { name: "Daireler", href: "/apartments", admin: true },
        { name: "Kişiler", href: "/users", admin: true },
        { name: "Mesajlar", href: "/messages", admin: false },
    ]

    return (
        <div
            className={`${sidebarOpened ? 'translate-x-0' : 'md:translate-x-0 -translate-x-full'} bg-slate-600 fixed flex-col left-0 top-0 h-full  flex w-64  
            duration-150 transition-all p-4`}>

            <div className='flex justify-between p-3 text-white shadow-lg shadow-black'>
                <h2 className='text-lg uppercase'>Apartman Yönetim Sistemi</h2>
                <button className='md:hidden' onClick={toggle}>
                    <VaadinClose />
                </button>
            </div>

            <ul className='mt-10'>
                {
                    links.map((link, index) => (
                        (!link.admin || link.admin && roles.includes("Admin")) && <NavLink className={`${location.pathname === link.href ? "bg-gray-900" : "hover:bg-gray-700"} 
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