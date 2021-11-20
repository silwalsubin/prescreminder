import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import Tabs from '../views/Tabs.vue'
import Welcome from '../views/Welcome.vue'
import Register from '../views/Register.vue'
import LogIn from '../views/Login.vue';
import { getBearerToken } from '../bearer-token-service';

import { RouteName } from './route-names';

const allowAnonymousRouteNames = [
  RouteName.WelcomePage,
  RouteName.RegisterPage,
  RouteName.LogInPage,
].map(x => x.toString());

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    component: Welcome,
    name: RouteName.WelcomePage
  },
  {
    path: '/register',
    component: Register,
    name: RouteName.RegisterPage,
  },
  {
    path: '/login',
    component: LogIn,
    name: RouteName.LogInPage,
  },
  {
    path: '/tabs/',
    component: Tabs,
    children: [
      {
        path: '',
        redirect: '/tabs/dashboard',
        name: RouteName.DashboardTabDefault
      },
      {
        path: 'dashboard',
        component: () => import('@/views/tabs/Dashboard.vue'),
        name: RouteName.DashboardTab
      },
      {
        path: 'prescriptions',
        component: () => import('@/views/tabs/Prescriptions/index.vue'),
        name: RouteName.PrescriptionTab
      },
      {
        path: 'history',
        component: () => import('@/views/tabs/History.vue'),
        name: RouteName.HistoryTab
      },
      {
        path: 'account',
        component: () => import('@/views/tabs/Account.vue'),
        name: RouteName.AccountTab
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  // navigate to welcome page if session is inactive
  if (to.name && ! allowAnonymousRouteNames.includes(to.name.toString())){
    if(!getBearerToken()){
      next({name: RouteName.WelcomePage})
    }
  }

  // navigate to dashboard is session is active
  if (to.name && allowAnonymousRouteNames.includes(to.name.toString())){
    if(getBearerToken()){
      next({name: RouteName.DashboardTabDefault})
    }
  }

  next();
})

export default router
