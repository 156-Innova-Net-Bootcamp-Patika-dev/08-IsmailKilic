import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import AddApartmentModal from '../components/Modals/AddApartmentModal'
import AssignUserModal from '../components/Modals/AssignUserModal'
import Table from '../components/Table'
import { toggleAptModal } from '../store/app'
import axiosClient from '../utils/axiosClient'

const Apartments = () => {
    const [data, setData] = useState([])
    const [showAssignUserModal, setShowAssignUserModal] = useState(false)
    const [assignId, setAssignId] = useState(0)

    const dispatch = useDispatch()

    const openModal = () => {
        dispatch(toggleAptModal())
    }

    useEffect(async () => {
        const res = await axiosClient.get("apartments")
        setData(res.data);
    }, [])

    const openAssignModal = (id) => {
        setAssignId(id)
        setShowAssignUserModal(true)
    }

    const handleRemoveUser = async (id) => {
        try {
            const res = await axiosClient.post("apartments/remove-user", {
                apartmentId: id
            })

            const datas = [...data]
            const index = datas.findIndex(x => x.id === id)
            datas[index] = res.data
            setData(datas)
        } catch (error) {

        }
    }

    const updateData = (newData) => {
        const datas = [...data]
        const index = datas.findIndex(x => x.id === newData.id)
        datas[index] = newData
        setData(datas)
    }

    const addData = (newData) => {
        setData([...data, newData]);
    }

    const titles = ["Id", "Daire No", "Blok", "Durum", "Kat", "Tip", "Sahibi", ""]

    return (
        <div className='w-full mx-auto md:w-5/6'>
            <div className='flex justify-between mt-5'>
                <h2 className='text-xl uppercase'>Daireler</h2>
                <button onClick={openModal} className='button'>Yeni Daire Ekle</button>
            </div>

            <AddApartmentModal addData={addData} />
            <AssignUserModal updateData={updateData} isOpen={showAssignUserModal} id={assignId} close={() => setShowAssignUserModal(false)} />

            <Table titles={titles} >
                {
                    data.map((d, index) => (
                        <tr key={index} className='h-10 odd:bg-white odd:text-gray-700 even:bg-[#F3F3F3]'>
                            <td>{d.id}</td>
                            <td>{d.no}</td>
                            <td>{d.block}</td>
                            <td>{d.isFree ? "Boş" : "Dolu"}</td>
                            <td>{d.floor}</td>
                            <td>{d.type}</td>
                            <td>{d.user?.userName}</td>
                            <td>
                                {d.isFree ?
                                    <button onClick={() => openAssignModal(d.id)} className='text-sm bg-green-600 button'>Kişi Ata</button>
                                    : <button onClick={() => handleRemoveUser(d.id)} className='text-sm bg-red-600 button'>Kişiyi Kaldır</button>}
                                <button className='ml-2 text-sm button'>Fatura Ekle</button>
                            </td>
                        </tr>
                    ))
                }
            </Table>
        </div>
    )
}

export default Apartments