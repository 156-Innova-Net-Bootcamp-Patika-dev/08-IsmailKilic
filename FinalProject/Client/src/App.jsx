import React from 'react'
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Routes, Route } from "react-router-dom";
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import AdminHome from './pages/AdminHome';
import ApartmentDetail from './pages/ApartmentDetail';
import Apartments from './pages/Apartments';
import ChangePassword from './pages/ChangePassword';
import Home from './pages/Home';
import Login from './pages/Login';
import Messages from './pages/Messages';
import MyPayments from './pages/MyPayments';
import Profile from './pages/Profile';
import UserPayments from './pages/UserPayments';
import Users from './pages/Users';
import { setAuth, updateRoles, updateUser } from './store/auth';
import axiosClient from './utils/axiosClient';

const App = () => {
  const { authenticated, roles } = useSelector(state => state.auth)
  const dispatch = useDispatch()

  useEffect(async () => {
    try {
      const res = await axiosClient.get("auth/me")
      dispatch(updateRoles(res.data))
      dispatch(updateUser(res.data))
      dispatch(setAuth(true))
    } catch (error) {
      dispatch(setAuth(false))
      localStorage.removeItem("user")
    }
  }, [])


  return (
    <div className="min-h-screen overflow-x-hidden font-mono bg-gradient-to-b from-[#bccde0] to-[#a3b1bb]">
      {
        authenticated === null &&
        <div className="fixed top-0 left-0 right-0 bottom-0 w-full h-screen z-50 overflow-hidden opacity-75 flex flex-col items-center justify-center">
          <div className="loader ease-linear rounded-full border-4 border-t-4 border-gray-200 h-12 w-12 mb-4"></div>
          <h2 className="text-center text-white text-xl font-semibold">Yükleniyor...</h2>
        </div>
      }
      {
        authenticated &&
        <>
          <Sidebar />
          <div className='flex-1 md:ml-64'>
            <Header />
            <div className='p-4'>
              <Routes>
                <Route path="/messages" element={<Messages />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/change-password" element={<ChangePassword />} />
                {
                  roles.includes("Admin") &&
                  <>
                    <Route path="/" element={<AdminHome />} />
                    <Route path="/apartments" element={<Apartments />} />
                    <Route path="/apartments/:id" element={<ApartmentDetail />} />
                    <Route path="/users" element={<Users />} />
                    <Route path="/payments" element={<UserPayments />} />
                  </>
                }
                {
                  roles.includes("User") &&
                  <>
                    <Route path="/" element={<Home />} />
                    <Route path="/payments" element={<MyPayments />} />
                  </>
                }
              </Routes>
            </div>
          </div>
        </>
      }

      {
        authenticated === false &&
        <div className='flex items-center justify-center flex-1 p-4'>
          <Routes>
            <Route path="*" element={<Login />} />
          </Routes>
        </div>
      }
    </div>
  )
}

export default App