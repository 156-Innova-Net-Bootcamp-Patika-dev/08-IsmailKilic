import { ErrorMessage, Field, Form, Formik } from 'formik'
import React from 'react'
import axiosClient from '../../utils/axiosClient'
import Modal from './Modal'

const CreateManyInvoice = ({ isOpen, close, ids }) => {
    const handleSubmit = async (values, resetForm) => {
        values.invoiceType = parseInt(values.invoiceType)

        try {
            const res = await axiosClient.post("invoices/many", {
                apartmentIds: ids,
                ...values
            })
            console.log(res.data);
            close()
            resetForm()
        } catch (err) {
            alert(err.response.data.errors)
        }
    }

    return (
        <Modal isOpen={isOpen} close={close} className='p-2'>
            <Formik
                initialValues={{
                    invoiceType: -1,
                    year: "",
                    month: "",
                    price: "",
                }}
                enableReinitialize={true}
                onSubmit={(values, { resetForm }) => {
                    handleSubmit(values, resetForm);
                }}>
                {({ errors, touched }) => (
                    <Form className='flex flex-col space-y-3'>
                        <h2 className='text-lg text-center text-white uppercase'>Fatura Ekle</h2>

                        <Field className='w-full p-2 rounded-sm outline-none' type="number" name="invoiceType" as="select">
                            <option value={-1}>Fatura tipini seçiniz...</option>
                            <option value={0}>Aidat</option>
                            <option value={1}>Elektrik</option>
                            <option value={2}>Su</option>
                            <option value={3}>Doğalgaz</option>
                        </Field>
                        <ErrorMessage
                            name="invoiceType"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' placeholder="Yıl" name="year" type="number" />
                        <ErrorMessage
                            name="year"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' placeholder="Ay" name="month" type="number" />
                        <ErrorMessage
                            name="month"
                            component="div"
                            className="text-white field-error"
                        />

                        <Field className='w-full p-2 rounded-sm outline-none' placeholder="Fiyat" name="price" type="number" />
                        <ErrorMessage
                            name="price"
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

export default CreateManyInvoice