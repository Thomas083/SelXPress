import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/identification/LoginView.vue'
import RegisterView from '../views/identification/RegisterView.vue'
import ForgotView from '../views/identification/ForgotView.vue'
import UserView from "../views/UserView.vue"
import AdminView from "../views/AdminView.vue"
import ProductDetails from '../views/ProductDetails.vue'
import AddProduct from "../views/AddProduct.vue"
import ProductDetails from '../views/ProductDetails.vue'

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
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/user',
    name: 'user',
    component: UserView
  },
  {
    path: '/admin',
    name: 'admin',
    component: AdminView
  },
  {
    path: '/product/:id/:name',
    name: 'product',
    component: ProductDetails
  },
  {
    path: '/add-product',
    name: 'add-product',
    component: AddProduct
  },
  {
    path: '/product/:id/:name',
    name: 'product',
    component: ProductDetails
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  if (!localStorage.getItem('user') && to.name !== 'login' && to.name !== 'register' && to.name !== 'forgot'){
    next({ name: 'login' });
  } else if (localStorage.getItem('user') && localStorage.getItem('user') && (to.name === 'login' || to.name === 'register' || to.name === 'forgot')) {
    next({ name: 'home'});
  } else {
    next()
  }
});

export default router
