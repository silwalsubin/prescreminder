import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import Tabs from '../views/Tabs.vue'
import Welcome from '../views/Welcome.vue'
import Register from '../views/Register.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    component: Welcome
  },
  {
    path: '/register',
    component: Register
  },
  {
    path: '/tabs/',
    component: Tabs,
    children: [
      {
        path: '',
        redirect: '/tabs/dashboard'
      },
      {
        path: 'dashboard',
        component: () => import('@/views/tabs/Dashboard.vue')
      },
      {
        path: 'prescriptions',
        component: () => import('@/views/tabs/Prescriptions.vue')
      },
      {
        path: 'history',
        component: () => import('@/views/tabs/History.vue')
      },
      {
        path: 'account',
        component: () => import('@/views/tabs/Account.vue')
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
