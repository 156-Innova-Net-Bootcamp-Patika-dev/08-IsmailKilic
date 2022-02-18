import React, { useRef } from 'react'
import { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { NavLink, useLocation } from 'react-router-dom'
import { toggleSidebar } from '../store/app'
import VaadinClose from './Icons/VaadinClose'
import MdiHome from './Icons/MdiHome'
import IcBaselineApartment from './Icons/IcBaselineApartment'
import IcRoundGroup from './Icons/IcRoundGroup'
import IcRoundMessage from './Icons/IcRoundMessage'
import FluentPayment16Filled from './Icons/FluentPayment16Filled'

const Sidebar = () => {
    const { sidebarOpened } = useSelector(state => state.app)
    const { user, roles } = useSelector(state => state.auth)

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
            if (sidebarOpened)
                toggle()
        }
    }, [location.pathname])


    const links = [
        {
            name:
                <div className='flex items-center space-x-3'>
                    <MdiHome />
                    <span>Anasayfa</span>
                </div>, href: "/", admin: false
        },
        {
            name:
                <div className='flex items-center space-x-3'>
                    <IcBaselineApartment />
                    <span>Daireler</span>
                </div>, href: "/apartments", admin: true
        },
        {
            name:
                <div className='flex items-center space-x-3'>
                    <IcRoundGroup />
                    <span>Kişiler</span>
                </div>, href: "/users", admin: true
        },
        {
            name: <div className='flex items-center space-x-3'>
                <FluentPayment16Filled />
                <span>Ödemeler</span>
            </div>, href: "/payments", admin: false
        },
        {
            name:
                <div className='flex items-center space-x-3'>
                    <IcRoundMessage />
                    <span>Mesajlar ({user.unread})</span>
                </div>, href: "/messages", admin: false
        },
    ]

    return (
        <div
            className={`${sidebarOpened ? 'translate-x-0' : 'md:translate-x-0 -translate-x-full'} bg-slate-600 fixed flex-col left-0 top-0 h-full  flex w-64  
            duration-150 transition-all p-4 z-50`}>

            <div className='flex justify-between p-3 text-white shadow-lg shadow-black'>
                <h2 className='text-lg uppercase'>Apartman Yönetim Sistemi</h2>
                <button className='md:hidden' onClick={toggle}>
                    <VaadinClose />
                </button>
            </div>

            <ul className='mt-10'>
                {
                    links.map((link, index) => (
                        (!link.admin || link.admin && roles.includes("Admin")) &&
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