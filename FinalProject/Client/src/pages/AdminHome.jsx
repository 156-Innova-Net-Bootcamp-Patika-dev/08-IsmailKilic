import React, { useEffect, useState } from 'react'
import MyDataTable from '../components/MyDataTable'
import axiosClient from '../utils/axiosClient'
import { invoiceTypes } from './ApartmentDetail'

const AdminHome = () => {
    const [data, setData] = useState([])
    const [totalDebt, setTotalDebt] = useState(0)
    const [totalPaid, setTotalPaid] = useState(0)
    const [filteredData, setFilteredData] = useState([])

    const [year, setYear] = useState("")
    const [month, setMonth] = useState("")
    const [type, setType] = useState("")

    // get all invoices
    useEffect(async () => {
        const res = await axiosClient.get("invoices/all")
        setData(res.data);
        setFilteredData(res.data);
    }, [])

    // when change filter inputs, filter data
    useEffect(() => {
        if (data.length === 0) return
        filter()
    }, [month, year, type])

    // when change filteredData calculate total prices
    useEffect(() => {
        calculateTotals()
    }, [filteredData])

    // filter data by year,month,type
    const filter = () => {
        setFilteredData(data.filter(x =>
            (year ? x.year == year : true) &&
            (month ? x.month == month : true) &&
            (type ? x.invoiceType == type : true)
        ))
    }

    const calculateTotals = () => {
        let paid = 0
        filteredData.filter(x => x.isPaid).map(d => {
            paid += d.price
        })
        setTotalPaid(paid)

        let debt = 0
        filteredData.filter(x => !x.isPaid).map(d => {
            debt += d.price
        })
        setTotalDebt(debt)
    }

    const columns = [
        { name: 'Id', selector: row => row.id, maxWidth: '10px' },
        { name: 'Daire Id', selector: row => row.apartment.id, maxWidth: '10px' },
        { name: 'Fatura Tipi', selector: row => invoiceTypes[row.invoiceType], },
        { name: 'Dönem', selector: row => `${row.month}/${row.year}`, },
        { name: 'Fiyat', selector: row => row.price + ' TL', },
        { name: 'Ödendi', selector: row => row.isPaid ? 'Evet' : 'Hayır' },
    ];

    return (
        <div className='max-w-6xl mx-auto pt-10'>

            <div className='mb-3 flex justify-between'>
                <div>
                    <h4>Filtrele</h4>
                    <div className='flex space-x-2'>
                        <input min={2000} value={year} onChange={e => setYear(e.target.value)} type="number" placeholder='Yıl' className='w-20 input' />
                        <input min={1} max={12} value={month} onChange={e => setMonth(e.target.value)} type="number" placeholder='Ay' className='w-20 input' />
                        <select value={type} onChange={e => setType(e.target.value)} className="input">
                            <option value="">Fatura tipi</option>
                            <option value={0}>Aidat</option>
                            <option value={1}>Elektrik</option>
                            <option value={2}>Su</option>
                            <option value={3}>Doğalgaz</option>
                        </select>
                    </div>
                </div>
                <ul className='text-right'>
                    <li>Toplam borç: {totalDebt} TL</li>
                    <li>Toplam ödenen: {totalPaid} TL</li>
                </ul>
            </div>

            <MyDataTable data={filteredData} columns={columns} />
        </div>
    )
}

export default AdminHome