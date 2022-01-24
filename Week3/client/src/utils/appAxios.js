import axios from 'axios';
import store from '../store';

const appAxios = axios.create({
    baseURL: process.env.VUE_APP_API
})

appAxios.interceptors.request.use(request => {
    const token = store.getters.getUser?.token || "";
    if (token) {
        request.headers.common.Authorization = `Bearer ${token}`;
    }

    return request;
})

export default appAxios