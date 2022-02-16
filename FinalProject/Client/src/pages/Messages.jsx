import React, { useEffect, useState } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import SendMessageModal from '../components/Modals/SendMessageModal'
import axiosClient from '../utils/axiosClient'
import moment from 'moment/min/moment-with-locales';
import MessageDetailModal from '../components/Modals/MessageDetailModal';
import { updateUser } from '../store/auth'
import MdiDelete from '../components/Icons/MdiDelete';

moment.locale("tr")

const Messages = () => {
  const [tab, setTab] = useState("inbox")
  const [showModal, setShowModal] = useState(false)
  const [messages, setMessages] = useState([])
  const [renderedMessages, setRenderedMessages] = useState([])
  const { user } = useSelector(state => state.auth)
  const [unRead, setUnRead] = useState(0)
  const [showMessageDetail, setShowMessageDetail] = useState(false)
  const [currentMessage, setCurrentMessage] = useState(0)
  const dispatch = useDispatch()

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

  const addMessage = (data) => {
    setMessages([data, ...messages])
  }

  const openMessageDetail = (id) => {
    setCurrentMessage(id)
    setShowMessageDetail(true)
  }

  const updateData = (data) => {
    const index = messages.findIndex(x => x.id === data.id)
    if (messages[index].isRead === data.isRead) return

    messages[index] = data
    setMessages([...messages])
    console.log(123);

    // update unread count
    dispatch(updateUser(data.receiver));
  }

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

        <SendMessageModal addMessage={addMessage} isOpen={showModal} close={() => setShowModal(false)} />
        <MessageDetailModal
          isOpen={showMessageDetail}
          close={() => setShowMessageDetail(false)}
          messageId={currentMessage}
          updateData={updateData}
        />

        <ul className='mt-6'>
          {
            renderedMessages.map((msg) => (
              <li
                onClick={() => openMessageDetail(msg.id)}
                key={msg.id}
                className={`${msg.isRead ? "bg-gray-300 border-white" : "bg-gray-50"} h-8 hover:py-4 w-full py-2.5 group items-center flex-1 border-y flex px-3 cursor-pointer`}>
                <input type="checkbox" checked={msg.isRead} disabled />
                <p className='ml-3 truncate w-14'>{tab === "inbox" ? msg.sender.userName : msg.receiver.userName}</p>
                <p className='flex-1 w-0 mx-10 truncate'>{msg.content}</p>
                <span className='group-hover:invisible'>{moment(msg.createdAt).format("lll")}</span>
                <button className='invisible group-hover:visible text-white rounded-full p-2 hover:bg-red-400 bg-red-600'>
                  <MdiDelete />
                </button>
              </li>
            ))
          }
        </ul>
      </section>
    </div>
  )
}

export default Messages