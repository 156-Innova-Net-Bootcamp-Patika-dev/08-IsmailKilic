import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import SendMessageModal from '../components/Modals/SendMessageModal'
import axiosClient from '../utils/axiosClient'
import moment from 'moment/min/moment-with-locales';

moment.locale("tr")

const Messages = () => {
  const [tab, setTab] = useState("inbox")
  const [showModal, setShowModal] = useState(false)
  const [messages, setMessages] = useState([])
  const [renderedMessages, setRenderedMessages] = useState([])
  const { user } = useSelector(state => state.auth)
  const [unRead, setUnRead] = useState(0)

  useEffect(async () => {
    const res = await axiosClient.get("messages");
    setMessages(res.data);
  }, [])

  useEffect(() => {
    if (messages.length === 0) return

    if (tab === "inbox") {
      setRenderedMessages(messages.filter(x => x.receiver.id === user.id));
      setUnRead(messages.filter(x => x.receiver.id === user.id && x.isRead === false).length);
    } else {
      setRenderedMessages(messages.filter(x => x.sender.id === user.id));
    }
  }, [tab, messages])



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
          <h4>{unRead} Okunmamış Email</h4>
          <button onClick={() => setShowModal(true)} className='button'>Yeni Mesaj</button>
        </div>}

        <SendMessageModal isOpen={showModal} close={() => setShowModal(false)} />

        <ul className='mt-6'>
          {
            renderedMessages.map((msg) => (
              <li key={msg.id} className={`${msg.isRead ? "bg-gray-200" : "bg-gray-50"} hover:py-4 w-full py-2.5 group items-center flex-1 border-y flex px-3 cursor-pointer`}>
                <input type="checkbox" />
                <p className='ml-3'>{tab === "inbox" ? msg.sender.userName : msg.receiver.userName}</p>
                <p className='flex-1 w-0 mx-10 truncate'>{msg.content}</p>
                <span className='group-hover:invisible'>{moment(msg.createdAt).format("lll")}</span>
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