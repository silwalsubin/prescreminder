import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import Tabs from '../views/Tabs.vue'
import Welcome from '../views/Welcome.vue'
import Register from '../views/Register.vue'
import LogIn from '../views/Login.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    component: Welcome,
    name: 'WelcomePage'
  },
  {
    path: '/register',
    component: Register,
    name: 'RegisterPage'
  },
  {
    path: '/login',
    component: LogIn,
    name: 'LogInPage'
  },
  {
    path: '/tabs/',
    component: Tabs,
    children: [
      {
        path: '',
        redirect: '/tabs/dashboard',
        name: 'DashboardTab Default'
      },
      {
        path: 'dashboard',
        component: () => import('@/views/tabs/Dashboard.vue'),
        name: 'DashboardTab'
      },
      {
        path: 'prescriptions',
        component: () => import('@/views/tabs/Prescriptions.vue'),
        name: 'PrescriptionTab'
      },
      {
        path: 'history',
        component: () => import('@/views/tabs/History.vue'),
        name: 'HistoryTab'
      },
      {
        path: 'account',
        component: () => import('@/views/tabs/Account.vue'),
        name: 'AccountTab'
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
