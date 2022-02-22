import React, { useEffect, useState } from 'react'
import Modal from './Modal'
import { Formik, Field, Form, ErrorMessage } from 'formik';
import axiosClient from '../../utils/axiosClient';

const AssignUserModal = ({ isOpen, close, id, updateData }) => {
    const [users, setUsers] = useState([])

    useEffect(async () => {
        const res = await axiosClient.get("admin/users")
        setUsers(res.data.filter(x => !x.isDelete))

    }, [])


    const handleSubmit = async (values, resetForm) => {
        values.ownerType = parseInt(values.ownerType);
        console.log(values);
        try {
            const res = await axiosClient.post("apartments/assign-user", values);
            updateData(res.data);
            close();
            resetForm();
        } catch (err) {
            alert(err.response.data.errors)
        }
    }

    return (
        <Modal isOpen={isOpen} close={close} className='p-2'>
            <Formik
                initialValues={{
                    apartmentId: id,
                    userId: "",
                    ownerType: ""
                }}
                enableReinitialize={true}
                onSubmit={(values, { resetForm }) => {
                    handleSubmit(values, resetForm);
                }}>
                {({ errors, touched }) => (
                    <Form className='flex flex-col space-y-3'>
                        <h2 className='text-lg text-center text-white uppercase'>Daireye Kişi Ata</h2>

                        <Field disabled className='w-full p-2 rounded-sm outline-none' name="apartmentId" placeholder="Blok Kodu" type="text" />
                        <ErrorMessage
                            name="apartmentId"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="userId" placeholder="Kişi" as="select">
                            <option value={""}>Kişi seçiniz...</option>
                            {
                                users.map(u => (
                                    <option key={u.id} value={u.id}>{u.userName}</option>
                                ))
                            }
                        </Field>
                        <ErrorMessage
                            name="userId"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="ownerType" placeholder="Sahiplik Durumu" type="number" as="select">
                            <option value={""}>Sahiplik durumunu seçiniz...</option>
                            <option value={0}>Ev Sahibi</option>
                            <option value={1}>Kiracı</option>
                        </Field>
                        <ErrorMessage
                            name="ownerType"
                            component="div"
                            className="text-white field-error"
                        />

                        <button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Ekle</button>
                    </Form>
                )}
            </Formik >
        </Modal >
    )
}

export default AssignUserModal