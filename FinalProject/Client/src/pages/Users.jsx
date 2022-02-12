import React from 'react'
import { useDispatch } from 'react-redux'
import AddUserModal from '../components/Modals/AddUserModal'
import { toggleUserModal } from '../store/app'

const Users = () => {
  const dispatch = useDispatch()

  const openModal = () => {
    dispatch(toggleUserModal())
  }

  return (
    <div className='w-full md:w-5/6 mx-auto'>
      <div className='flex justify-between mt-5'>
        <h2 className='uppercase text-xl'>Kişiler</h2>
        <button onClick={openModal} className='button'>Yeni Kişi Ekle</button>
      </div>

      <AddUserModal />
    </div>
  )
}

export default Users