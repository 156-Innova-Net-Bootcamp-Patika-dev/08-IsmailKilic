import React from 'react'
import { useEffect } from 'react'
import Modal from './Modal'
import axiosClient from '../../utils/axiosClient'
import { useState } from 'react'
import moment from 'moment/min/moment-with-locales';

moment.locale("tr")

const MessageDetailModal = ({ isOpen, close, messageId, updateData }) => {
    const [message, setMessage] = useState("")

    useEffect(async() => {
        if(!messageId) return
        const res = await axiosClient.get(`messages/${messageId}`)
        setMessage(res.data);
        updateData(res.data)
    }, [messageId])
    
    return (
        <Modal isOpen={isOpen} close={close} className='p-2'>
            <div className='w-full bg-white rounded-lg p-2'>
                <ul>
                    <li>Gönderen: {message?.sender?.fullName}</li>
                    <li>Tarih: {moment(message?.createdAt).format("lll")}</li>
                    <li>{message?.content}</li>
                </ul>
            </div>
        </Modal>

    )
}

export default MessageDetailModal