import React from 'react'
import { useSelector } from 'react-redux';
import { Routes, Route, Link } from "react-router-dom";
import Header from './components/Header';
import Sidebar from './components/Sidebar';
import Apartments from './pages/Apartments';
import Home from './pages/Home';
import Login from './pages/Login';
import Messages from './pages/Messages';
import Users from './pages/Users';

const App = () => {
  const { authenticated } = useSelector(state => state.auth)

  return (
    <div className="min-h-screen flex font-mono bg-gradient-to-b from-[#bccde0] to-[#a3b1bb]">
      {
        authenticated
          ?
          <>
            <Sidebar />
            <div className='md:ml-64 flex-1'>
              <Header />
              <div className='p-4'>
                <Routes>
                  <Route path="/" element={<Home />} />
                  <Route path="/apartments" element={<Apartments />} />
                  <Route path="/users" element={<Users />} />
                  <Route path="/messages" element={<Messages />} />
                </Routes>
              </div>
            </div>
          </>
          :
          <div className='p-4 flex flex-1 justify-center items-center'>
            <Routes>
              <Route path="*" element={<Login />} />
            </Routes>
          </div>
      }
    </div>
  )
}

export default App