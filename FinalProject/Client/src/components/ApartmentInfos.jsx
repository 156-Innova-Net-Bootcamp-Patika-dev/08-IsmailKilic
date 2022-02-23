import React from 'react'
import { useNavigate } from 'react-router-dom'
import { ownerTypes } from '../pages/ApartmentDetail'
import axiosClient from '../utils/axiosClient'

const ApartmentInfos = ({ data }) => {
    const navigate = useNavigate()

    const handleDelete = async () => {
        if (confirm("Bu daireyi silmek istediğinize emin misiniz?")) {
            try {
                const res = await axiosClient.delete(`apartments/${data.id}`)
                alert("Silme başarılı")
                navigate("/apartments")
            } catch ({ response }) {
                alert(response.data.errors)
            }
        }
    }
    return (
        <div className='grow'>
            <h2 className='mb-3 text-lg'>Daire Bilgileri</h2>
            <ul>
                <li>Blok: {data?.block}</li>
                <li>Kat: {data?.floor}</li>
                <li>Daire No: {data?.no}</li>
                <li>Daire Tipi: {data?.type}</li>
                <li>Durum: {data?.isFree ? "Boş" : "Dolu"}</li>
                <li>{ownerTypes[data?.ownerType]}</li>
                <li className='flex mt-3 space-x-3'>
                    <button className='button bg-green-600'>Güncelle</button>
                    {!data?.user && data?.invoices?.length === 0 &&
                        <button onClick={handleDelete} className='button bg-red-600'>Sil</button>}
                </li>
            </ul>
        </div>
    )
}

export default ApartmentInfos