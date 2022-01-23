import axios from 'axios';

const appAxios = axios.create({
    baseURL: process.env.VUE_APP_API
})

export default appAxios