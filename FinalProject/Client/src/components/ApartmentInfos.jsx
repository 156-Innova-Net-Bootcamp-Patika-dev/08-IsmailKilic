import { Field, Form, Formik } from 'formik'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import { ownerTypes } from '../pages/ApartmentDetail'
import axiosClient from '../utils/axiosClient'

const ApartmentInfos = ({ data }) => {
    const navigate = useNavigate()

    const handleDelete = async () => {
        if (confirm("Bu daireyi silmek istediğinize emin misiniz?")) {
            try {
                const res = await axiosClient.delete(`apartments/${data.id}`)
                alert("Silme başarılı")
                navigate("/apartments")
            } catch ({ response }) {
                alert(response.data.errors)
            }
        }
    }

    const handleUpdate = async (values) => {
        if (confirm("Bu daireyi güncellemek istediğinize emin misiniz?")) {
            values.ownerType = parseInt(values.ownerType) || 0;

            try {
                const res = await axiosClient.put(`apartments/${data.id}`, values)
                alert("Güncelleme başarılı")
            } catch ({ response }) {
                alert(response.data.errors)
            }
        }
    }

    return (
        <div className='grow'>
            <h2 className='mb-3 text-lg'>Daire Bilgileri</h2>
            <Formik
                initialValues={{
                    block: data?.block || '',
                    type: data?.type || '',
                    floor: data?.floor || '',
                    no: data?.no || '',
                    ownerType: data?.ownerType || ''
                }}
                enableReinitialize={true}
                onSubmit={(values) => {
                    handleUpdate(values)
                }}>
                {({ errors, touched }) => (
                    <Form>
                        <div>
                            Blok:
                            <Field className='bg-gray-600 ml-1 outline-none' name="block" type="text" />
                        </div>
                        <div>
                            Kat:
                            <Field className='bg-gray-600 ml-1 outline-none' name="floor" type="text" />
                        </div>
                        <div>
                            Daire No:
                            <Field className='bg-gray-600 ml-1 outline-none' name="no" type="text" />
                        </div>
                        <div>
                            Daire Tipi:
                            <Field className='bg-gray-600 ml-1 outline-none' name="type" type="text" />
                        </div>
                        <div>Durum: {data?.isFree ? "Boş" : "Dolu"}</div>
                        <div>
                            <Field className='bg-gray-600 outline-none' name="ownerType" as="select">
                                {
                                    ownerTypes.map((x, index) => (
                                        <option key={index} value={index}>{x}</option>
                                    ))
                                }
                            </Field>
                        </div>

                        <div className='flex mt-3 space-x-3'>
                            <button type='submit' className='button bg-green-600'>Güncelle</button>
                            {!data?.user && data?.invoices?.length === 0 &&
                                <button onClick={handleDelete} className='button bg-red-600'>Sil</button>}
                        </div>
                    </Form>
                )}

            </Formik>

        </div>
    )
}

export default ApartmentInfos