import React, { useEffect, useState } from 'react'
import { useDispatch } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import AddApartmentModal from '../components/Modals/AddApartmentModal'
import AssignUserModal from '../components/Modals/AssignUserModal'
import { toggleAptModal } from '../store/app'
import axiosClient from '../utils/axiosClient'
import IcSharpAdd from '../components/Icons/IcSharpAdd'
import MdiDelete from '../components/Icons/MdiDelete'
import MyDataTable from '../components/MyDataTable'
import CreateManyInvoice from '../components/Modals/CreateManyInvoice'

const Apartments = () => {
    const [data, setData] = useState([])
    const [showAssignUserModal, setShowAssignUserModal] = useState(false)
    const [showInvoice, setShowInvoice] = useState(false)
    const [assignId, setAssignId] = useState(0)
    const [selectedRows, setSelectedRows] = useState([]);

    const dispatch = useDispatch()
    const navigate = useNavigate()

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
        let conf = confirm("Bu daireyi boşaltmak istediğinize emin misiniz?")
        if (!conf) return

        try {
            const res = await axiosClient.post("apartments/remove-user", {
                apartmentId: id
            })

            const datas = [...data]
            const index = datas.findIndex(x => x.id === id)
            datas[index] = res.data
            setData(datas)
        } catch (err) {
            alert(err.response.data.errors)
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

    const goDetail = (id) => {
        navigate(`/apartments/${id}`);
    }

    const handleChange = ({ selectedRows }) => {
        setSelectedRows(selectedRows.map(x => x.id));
    };

    const columns = [
        { name: 'Id', selector: row => row.id, maxWidth: '10px' },
        { name: 'Daire No', selector: row => row.no, maxWidth: '10px' },
        { name: 'Blok', selector: row => row.block, },
        { name: 'Durum', selector: row => row.isFree ? 'Boş' : 'Dolu', },
        { name: 'Kat', selector: row => row.floor, },
        { name: 'Tip', selector: row => row.type, },
        { name: 'Sahibi', selector: row => row.user?.userName, },
        {
            name: '', minWidth: '150px',
            selector: row =>
                <div className='flex items-center'>
                    {row.isFree ?
                        <button onClick={() => openAssignModal(row.id)} className='text-lg bg-green-600 button'>
                            <IcSharpAdd />
                        </button>
                        : <button onClick={() => handleRemoveUser(row.id)} className='text-lg bg-red-600 button'>
                            <MdiDelete />
                        </button>}
                    <button onClick={() => goDetail(row.id)} className='ml-2 text-sm button'>Detay</button>
                </div>,
        },
    ];

    return (
        <div className='w-full mx-auto md:w-5/6'>
            <div className='flex justify-between my-5'>
                <h2 className='text-xl uppercase'>Daireler</h2>
                <button onClick={openModal} className='button'>Yeni Daire Ekle</button>
            </div>

            {
                selectedRows.length > 0 &&
                <div className='my-2 flex justify-end'>
                    <button onClick={() => setShowInvoice(true)} className='button'>Toplu Fatura Ekle</button>
                </div>
            }

            <AddApartmentModal addData={addData} />
            <CreateManyInvoice isOpen={showInvoice} ids={selectedRows} close={() => setShowInvoice(false)} />
            <AssignUserModal updateData={updateData} isOpen={showAssignUserModal} id={assignId} close={() => setShowAssignUserModal(false)} />

            <MyDataTable
                selectableRows
                onSelectedRowsChange={handleChange}
                columns={columns}
                data={data}
            />
        </div>
    )
}

export default Apartments