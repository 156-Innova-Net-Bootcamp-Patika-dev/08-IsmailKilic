import React, { useEffect, useState } from 'react'
import axiosClient from '../utils/axiosClient'
import MyDataTable from '../components/MyDataTable';
import moment from 'moment/min/moment-with-locales';
import { invoiceTypes } from './ApartmentDetail';

moment.locale("tr")
const columns = [
    {
        name: 'Daire Id',
        selector: row => row.apartmentId,
        maxWidth: '10px'
    },
    {
        name: 'Fatura Tipi',
        selector: row => invoiceTypes[row.invoice.invoiceType],
    },
    {
        name: 'Dönem',
        selector: row => row.invoice.month + '/' + row.invoice.year,
    },
    {
        name: 'Ödemeyi Yapan',
        selector: row => row.user.userName,
    },
    {
        name: 'Ödenilen Kredi Kartı',
        selector: row => `**** **** **** ${row.last4Number}`,
        minWidth: '200px'
    },
    {
        name: 'Fiyat',
        selector: row => `${row.price} TL`,
    },
    {
        name: 'Ödeme Tarihi',
        selector: row => moment(row.createdAt).format("MMM DD H:mm"),
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
            <h2 className='text-xl uppercase my-5'>Ödemelerim</h2>

            <MyDataTable
                columns={columns}
                data={payments} />
        </div>
    )
}

export default MyPayments