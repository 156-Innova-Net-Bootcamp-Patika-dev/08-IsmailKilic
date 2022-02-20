import axios from 'axios';

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_APP_API,
    timeout: 5000,
    timeoutErrorMessage: "Server hatası"
})

axiosClient.interceptors.request.use(request => {
    const token = JSON.parse(localStorage.getItem("user"))?.token || ""
    request.headers.common.Authorization = `Bearer ${token}`;

    return request;
})

export default axiosClient