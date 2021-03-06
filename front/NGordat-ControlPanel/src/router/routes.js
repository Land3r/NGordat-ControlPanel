
const routes = [
  {
    name: '',
    path: '/user',
    component: () => import('layouts/EmptyLayout.vue'),
    meta: {
      requiresAuth: false
    },
    children: [
      {
        name: 'LoginPage',
        path: 'login',
        component: () => import('pages/user/LoginPage.vue')
      },
      {
        name: 'RegisterPage',
        path: 'register',
        component: () => import('pages/user/RegisterPage.vue')
      },
      {
        name: 'ActivatePage',
        path: 'activate/:activationtoken',
        component: () => import('pages/user/ActivatePage.vue')
      },
      {
        name: 'ForgotPasswordPage',
        path: 'forgotpassword',
        component: () => import('pages/user/ForgotPasswordPage.vue')
      },
      {
        name: 'ResetPasswordPage',
        path: 'resetpassword/:resetpasswordtoken',
        component: () => import('pages/user/ResetPasswordPage.vue')
      },
      {
        path: '/',
        redirect: { name: 'LoginPage' }
      }
    ]
  },
  {
    path: '/groceries',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'GroceriesReferentialPage',
        path: '/referential',
        component: () => import('pages/groceries/ReferentialPage.vue')
      },
      {
        name: 'GroceriesIndexPage',
        path: '',
        component: () => import('pages/groceries/IndexPage.vue')
      }
    ]
  },
  {
    path: '/download',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'DownloadAvailablePlatformsPage',
        path: '/',
        component: () => import('pages/download/AvailablePlatforms')
      }
    ]
  },
  {
    path: '/technical',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'SpeechToTextPage',
        path: '/speechtotext',
        component: () => import('pages/technical/SpeechToTextPage.vue')
      }
    ]
  },
  {
    path: '/test',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'TestPage',
        path: '/testpage',
        component: () => import('pages/TestPage.vue')
      }
    ]
  },
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    meta: {
      requiresAuth: true
    },
    children: [
      {
        name: 'IndexPage',
        path: '/',
        component: () => import('pages/Index.vue')
      }
    ]
  }
]

// Always leave this as last one
if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
