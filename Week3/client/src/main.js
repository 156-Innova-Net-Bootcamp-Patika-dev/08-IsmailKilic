import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import CKEditor from '@ckeditor/ckeditor5-vue';

import 'bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.bundle'
import appAxios from './utils/appAxios'

const app = createApp(App)
app.use(router)
app.use(store)
app.use(CKEditor)
app.config.globalProperties.$appAxios = appAxios

app.mount('#app')
