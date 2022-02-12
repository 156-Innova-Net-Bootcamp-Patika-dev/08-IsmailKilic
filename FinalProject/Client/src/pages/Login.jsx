import React from 'react'
import { useDispatch } from 'react-redux'
import { loginSuccess } from '../store/auth'

const Login = () => {
    const dispatch = useDispatch()

    const handleLogin = e => {
        e.preventDefault()
        dispatch(loginSuccess())
    }

    return (
        <form onClick={handleLogin}>
            <h1 className='mb-10 text-center uppercase text-2xl font-semibold'>Giriş Yap</h1>
            <input type="text" className='input' />
            <input type="password" className='input' />
            <button type="submit" className='input bg-green-600 cursor-pointer'>
                Giriş Yap
            </button>
        </form>
    )
}

export default Login