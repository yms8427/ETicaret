export default [
  {
    _name: 'CSidebarNav',
    _children: [
      {
        _name: 'CSidebarNavItem',
        name: 'Ana Sayfa',
        to: '/dashboard',
        icon: 'cil-speedometer',
      },
      {
        _name: 'CSidebarNavTitle',
        _children: ['Yönetim Alanı']
      },
      {
        _name: 'CSidebarNavItem',
        name: 'Kullanıcı Yönetimi',
        to: '/users',
        icon: 'cil-drop'
      },
      {
        _name: 'CSidebarNavItem',
        name: 'Müşteri Yönetimi',
        to: '/clients',
        icon: 'cil-drop'
      },
      {
        _name: 'CSidebarNavTitle',
        _children: ['İçerik Yönetimi']
      },
      {
        _name: 'CSidebarNavDropdown',
        name: 'Ürün Yönetimi',
        route: '/product',
        icon: 'cil-puzzle',
        items: [
          {
            name: 'Yeni Ürün',
            to: '/product/new'
          },
          {
            name: 'Ürün Ara',
            to: '/product/search'
          },
          {
            name: 'Kampanya Yönetimi',
            to: '/product/campaigns'
          },
          {
            name: 'Ürün Kataloğu',
            to: '/product'
          }
        ]
      },
      {
        _name: 'CSidebarNavItem',
        name: 'Sağlayıcılar',
        icon: 'cil-puzzle',
        to: '/supplier'
      },
      {
        _name: 'CSidebarNavDropdown',
        name: 'Kategori Yönetimi',
        route: '/category',
        icon: 'cil-puzzle',
        items: [
          {
            name: 'Üst Kategori',
            to: '/category/categories'
          },
          {
            name: 'Alt Kategori',
            to: '/category/subcategories'
          }
        ]
      }
    ]
  }
]