import React, { useState } from 'react'
import Modal from './Modal'
import { Field, Form, Formik } from 'formik'
import InputMask from 'react-input-mask';

const PaymentModal = ({ isOpen, close }) => {
    const [backface, setBackface] = useState(false)

    const handleSubmit = (values, resetForm) => {
        resetForm();
        close();
    }

    return (
        <Modal isOpen={isOpen} close={close} className='p-2 text-black'>
            <Formik
                initialValues={{
                    fullName: '',
                    cartNumber: '',
                    lastDate: '',
                    cvv: ''
                }}
                enableReinitialize={true}
                onSubmit={(values, { resetForm }) => {
                    handleSubmit(values, resetForm);
                }}>
                {({ values, setFieldValue }) => (
                    <Form className='flex flex-col space-y-3'>
                        <h2 className='text-lg text-center text-white uppercase'>Ödeme Yap</h2>

                        <div className="relative w-full h-64 my-5">
                            <div className="box-border absolute top-0 left-0 flex flex-col w-full h-full py-8 px-7 rounded-2xl bg-gradient-to-t from-blue-600 to-blue-500">
                                <div className="flex justify-between">
                                    <img src="/img/chip.svg" alt="" />
                                </div>
                                <div className="mt-6 font-mono text-2xl text-white">
                                    {values.cartNumber || '**** **** **** ****'}
                                </div>
                                <div className="flex justify-between mt-auto">
                                    <div>
                                        <div className="opacity-80 mb-1.5">Card Holder Name</div>
                                        <div className="text-lg font-medium">{values.fullName || '***'}</div>
                                    </div>
                                    <div>
                                        <div className="opacity-80 mb-1.5">Expiry Date</div>
                                        <div className="text-lg font-medium">{values.lastDate || '***'}</div>
                                    </div>
                                </div>
                            </div>
                            <div className={`${backface ? 'scale-100' : 'scale-0'} transition-all duration-300 box-border absolute top-0 left-0 flex flex-col w-full h-full py-8 px-7 rounded-2xl bg-gradient-to-t from-blue-600 to-blue-500`}>
                                <div className="flex justify-end p-5 mt-auto text-black bg-white">
                                    CVV <em className='ml-4 font-bold'>{values.cvv || '***'}</em>
                                </div>
                            </div>
                        </div>

                        <Field className='w-full p-2 text-black rounded-sm outline-none' name="fullName" placeholder="Kart Sahibi" type="text" />

                        <InputMask mask="9999 9999 9999 9999" maskChar="" value={values.cartNumber} onChange={(e) => setFieldValue("cartNumber", e.target.value)}>
                            {(inputProps) => <Field {...inputProps} className='w-full p-2 text-black rounded-sm outline-none' name="cartNumber" placeholder="Kart Numarası" type="text" />}
                        </InputMask>

                        <InputMask mask="99/99" maskChar="" value={values.lastDate} onChange={(e) => setFieldValue("lastDate", e.target.value)}>
                            {(inputProps) => <Field {...inputProps} className='w-full p-2 text-black rounded-sm outline-none' name="lastDate" placeholder="Son Kullanma Tarihi" type="text" />}
                        </InputMask>

                        <InputMask
                            mask="999"
                            maskChar=""
                            value={values.cvv}
                            onFocus={() => setBackface(true)}
                            onBlur={() => setBackface(false)}
                            onChange={(e) => setFieldValue("cvv", e.target.value)}>
                            {(inputProps) =>
                                <Field {...inputProps} className='w-full p-2 text-black rounded-sm outline-none' name="cvv" placeholder="CVV" type="text" />}
                        </InputMask>

                        <button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Ekle</button>
                    </Form>
                )}
            </Formik>
        </Modal>
    )
}

export default PaymentModal