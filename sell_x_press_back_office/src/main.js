import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { createPinia } from 'pinia';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./main.css";
import 'mosha-vue-toastify/dist/style.css'
import Select2 from 'vue3-select2-component';

const pinia = createPinia();
const app = createApp(App);

app
    .use(pinia)
    .use(router)
    .component('Select2', Select2)
    .mount('#app');

import 'bootstrap/dist/js/bootstrap.bundle.min.js';