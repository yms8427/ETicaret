import Vue from 'vue'
import Router from 'vue-router'

// Containers
const TheContainer = () => import('@/containers/TheContainer')

// Views
const Dashboard = () => import('@/views/Dashboard')

// Views - Pages
const Page404 = () => import('@/views/pages/Page404')
const Page500 = () => import('@/views/pages/Page500')
const Login = () => import('@/views/pages/Login')
const Register = () => import('@/views/pages/Register')

// Users
const Users = () => import('@/views/users/Users')
const User = () => import('@/views/users/User')

//Clients
const Clients = () => import('@/views/clients/Clients')
const ClientDetail = () => import('@/views/clients/Client')

//Products
const Campaigns = () => import('@/views/products/Campaigns')
const NewProduct = () => import('@/views/products/NewProduct')
const Products = () => import('@/views/products/Products')
const SearchProduct = () => import('@/views/products/Search')

//Suppliers
const Suppliers = () => import('@/views/suppliers/Suppliers')

//Categories

const Categories = () => import('@/views/categories/Categories')
const SubCategories = () => import('@/views/categories/SubCategories')


Vue.use(Router)

export default new Router({
  mode: 'history',
  linkActiveClass: 'active',
  scrollBehavior: () => ({ y: 0 }),
  routes: configRoutes()
})

function configRoutes() {
  return [
    {
      path: '/',
      redirect: '/dashboard',
      name: 'Home',
      component: TheContainer,
      children: [
        {
          path: 'dashboard',
          name: 'Dashboard',
          component: Dashboard
        },
        {
          path: 'users',
          meta: {
            label: 'Users'
          },
          component: {
            render(c) {
              return c('router-view')
            }
          },
          children: [
            {
              path: '',
              name: 'Site Kullanıcıları Yönetimi',
              component: Users
            },
            {
              path: ':id',
              meta: {
                label: 'User Details'
              },
              name: 'User',
              component: User
            }, 
            
          ]
        },
        {
          path: "clients",
          meta: {
            label: "Müşteri Yönetimi"
          },
          component: {
            render(c) {
              return c('router-view')
            }
          },
          children: [
            {
              path: '',
              component: Clients
            },
            {
              path: ':id',
              meta: {
                label: 'Müşteri Detayı'
              },
              name: 'ClientDetail',
              component: ClientDetail
            }
          ]
        },
      ]
    },
    {
      path: "/product",
      meta: {
        label: "Ürün Yönetimi"
      },
      component: TheContainer,
      children: [
        {
          path: '',
          component: Products,
          name: "Ürün Kataloğu"
        },
        {
          path: 'search',
          meta: {
            label: 'Ürün Arama'
          },
          name: 'SearchProduct',
          component: SearchProduct
        },
        {
          path: 'campaigns',
          meta: {
            label: 'Ürün Kampanyaları'
          },
          name: 'Campaigns',
          component: Campaigns
        },
        {
          path: 'new',
          meta: {
            label: 'Yeni Ürün Tanımı'
          },
          name: 'NewProduct',
          component: NewProduct
        }
      ]
    },
    {
      path: "/supplier",
      meta: {
        label: "Sağlayıcılar"
      },
      component: TheContainer,
      children: [
        {
          path: '',
          meta: {
            label: 'Sağlayıcı İşlemleri'
          },
          component: Suppliers,
          name: "Sağlayıcılar"
        },
      ]
      

    },
    {
      path: "/category",
      meta: {
        label: "Kategori Yönetimi"
      },
      component: TheContainer,
      children: [
        {
          path: 'categories',
          meta: {
            label: 'Üst Kategori İşlemleri'
          },
          component: Categories,
          name: "Üst Kategori"
        },
        {
          path: 'subcategories',
          meta: {
            label: 'Alt Kategori İşlemleri'
          },
          component: SubCategories,
          name: "Alt Kategori"
        },
      ]


    },
    {
      path: '/',
      redirect: '/not-found',
      name: 'Pages',
      component: {
        render(c) { return c('router-view') }
      },
      children: [
        {
          path: 'not-found',
          name: 'Page404',
          component: Page404
        },
        {
          path: '500',
          name: 'Page500',
          component: Page500
        },
        {
          path: 'login',
          name: 'Login',
          component: Login
        },
        {
          path: 'register',
          name: 'Register',
          component: Register
        }
      ]
    }
  ]
}

