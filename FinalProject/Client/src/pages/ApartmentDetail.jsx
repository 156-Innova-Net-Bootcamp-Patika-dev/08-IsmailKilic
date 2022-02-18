import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import axiosClient from '../utils/axiosClient'
import CreateInvoiceModal from '../components/Modals/CreateInvoiceModal'
import MyDataTable from '../components/MyDataTable';

export const invoiceTypes = ["Aidat", "Elektrik", "Su", "Doğalgaz"]

const ApartmentDetail = () => {
    let { id } = useParams();
    const [data, setData] = useState({})
    const [year, setYear] = useState("")
    const [invoices, setInvoices] = useState([])
    const [month, setMonth] = useState("")
    const [type, setType] = useState("")
    const [filteredInvoice, setfilteredInvoice] = useState([])
    const [showInvoiceModal, setShowInvoiceModal] = useState(false)

    useEffect(async () => {
        const res = await axiosClient.get(`apartments/${id}`)
        setData(res.data);
        setInvoices(res.data.invoices)
        setfilteredInvoice(res.data.invoices);
    }, [])

    useEffect(() => {
        if (invoices.length === 0) return
        console.log("asd");
        filter()
    }, [month, year, type])

    const filter = () => {
        setfilteredInvoice(invoices.filter(x =>
            (year ? x.year == year : true) &&
            (month ? x.month == month : true) &&
            (type ? x.invoiceType == type : true)
        ))
    }

    const openInvoiceModal = () => {
        setShowInvoiceModal(true)
    }

    const columns = [
        { name: 'Id', selector: row => row.id, maxWidth: '10px' },
        { name: 'Fatura Tipi', selector: row => invoiceTypes[row.invoiceType], },
        { name: 'Dönem', selector: row => `${row.month}/${row.year}`, },
        { name: 'Fiyat', selector: row => row.price + ' TL', },
        { name: 'Ödendi', selector: row => row.isPaid ? 'Evet' : 'Hayır' },
    ];

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

            <div className='max-w-5xl mx-auto'>
                <div className='flex items-center'>
                    <h3 className='my-4 text-xl'>Faturalar</h3>
                    <button onClick={openInvoiceModal} className='h-10 ml-2 text-sm button'>Fatura Ekle</button>
                </div>

                <div className='mb-4'>
                    <h4>Filtrele</h4>
                    <div className='flex space-x-2'>
                        <input min={2000} value={year} onChange={e => setYear(e.target.value)} type="number" placeholder='Yıl' className='w-20 input' />
                        <input min={1} max={12} value={month} onChange={e => setMonth(e.target.value)} type="number" placeholder='Ay' className='w-20 input' />
                        <select value={type} onChange={e => setType(e.target.value)} className="w-40 input">
                            <option value="">Fatura tipi</option>
                            <option value={0}>Aidat</option>
                            <option value={1}>Elektrik</option>
                            <option value={2}>Su</option>
                            <option value={3}>Doğalgaz</option>
                        </select>
                    </div>
                </div>

                <MyDataTable
                    columns={columns}
                    data={filteredInvoice} />
            </div>

        </div>
    )
}

export default ApartmentDetail