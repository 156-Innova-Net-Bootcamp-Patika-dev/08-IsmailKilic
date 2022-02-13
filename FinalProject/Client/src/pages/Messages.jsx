import React, { useState } from 'react'
import SendMessageModal from '../components/Modals/SendMessageModal'

const Messages = () => {
  const [tab, setTab] = useState("inbox")
  const [showModal, setShowModal] = useState(false)

  return (
    <div className='bg-white'>
      <nav className='p-3 bg-slate-600'>
        <h2 className='flex-1 text-lg text-white uppercase'>Mail Kutusu</h2>
        <div className='mt-6 space-x-4 text-white'>
          <button className={`${tab === 'inbox' && 'border-b-2'}`} onClick={() => setTab("inbox")}>Gelen Kutusu</button>
          <button className={`${tab === 'sent' && 'border-b-2'}`} onClick={() => setTab("sent")}>Gönderilenler</button>
        </div>
      </nav>

      <section className='p-4'>
        {tab === 'inbox' && <div className='flex justify-between px-8'>
          <h4>10 Okunmamış Email</h4>
          <button onClick={() => setShowModal(true)} className='button'>Yeni Mesaj</button>
        </div>}

        <SendMessageModal isOpen={showModal} close={() => setShowModal(false)} />

        <ul className='mt-6'>
          {
            Array(16).fill(0).map((val, index) => (
              <li key={index} className={`hover:py-4 w-full py-2.5 group items-center flex-1 border-y flex odd:bg-gray-50 even:bg-gray-200 px-3 cursor-pointer`}>
                <input type="checkbox" />
                <p className='ml-3'>Ad Soyad</p>
                <p className='flex-1 w-0 mx-10 truncate'>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Modi, dignissimos accusamus voluptatum nihil fugiat placeat, quis voluptates error nobis reprehenderit in cupiditate quidem corrupti laborum quae quisquam. Minima, aliquam suscipit?</p>
                <span className='group-hover:invisible'>12 Şubat</span>
                <button className='invisible group-hover:visible'>Sil</button>
              </li>
            ))
          }
        </ul>
      </section>
    </div>
  )
}

export default Messages