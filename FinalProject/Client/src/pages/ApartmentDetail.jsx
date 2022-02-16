import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import axiosClient from '../utils/axiosClient'
import CreateInvoiceModal from '../components/Modals/CreateInvoiceModal'

export const invoiceTypes = ["Aidat", "Elektrik", "Su", "Doğalgaz"]

const ApartmentDetail = () => {
    let { id } = useParams();
    const [data, setData] = useState({})
    const [year, setYear] = useState("")
    const [month, setMonth] = useState("")
    const [invoiceType, setInvoiceType] = useState(-1)
    const [filteredInvoice, setfilteredInvoice] = useState([])
    const [showInvoiceModal, setShowInvoiceModal] = useState(false)

    useEffect(async () => {
        const res = await axiosClient.get(`apartments/${id}`)
        setData(res.data);
        setfilteredInvoice(res.data.invoices);
    }, [])

    useEffect(() => {
        if (!data || data.invoices?.length === 0) return
        month && filter("month", month)
    }, [month])

    useEffect(() => {
        if (!data || data.invoices?.length === 0) return
        year && filter("year", year)
    }, [year])

    useEffect(() => {
        if (!data || data.invoices?.length === 0) return
        invoiceType >= 0 && filter("invoiceType", invoiceType)
    }, [invoiceType])

    const filter = (key, val) => {
        setfilteredInvoice(data.invoices?.filter(x => x[key] == val))
    }

    const openInvoiceModal = () => {
        setShowInvoiceModal(true)
    }

    return (
        <div>
            {/* apartment infos */}
            <div className='flex w-full p-2 text-white bg-gray-600'>
                <div className='grow'>
                    <h2 className='mb-3 text-lg'>Daire Bilgileri</h2>
                    <ul>
                        <li>Blok: {data?.block}</li>
                        <li>Kat: {data?.floor}</li>
                        <li>Daire No: {data?.no}</li>
                        <li>Daire Tipi: {data?.type}</li>
                        <li>Durum: {data?.isFree ? "Boş" : "Dolu"}</li>
                    </ul>
                </div>
                <div className='grow'>
                    <h2 className='mb-3 text-lg'>Ev Sahibi Bilgileri</h2>
                    {
                        data?.user ?
                            <div>
                                <ul>
                                    <li>Ad Soyad: {data?.user?.fullName}</li>
                                    <li>Kullanıcı Adı: {data?.user?.userName}</li>
                                    <li>Email: {data?.user?.email}</li>
                                    <li>Telefon No: {data?.user?.phoneNumber}</li>
                                    <li>TC No: {data?.user?.tcNo}</li>
                                    <li>Araç Plaka: {data?.user?.licenseNo}</li>
                                </ul>
                            </div> :
                            <div>Daire boş durumda</div>
                    }
                </div>
            </div>

            <CreateInvoiceModal id={id} isOpen={showInvoiceModal} close={() => setShowInvoiceModal(false)} />

            <div className='flex items-center'>
                <h3 className='my-4 text-xl'>Faturalar</h3>
                <button onClick={openInvoiceModal} className='h-10 ml-2 text-sm button'>Fatura Ekle</button>
            </div>
            
            <div>
                <h4>Filtrele</h4>
                <div className='flex space-x-2'>
                    <input min={2000} max={2022} value={year} type="number" onChange={e => setYear(e.target.value)} className='px-2' placeholder='Yıl' />
                    <input min={1} max={12} value={month} type="number" onChange={e => setMonth(e.target.value)} className='px-2' placeholder='Ay' />
                    <select value={invoiceType} onChange={e => setInvoiceType(e.target.value)}>
                        <option value={-1}>Fatura tipini seçiniz...</option>
                        {
                            invoiceTypes.map((type, index) => (
                                <option key={index} value={index}>{type}</option>
                            ))
                        }
                    </select>
                </div>
            </div>

            <div>
                <ul>
                    {
                        filteredInvoice?.map(item => (
                            <li key={item.id} className="flex flex-col p-2 mt-2 bg-white rounded-sm">
                                <span>Fatura Tipi: {invoiceTypes[item.invoiceType]}</span>
                                <span>Yıl: {item.year}</span>
                                <span>Ay: {item.month}</span>
                                <span>Fiyat: {item.price}</span>
                                <span>Ödendi: {item.isPaid ? "Evet" : "Hayır"}</span>
                            </li>
                        ))
                    }
                </ul>
            </div>

        </div>
    )
}

export default ApartmentDetail