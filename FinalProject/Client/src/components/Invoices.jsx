import React from 'react'
import { invoiceTypes } from '../pages/ApartmentDetail';
import axiosClient from '../utils/axiosClient';
import MyDataTable from './MyDataTable';

const Invoices = ({ invoices }) => {
    const paymentMethods = [{
        supportedMethods: ['basic-card']
    }];

    async function satinAl(item) {
        const paymentDetails = {
            total: {
                label: 'Toplam',
                amount: {
                    currency: 'TRY',
                    value: parseFloat(item.price)
                }
            }
        };

        const request = new PaymentRequest(paymentMethods, paymentDetails);
        const paymentResponse = await request.show();
        await paymentResponse.complete();

        const cardNumber = paymentResponse.details.cardNumber
        try {
            const res = await axiosClient.post(`${import.meta.env.VITE_APP_PAYMENT_API}payments`, {
                invoiceId: item.id,
                apartmentId: item.apartment.id,
                last4Number: cardNumber.slice(cardNumber.length - 4),
                price: item.price
            })
            alert("Ödeme başarılı");
        } catch (err) {
            alert("Ödeme hatalı");
        }
    }

    const columns = [
        { name: 'Id', selector: row => row.id, maxWidth: '10px' },
        { name: 'Fatura Tipi', selector: row => invoiceTypes[row.invoiceType], },
        { name: 'Dönem', selector: row => `${row.month}/${row.year}`, },
        { name: 'Fiyat', selector: row => row.price + ' TL', },
        {
            name: '', minWidth: '140px', selector: row =>
                <div>
                    <button
                        onClick={() => satinAl(row)}
                        className='inline h-10 px-3 my-1 text-white bg-blue-600 rounded-sm hover:bg-blue-700'>
                        Ödeme Yap
                    </button>
                </div>
        },
    ];

    return (
        <div>
            <h2 className='text-lg text-black'>Faturalarım</h2>
            <MyDataTable columns={columns} data={invoices.filter(x => !x.isPaid)} />
        </div>
    )
}

export default Invoices