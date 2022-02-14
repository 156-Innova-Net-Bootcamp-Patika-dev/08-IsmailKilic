import React, { useEffect, useState } from 'react'
import axiosClient from '../utils/axiosClient';
import { invoiceTypes } from './ApartmentDetail';

const Home = () => {
  const [invoices, setInvoices] = useState([])
  const [apartments, setApartments] = useState([])

  useEffect(async () => {
    const res = await axiosClient.get("invoices")
    setInvoices(res.data);
    
    const res2 = await axiosClient.get(`apartments?byUser=1`)
    setApartments(res2.data);
  }, [])

  return (
    <div className='flex flex-col space-y-2 text-white md:space-y-0 md:space-x-4 md:flex-row'>
      <div className='p-2 bg-gray-700 grow'>
        <h2 className='text-lg'>Dairelerim</h2>
        {
          apartments.length === 0 ? <div>Daireniz bulunmamaktadır.</div> :
            apartments.map(item => (
              <div key={item.id} className='p-1 mt-2 border'>
                <ul>
                  <li>{item.block} Blok</li>
                  <li>{item.floor}. Kat No: {item.no}</li>
                  <li>Daire Tipi: {item.type}</li>
                </ul>
              </div>
            ))
        }
      </div>

      <div className='p-2 bg-gray-700 grow'>
        <h2 className='text-lg'>Faturalarım</h2>
        {
          invoices.length === 0 ? <div>Ödenmemiş faturanız bulunmamaktadır.</div> :
            invoices.filter(x => x.isPaid === false).map(item => (
              <div key={item.id} className='p-1 mt-2 border'>
                <ul>
                  <li>Fatura tipi: {invoiceTypes[item.invoiceType]}</li>
                  <li>Dönem: {item.month}/{item.year}</li>
                  <li>Fiyat: {item.price} TL</li>
                </ul>
              </div>
            ))
        }
      </div>
    </div>
  )
}

export default Home