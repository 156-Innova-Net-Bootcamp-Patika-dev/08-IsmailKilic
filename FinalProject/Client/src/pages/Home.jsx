import React, { useEffect, useState } from 'react'
import axiosClient from '../utils/axiosClient';
import { invoiceTypes } from './ApartmentDetail';

const Home = () => {
  const [invoices, setInvoices] = useState([])
  const [apartments, setApartments] = useState([])

  useEffect(async () => {
    const res = await axiosClient.get("invoices")
    setInvoices(res.data);
  }, [])

  return (
    <div className='flex space-x-4 text-white'>
      <div className='p-2 bg-gray-700 grow'>
        <h2 className='text-lg'>Dairelerim</h2>
      </div>

      <div className='p-2 bg-gray-700 grow'>
        <h2 className='text-lg'>Faturalarım</h2>
        {
          invoices.length === 0 ? <div>Faturanız bulunmamaktadır.</div> : 
          invoices.filter(x=>x.isPaid === false).map(item => (
            <div key={item.id} className='p-1 mt-2 border'>
              <ul>
                <li>Fatura tipi: {invoiceTypes[item.invoiceType]}</li>
                <li>Dönem: {item.month}/{item.year}</li>
                <li>Fiyat: {item.price}</li>
              </ul>
            </div>
          ))
        }
      </div>
    </div>
  )
}

export default Home