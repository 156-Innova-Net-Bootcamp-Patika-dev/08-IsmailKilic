import { ErrorMessage, Field, Form, Formik } from 'formik'
import React, { useEffect, useState } from 'react'
import { useSelector } from 'react-redux'
import axiosClient from '../../utils/axiosClient'
import Modal from './Modal'

const SendMessageModal = ({ isOpen, close }) => {
    const { user } = useSelector(state => state.auth)
    const [users, setUsers] = useState([])

    useEffect(async () => {
        const res = await axiosClient.get("admin/users")
        const data = res.data.filter(x => x.id !== user.id)
        setUsers(data)
    }, [])

    const handleSubmit = async (values) => {
        const res = await axiosClient.post("messages", {
            ...values,
            senderId: user.id
        })
        console.log(res.data);
        close();
    }

    return (
        <Modal isOpen={isOpen} close={close} className='p-2'>
            <Formik
                initialValues={{
                    content: '',
                    receiverId: ''
                }}
                enableReinitialize={true}
                onSubmit={(values, { resetForm }) => {
                    handleSubmit(values);
                    resetForm();
                }}>
                {({ errors, touched }) => (
                    <Form className='flex flex-col space-y-3'>
                        <h2 className='text-lg text-center text-white uppercase'>Mesaj Gönder</h2>

                        <Field className='w-full p-2 rounded-sm outline-none' name="receiverId" placeholder="Kişi" as="select">
                            <option value={""}>Kişi seçiniz...</option>
                            {
                                users.map(u => (
                                    <option key={u.id} value={u.id}>{u.userName}</option>
                                ))
                            }
                        </Field>
                        <ErrorMessage
                            name="receiverId"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="content" placeholder="Mesaj" type="text" as="textarea"/>
                        <ErrorMessage
                            name="content"
                            component="div"
                            className="text-white field-error"
                        />

                        <button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Ekle</button>
                    </Form>
                )}
            </Formik>
        </Modal>
    )
}

export default SendMessageModal