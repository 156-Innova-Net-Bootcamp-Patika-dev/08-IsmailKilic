import React from 'react'
import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Routes, Route, Link } from "react-router-dom";
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import ApartmentDetail from './pages/ApartmentDetail';
import Apartments from './pages/Apartments';
import ChangePassword from './pages/ChangePassword';
import Home from './pages/Home';
import Login from './pages/Login';
import Messages from './pages/Messages';
import Profile from './pages/Profile';
import Users from './pages/Users';
import { setAuth } from './store/auth';
import parseJwt from './utils/parseJwt'

const App = () => {
  const { authenticated, roles, token } = useSelector(state => state.auth)
  const dispatch = useDispatch()

  useEffect(() => {
    const data = parseJwt(token);
    let isAuth = new Date(data.exp * 1000) > new Date();

    if (isAuth) dispatch(setAuth(true))
    else dispatch(setAuth(false))
  }, [])


  return (
    <div className="min-h-screen overflow-x-hidden flex font-mono bg-gradient-to-b from-[#bccde0] to-[#a3b1bb]">
      {
        authenticated
          ?
          <>
            <Sidebar />
            <div className='flex-1 md:ml-64'>
              <Header />
              <div className='p-4'>
                <Routes>
                  <Route path="/" element={<Home />} />
                  <Route path="/messages" element={<Messages />} />
                  <Route path="/profile" element={<Profile />} />
                  <Route path="/change-password" element={<ChangePassword />} />
                  {
                    roles.includes("Admin") &&
                    <>
                      <Route path="/apartments" element={<Apartments />} />
                      <Route path="/apartments/:id" element={<ApartmentDetail />} />
                      <Route path="/users" element={<Users />} />
                    </>
                  }
                </Routes>
              </div>
            </div>
          </>
          :
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