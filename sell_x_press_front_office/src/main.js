import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { createPinia } from 'pinia';
// import "./main.css";
import 'bootstrap/dist/css/bootstrap.css';

const pinia = createPinia();
const app = createApp(App);

app
    .use(pinia)
    .use(router)
    .mount('#app');

import 'bootstrap/dist/js/bootstrap.bundle.min.js';