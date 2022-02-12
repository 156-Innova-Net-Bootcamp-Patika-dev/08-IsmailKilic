import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import AddApartmentModal from '../components/Modals/AddApartmentModal'
import Table from '../components/Table'
import { toggleAptModal } from '../store/app'
import axiosClient from '../utils/axiosClient'

const Apartments = () => {
    const [data, setData] = useState([])
    const dispatch = useDispatch()

    const openModal = () => {
        dispatch(toggleAptModal())
    }

    useEffect(async () => {
        const res = await axiosClient.get("apartments")
        setData(res.data);
    }, [])

    const titles = ["No", "Blok", "Durum", "Kat", "Tip", ""]

    return (
        <div className='w-full md:w-5/6 mx-auto'>
            <div className='flex justify-between mt-5'>
                <h2 className='uppercase text-xl'>Daireler</h2>
                <button onClick={openModal} className='button'>Yeni Daire Ekle</button>
            </div>

            <AddApartmentModal />

            <Table titles={titles} >
                {
                    data.map((d, index) => (
                        <tr key={index} className='h-10 odd:bg-white odd:text-gray-700 even:bg-[#F3F3F3]'>
                            <td>{d.no}</td>
                            <td>{d.block}</td>
                            <td>{d.isFree ? "Boş" : "Dolu"}</td>
                            <td>{d.floor}</td>
                            <td>{d.type}</td>
                            <td>
                                {d.isFree && <button className='button text-sm'>Kişi Ata</button>}
                            </td>
                        </tr>
                    ))
                }
            </Table>
        </div>
    )
}

export default Apartments