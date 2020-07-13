import React, { Component } from 'react'
import { useSelector, useDispatch } from 'react-redux'
import {
  CHeader,
  CToggler,
  CHeaderBrand,
  CHeaderNav,
  CHeaderNavItem,
  CHeaderNavLink,
  CSubheader,
  CBreadcrumbRouter,
  CLink
} from '@coreui/react'
import CIcon from '@coreui/icons-react'

// routes config
import routes from '../routes'
import { onToggleSider } from '../application/actions/appAction';
import store from '../AppStore';
import { connect } from 'react-redux';

import {
  TheHeaderDropdown,
  TheHeaderDropdownMssg,
  TheHeaderDropdownNotif,
  TheHeaderDropdownTasks
} from './index'
const mapStateToProps = (state) => {
  return {
    showSider: state.root.showSider
  };
};
class TheHeader extends Component {
  // const dispatch = useDispatch()
  // const sidebarShow = useSelector(state => state.sidebarShow)
  constructor(props) {
    super(props);
    this.state = {
      showSider:  this.props.showSider,

    }
  }
  
  toggleSidebar = () => {    
    store.dispatch(onToggleSider())
  }

  
  render() {
    return (
      <CHeader withSubheader>
        <CToggler
          inHeader
          className="ml-md-3 d-lg-none"
          onClick={this.toggleSidebar}
        />
        <CToggler
          inHeader
          className="ml-3 d-md-down-none"
          onClick={this.toggleSidebar}
        />
        <CHeaderBrand className="mx-auto d-lg-none" to="/">
          {/* <CIcon name="logo" height="48" alt="Logo" /> */}
          <div className='header-sider-bar-mobile' >ADC</div>
        </CHeaderBrand>

        <CHeaderNav className="d-md-down-none mr-auto">
          <CHeaderNavItem className="px-3" >
            <CHeaderNavLink to="/dashboard">Dashboard</CHeaderNavLink>
          </CHeaderNavItem>
          {/* <CHeaderNavItem className="px-3">
            <CHeaderNavLink to="/users">Users</CHeaderNavLink>
          </CHeaderNavItem>
          <CHeaderNavItem className="px-3">
            <CHeaderNavLink>Settings</CHeaderNavLink>
          </CHeaderNavItem> */}
        </CHeaderNav>

        <CHeaderNav className="px-3">
          {/* <TheHeaderDropdownNotif />
          <TheHeaderDropdownTasks />
          <TheHeaderDropdownMssg /> */}
          <TheHeaderDropdown />
        </CHeaderNav>

        <CSubheader className="px-3 justify-content-between">
          <CBreadcrumbRouter
            className="border-0 c-subheader-nav m-0 px-0 px-md-3"
            routes={routes}
          />
          <div className="d-md-down-none mfe-2 c-subheader-nav">
            {/* <CLink className="c-subheader-nav-link" href="#">
              <CIcon name="cil-speech" alt="Settings" />
            </CLink> */}
            {/* <CLink
              className="c-subheader-nav-link"
              aria-current="page"
              to="/dashboard"
            >
              <CIcon name="cil-graph" alt="Dashboard" />&nbsp;Dashboard
            </CLink>
            <CLink className="c-subheader-nav-link" href="#">
              <CIcon name="cil-settings" alt="Settings" />&nbsp;Settings
            </CLink> */}
          </div>
        </CSubheader>
      </CHeader>
    )
  }

}

export default connect(mapStateToProps)(TheHeader);
