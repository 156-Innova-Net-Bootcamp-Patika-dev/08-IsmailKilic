import { ErrorMessage, Field, Form, Formik } from 'formik'
import React from 'react'
import { useDispatch } from 'react-redux'
import { loginSuccess } from '../store/auth'
import * as Yup from 'yup';
import axiosClient from '../utils/axiosClient';

const LoginSchema = Yup.object().shape({
    username: Yup.string().required('Kullanıcı adı alanı boş olmamalı'),
    password: Yup.string().required("Parola alanı boş olmamalı").
        min(6, "Parola alanı en az 6 karakterden oluşmalı"),
});

const Login = () => {
    const dispatch = useDispatch()

    const handleLogin = async (values, resetForm) => {
        try {
            const res = await axiosClient.post("auth/login", values)

            localStorage.setItem("user", JSON.stringify({
                token: res.data.token,
                roles: res.data.roles,
                user: res.data.user
            }));
            dispatch(loginSuccess(res.data))
            resetForm();
        } catch (err) {
            alert(err.response.data.errors)
        }
    }

    return (
        <Formik
            initialValues={{
                username: '',
                password: '',
            }}
            validationSchema={LoginSchema}
            onSubmit={(values, { resetForm }) => {
                handleLogin(values, resetForm);
            }}>
            {({ errors, touched }) => (
                <Form>
                    <h1 className='mb-10 text-2xl font-semibold text-center uppercase'>Giriş Yap</h1>

                    <Field className='input' name="username" placeholder="Kullanıcı Adı" type="text" />
                    <ErrorMessage
                        name="username"
                        component="div"
                        className="text-white field-error"
                    />

                    <Field className='input' name="password" placeholder="Parola" type="password" />
                    <ErrorMessage
                        name="password"
                        component="div"
                        className="text-white field-error"
                    />

                    <button type='submit' className='w-full py-2 text-white rounded-sm bg-emerald-600 hover:bg-emerald-800'>Giriş Yap</button>
                </Form>
            )}
        </Formik>
    )
}

export default Login