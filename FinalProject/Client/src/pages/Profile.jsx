import { ErrorMessage, Field, Form, Formik } from 'formik'
import React from 'react'
import { useDispatch } from 'react-redux'
import axiosClient from '../utils/axiosClient';
import { useSelector } from 'react-redux';
import { updateUser } from '../store/auth';

const Profile = () => {
	const { user } = useSelector(state => state.auth)
	const dispatch = useDispatch()

	const handleUpdate = async (values) => {
		try {
			const res = await axiosClient.put("auth/update-user", values)
			dispatch(updateUser(res.data));
			alert("Bilgiler güncellendi");
		} catch ({ response }) {
			alert(response.data.errors);
		}
	}

	if (!user) return null

	return (
		<Formik
			initialValues={{
				fullName: user ? user.fullName : "",
				phoneNumber: user ? user.phoneNumber : "",
				tcNo: user ? user.tcNo : "",
				licenseNo: user ? user.licenseNo : "",
			}}
			enableReinitialize={true}
			onSubmit={(values, { resetForm }) => {
				handleUpdate(values);
			}}>
			{({ errors, touched }) => (
				<Form className='max-w-lg mx-auto'>
					<h1 className='mb-10 text-2xl font-semibold text-center uppercase'>Profil Bilgilerim</h1>

					<label htmlFor="fullName">Ad Soyad</label>
					<Field className='input' name="fullName" type="text" />
					<ErrorMessage
						name="fullName"
						component="div"
						className="text-white field-error"
					/>

					<div>
						<label htmlFor="userName">Kullanıcı Adı</label>
						<input readOnly type="text" name="userName" value={user.userName} className='input' />
					</div>

					<div>
						<label htmlFor="email">Email</label>
						<input readOnly type="email" name="email" value={user.email} className='input' />
					</div>

					<label htmlFor="phoneNumber">Telefon No</label>
					<Field className='input' name="phoneNumber" type="text" />
					<ErrorMessage
						name="phoneNumber"
						component="div"
						className="text-white field-error"
					/>

					<label htmlFor="tcNo">TC No</label>
					<Field className='input' name="tcNo" type="text" />
					<ErrorMessage
						name="tcNo"
						component="div"
						className="text-white field-error"
					/>

					<label htmlFor="licenseNo">Araç Plaka</label>
					<Field className='input' name="licenseNo" type="text" />
					<ErrorMessage
						name="licenseNo"
						component="div"
						className="text-white field-error"
					/>

					<button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Kaydet</button>
				</Form>
			)}
		</Formik>
	)
}

export default Profile