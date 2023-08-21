import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/identification/LoginView.vue'
import RegisterView from '../views/identification/RegisterView.vue'
import ForgotView from '../views/identification/ForgotView.vue'
import UserView from '../views/UserView.vue'
import ProductDetailsView from '../views/ProductDetailsView.vue'
import CartView from '../views/CartView.vue'
import HistoryView from '../views/HistoryView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  },
  {
    path: '/forgot',
    name: 'forgot',
    component: ForgotView
  },
  {
    path: '/user',
    name: 'user',
    component: UserView
  },
  {
    path: '/product',
    name: 'product',
    component: ProductDetailsView
  },
  {
    path: '/cart',
    name: 'cart',
    component: CartView
  },
  {
    path: '/history',
    name: 'histoty',
    component: HistoryView
  }

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
