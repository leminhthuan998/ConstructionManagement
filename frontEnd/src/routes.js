import React from 'react';
import Page500 from "./views/pages/page500/Page500";
import Page401 from "./views/pages/page401/Page401";

const Toaster = React.lazy(() => import('./views/notifications/toaster/Toaster'));
const Tables = React.lazy(() => import('./views/base/tables/Tables'));

const Breadcrumbs = React.lazy(() => import('./views/base/breadcrumbs/Breadcrumbs'));
const Cards = React.lazy(() => import('./views/base/cards/Cards'));
const Carousels = React.lazy(() => import('./views/base/carousels/Carousels'));
const Collapses = React.lazy(() => import('./views/base/collapses/Collapses'));
const BasicForms = React.lazy(() => import('./views/base/forms/BasicForms'));

const Jumbotrons = React.lazy(() => import('./views/base/jumbotrons/Jumbotrons'));
const ListGroups = React.lazy(() => import('./views/base/list-groups/ListGroups'));
const Navbars = React.lazy(() => import('./views/base/navbars/Navbars'));
const Navs = React.lazy(() => import('./views/base/navs/Navs'));
const Paginations = React.lazy(() => import('./views/base/paginations/Pagnations'));
const Popovers = React.lazy(() => import('./views/base/popovers/Popovers'));
const ProgressBar = React.lazy(() => import('./views/base/progress-bar/ProgressBar'));
const Switches = React.lazy(() => import('./views/base/switches/Switches'));

const Tabs = React.lazy(() => import('./views/base/tabs/Tabs'));
const Tooltips = React.lazy(() => import('./views/base/tooltips/Tooltips'));
const BrandButtons = React.lazy(() => import('./views/buttons/brand-buttons/BrandButtons'));
const ButtonDropdowns = React.lazy(() => import('./views/buttons/button-dropdowns/ButtonDropdowns'));
const ButtonGroups = React.lazy(() => import('./views/buttons/button-groups/ButtonGroups'));
const Buttons = React.lazy(() => import('./views/buttons/buttons/Buttons'));
const Charts = React.lazy(() => import('./views/charts/Charts'));
const Dashboard = React.lazy(() => import('./component/dashboard/Dashboard'));
const CoreUIIcons = React.lazy(() => import('./views/icons/coreui-icons/CoreUIIcons'));
const Flags = React.lazy(() => import('./views/icons/flags/Flags'));
const Brands = React.lazy(() => import('./views/icons/brands/Brands'));
const Alerts = React.lazy(() => import('./views/notifications/alerts/Alerts'));
const Badges = React.lazy(() => import('./views/notifications/badges/Badges'));
const Modals = React.lazy(() => import('./views/notifications/modals/Modals'));
const Colors = React.lazy(() => import('./views/theme/colors/Colors'));
const Typography = React.lazy(() => import('./views/theme/typography/Typography'));
const Widgets = React.lazy(() => import('./views/widgets/Widgets'));
const Users = React.lazy(() => import('./views/users/Users'));
const User = React.lazy(() => import('./views/users/User'));
const Vehicle = React.lazy(() => import('./component/vehicle/view/ViewVehicle'));
const MAC = React.lazy(() => import('./component/mac/MacListView'));
const VatTu = React.lazy(() => import('./component/vatTu/view/VatTuListView'));
const HopDong = React.lazy(() => import('./component/hopDong/HopDongListView'));
const LoaiVatTu = React.lazy(() => import('./component/loaiVatTu/LoaiVatTuListView'));
const ThongTinMeTron = React.lazy(() => import('./component/thongTinMeTron/ThongTinMeTronListView'));
const Role = React.lazy(() => import('./component/Role/RoleListView'));
const CapPhoi = React.lazy(() => import('./component/capPhoi/CapPhoiListView'));
const ThanhPhanDat = React.lazy(() => import('./component/thanhPhanDat/ThanhPhanDatListView'));
const ThanhPhanCan = React.lazy(() => import('./component/thanhPhanCan/ThanhPhanCanListView'));
const SaiSo = React.lazy(() => import('./component/saiSo/SaiSoListView'));
const _User = React.lazy(() => import('./component/user/UserListView'));

const Page404 = React.lazy(() => import('./views/pages/page404/Page404'));


const routes = [
  { path: '/', exact: true, name: 'Home' },
  { path: '/dashboard', name: 'Dashboard', component: Dashboard },
  { path: '/theme', name: 'Theme', component: Colors, exact: true },
  { path: '/theme/colors', name: 'Colors', component: Colors },
  { path: '/theme/typography', name: 'Typography', component: Typography },
  { path: '/base', name: 'Base', component: Cards, exact: true },
  { path: '/base/breadcrumbs', name: 'Breadcrumbs', component: Breadcrumbs },
  { path: '/base/cards', name: 'Cards', component: Cards },
  { path: '/base/carousels', name: 'Carousel', component: Carousels },
  { path: '/base/collapses', name: 'Collapse', component: Collapses },
  { path: '/base/forms', name: 'Forms', component: BasicForms },
  { path: '/base/jumbotrons', name: 'Jumbotrons', component: Jumbotrons },
  { path: '/base/list-groups', name: 'List Groups', component: ListGroups },
  { path: '/base/navbars', name: 'Navbars', component: Navbars },
  { path: '/base/navs', name: 'Navs', component: Navs },
  { path: '/base/paginations', name: 'Paginations', component: Paginations },
  { path: '/base/popovers', name: 'Popovers', component: Popovers },
  { path: '/base/progress-bar', name: 'Progress Bar', component: ProgressBar },
  { path: '/base/switches', name: 'Switches', component: Switches },
  { path: '/base/tables', name: 'Tables', component: Tables },
  { path: '/base/tabs', name: 'Tabs', component: Tabs },
  { path: '/base/tooltips', name: 'Tooltips', component: Tooltips },
  { path: '/buttons', name: 'Buttons', component: Buttons, exact: true },
  { path: '/buttons/buttons', name: 'Buttons', component: Buttons },
  { path: '/buttons/button-dropdowns', name: 'Dropdowns', component: ButtonDropdowns },
  { path: '/buttons/button-groups', name: 'Button Groups', component: ButtonGroups },
  { path: '/buttons/brand-buttons', name: 'Brand Buttons', component: BrandButtons },
  { path: '/charts', name: 'Charts', component: Charts },
  { path: '/icons', exact: true, name: 'Icons', component: CoreUIIcons },
  { path: '/icons/coreui-icons', name: 'CoreUI Icons', component: CoreUIIcons },
  { path: '/icons/flags', name: 'Flags', component: Flags },
  { path: '/icons/brands', name: 'Brands', component: Brands },
  { path: '/notifications', name: 'Notifications', component: Alerts, exact: true },
  { path: '/notifications/alerts', name: 'Alerts', component: Alerts },
  { path: '/notifications/badges', name: 'Badges', component: Badges },
  { path: '/notifications/modals', name: 'Modals', component: Modals },
  { path: '/notifications/toaster', name: 'Toaster', component: Toaster },
  { path: '/widgets', name: 'Widgets', component: Widgets },
  { path: '/users', exact: true,  name: 'Users', component: Users },
  { path: '/users/:id', exact: true, name: 'User Details', component: User },

  { path: '/vehicle', exact: true, name: 'Phương tiện', component: Vehicle },
  { path: '/mac', exact: true, name: 'Loại bê tông (MAC)', component: MAC },
  { path: '/vat-tu', exact: true, name: 'Vật tư', component: VatTu },
  { path: '/hop-dong', exact: true, name: 'Hợp đồng', component: HopDong },
  { path: '/loai-vat-tu', exact: true, name: 'Loại vật tư', component: LoaiVatTu },
  { path: '/thong-tin-me-tron', exact: true, name: 'Thông tin mẻ trộn', component: ThongTinMeTron },
  { path: '/role', exact: true, name: 'Role', component: Role },
  { path: '/unauthorized', exact: true, name: 'Unauthorized', component: Page401 },
  { path: '/cap-phoi', exact: true, name: 'Cấp phối', component: CapPhoi },
  { path: '/thanh-phan-dat', exact: true, name: 'Thành phần đặt', component: ThanhPhanDat },
  { path: '/thanh-phan-can', exact: true, name: 'Thành phần cân', component: ThanhPhanCan },
  { path: '/sai-so', exact: true, name: 'Sai số', component: SaiSo },
  { path: '/identity', exact: true, name: 'User', component: _User },


  { path: "*", component: Page404 },
];

export default routes;
