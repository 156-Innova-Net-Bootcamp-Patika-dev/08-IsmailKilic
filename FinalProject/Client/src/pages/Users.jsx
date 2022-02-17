import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import AddUserModal from '../components/Modals/AddUserModal'
import MyDataTable from '../components/MyDataTable'
import { toggleUserModal } from '../store/app'
import axiosClient from '../utils/axiosClient'

const Users = () => {
  const [data, setData] = useState([])
  const dispatch = useDispatch()

  useEffect(async () => {
    const res = await axiosClient.get("admin/users")
    setData(res.data);
  }, [])

  const openModal = () => {
    dispatch(toggleUserModal())
  }

  const columns = [
    { name: 'Id', selector: row => row.id, maxWidth: '10px' },
    { name: 'Ad Soyad', selector: row => row.fullname, },
    { name: 'TC No', selector: row => row.tcNo, },
    { name: 'Email', selector: row => row.email, },
    { name: 'Kullanıcı Adı', selector: row => row.userName, },
    { name: 'Telefon', selector: row => row.phoneNumber, },
    { name: 'Araç Plaka', selector: row => row.licenseNo, },
  ];

  return (
    <div className='w-full mx-auto md:w-5/6'>
      <div className='flex justify-between my-5'>
        <h2 className='text-xl uppercase'>Kişiler</h2>
        <button onClick={openModal} className='button'>Yeni Kişi Ekle</button>
      </div>

      <AddUserModal />

      <MyDataTable
        columns={columns}
        data={data}
      />
    </div>
  )
}

export default Users