import React, { useEffect, useState } from 'react'
import axiosClient from '../utils/axiosClient'
import DataTable from 'react-data-table-component';

const columns = [
    {
        name: 'Daire Id',
        selector: row => row.apartmentId,
    },
    {
        name: 'Fatura Id',
        selector: row => row.invoiceId,
    },
    {
        name: 'Ödenilen Kredi Kartı',
        selector: row => `**** **** **** ${row.last4Number}`,
    },
    {
        name: 'Fiyat',
        selector: row => `${row.price} TL`,
    },
    {
        name: 'Ödeme Tarihi',
        selector: row => row.createdAt,
    },
];

const MyPayments = () => {
    const [payments, setPayments] = useState([])

    useEffect(async () => {
        const res = await axiosClient.get(`${import.meta.env.VITE_APP_PAYMENT_API}payments`);
        setPayments(res.data);
    }, [])


    return (
        <div className='max-w-5xl mx-auto pt-10'>
            <DataTable
                striped
                responsive
                pagination
                title="Ödemelerim"
                highlightOnHover
                dense
                columns={columns}
                data={payments}
            />
        </div>
    )
}

export default MyPayments