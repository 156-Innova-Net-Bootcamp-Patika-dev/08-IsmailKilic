import React from 'react'
import { useState } from 'react';
import { invoiceTypes } from '../pages/ApartmentDetail';
import PaymentModal from './Modals/PaymentModal';

const Invoices = ({ invoices }) => {
    const [show, setShow] = useState(false)

    return (
        <div className='p-2 bg-gray-700 grow'>
            <PaymentModal isOpen={show} close={() => setShow(false)} />

            <h2 className='text-lg'>Faturalarım</h2>
            {
                invoices.length === 0 ? <div>Ödenmemiş faturanız bulunmamaktadır.</div> :
                    invoices.filter(x => x.isPaid === false).map(item => (
                        <div key={item.id} className='flex items-center px-3 py-1 mt-2 border'>
                            <ul className='flex-1'>
                                <li>Fatura tipi: {invoiceTypes[item.invoiceType]}</li>
                                <li>Dönem: {item.month}/{item.year}</li>
                                <li>Fiyat: {item.price} TL</li>
                            </ul>

                            <button
                                onClick={() => setShow(true)}
                                className='inline h-10 px-3 text-gray-700 bg-white rounded-sm hover:bg-gray-200'>
                                Ödeme Yap
                            </button>
                        </div>
                    ))
            }
        </div>
    )
}

export default Invoices