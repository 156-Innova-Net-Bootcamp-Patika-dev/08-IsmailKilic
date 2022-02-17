import React from 'react'
import { invoiceTypes } from '../pages/ApartmentDetail';

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

        console.log(paymentResponse);
    }

    return (
        <div className='p-2 bg-gray-700 grow'>

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
                                onClick={()=>satinAl(item)}
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