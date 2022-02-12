import axios from 'axios';

const axiosClient = axios.create({
    baseURL: import.meta.env.VITE_APP_API,
    timeout: 2000,
    timeoutErrorMessage: "Server hatası"
})

export default axiosClient