import React from 'react'
import { useDispatch, useSelector } from 'react-redux';
import { toggleUserModal } from '../../store/app';
import Modal from './Modal';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import axiosClient from '../../utils/axiosClient';

const UserSchema = Yup.object().shape({
    username: Yup.string().required('Kullanıcı adı alanı boş olmamalı'),
    email: Yup.string().required('E-posta alanı boş olmamalı').
        email('E-posta alanı geçerli bir adres olmalı'),
    role: Yup.string().required('Rol alanı boş olmamalı').
        required('Rol alanı boş olmamalı'),
    tcNo: Yup.string().required('TC No alanı boş olmamalı').
        length(11, 'TC No alanı 11 karakterden oluşmalı'),
    phone: Yup.string().required('Telefon No alanı boş olmamalı').
        length(10, 'Telefon No alanı 10 karakterden oluşmalı'),
});

const AddUserModal = () => {
    const dispatch = useDispatch();
    const { showAddUserModal } = useSelector((state) => state.app);

    const closeModal = () => {
        dispatch(toggleUserModal());
    }

    const handleSubmit = async (values, resetForm) => {
        try {
            const res = await axiosClient.post("admin/newuser", values);
            closeModal();
            resetForm();
        } catch (err) {
            alert(err.response.data.errors)
        }
    }

    return (
        <Modal isOpen={showAddUserModal} close={closeModal} className='p-2'>
            <Formik
                initialValues={{
                    fullname: '',
                    username: '',
                    email: '',
                    role: '',
                    tcNo: '',
                    phone: '',
                    licenseNo: ''
                }}
                validationSchema={UserSchema}
                onSubmit={(values, { resetForm }) => {
                    handleSubmit(values, resetForm);
                }}
                render={({ errors, touched }) => (
                    <Form className='flex flex-col space-y-3'>
                        <h2 className='text-lg text-center text-white uppercase'>Kişi Ekle</h2>

                        <Field className='w-full p-2 rounded-sm outline-none' name="fullname" placeholder="Ad Soyad" type="text" />
                        <ErrorMessage
                            name="fullname"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="username" placeholder="Kullanıcı Adı" type="text" />
                        <ErrorMessage
                            name="username"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="email" placeholder="E-Posta" type="email" />
                        <ErrorMessage
                            name="email"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="role" placeholder="Rol" type="text" />
                        <ErrorMessage
                            name="role"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="tcNo" placeholder="TC No" type="text" />
                        <ErrorMessage
                            name="tcNo"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="phone" placeholder="Telefon No" type="text" />
                        <ErrorMessage
                            name="phone"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' name="licenseNo" placeholder="Araç Plaka" type="text" />
                        <ErrorMessage
                            name="licenseNo"
                            component="div"
                            className="text-white field-error"
                        />


                        <button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Ekle</button>
                    </Form>
                )}
            />
        </Modal>
    )
}

export default AddUserModal