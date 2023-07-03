import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { createPinia } from 'pinia';
import "./main.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import bootstrap from 'bootstrap/dist/js/bootstrap.bundle.min.js';

const pinia = createPinia();
const app = createApp(App);

app
    .use(pinia)
    .use(router)
    .use(bootstrap)
    .mount('#app');