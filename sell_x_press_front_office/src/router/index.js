import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/identification/LoginView.vue'
import RegisterView from '../views/identification/RegisterView.vue'
import ForgotView from '../views/identification/ForgotView.vue'
import ProductListView from "../views/ProductListView.vue"
import ProductDetailsView from "../views/ProductDetailsView.vue"
import UserView from '../views/UserView.vue'
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
    path: '/product/:id/:name',
    name: 'product',
    component: ProductDetailsView,
    props: true,
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
  },
  {
    path: '/products/:id/:name',
    name: 'products',
    component: ProductListView,
    props: true,
  }

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  // Check if the route exists
  if (to.matched.length === 0) {
    // Route doesn't exist, redirect to home
    next({ name: 'home' });
  } else {
    // Route exists, proceed with navigation
    next();
  }
});

export default router
