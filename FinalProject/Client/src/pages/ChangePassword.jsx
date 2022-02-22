import React from 'react'
import { ErrorMessage, Field, Form, Formik } from 'formik'
import axiosClient from '../utils/axiosClient';

const ChangePassword = () => {
	const handleChange = async (values, resetForm) => {
		try {
			const res = await axiosClient.put("auth/change-password", values);
			resetForm();
			alert("Şifreniz güncellendi");
		} catch ({ response }) {
			alert(response.data.errors);
		}
	}
	return (
		<Formik
			initialValues={{
				oldPassword: '',
				newPassword: '',
			}}
			onSubmit={(values, { resetForm }) => {
				handleChange(values, resetForm);
			}}>
			{({ errors, touched }) => (
				<Form className='max-w-lg mx-auto mt-10'>
					<h1 className='mb-10 text-2xl font-semibold text-center uppercase'>Şifre Değiştir</h1>

					<Field className='input' name="oldPassword" placeholder="Şuanki Şifre" type="password" />
					<ErrorMessage
						name="oldPassword"
						component="div"
						className="text-white field-error"
					/>

					<Field className='input' name="newPassword" placeholder="Yeni Şifre" type="password" />
					<ErrorMessage
						name="newPassword"
						component="div"
						className="text-white field-error"
					/>

					<button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Şifreni Değiştir</button>
				</Form>
			)}
		</Formik>
	)
}

export default ChangePassword