import CIcon from '@coreui/icons-react';
import {
  CCreateElement,
  CSidebar,
  CSidebarBrand,
  CSidebarMinimizer, CSidebarNav,
  CSidebarNavDivider,
  CSidebarNavDropdown,
  CSidebarNavItem, CSidebarNavTitle
} from '@coreui/react';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { onToggleSider } from '../application/actions/appAction';
import store from '../AppStore';
// sidebar nav config
import navigation from './_nav';



const mapStateToProps = (state) => {
  return {
    showSider: state.root.showSider
  };
};
class TheSidebar extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showSider:  this.props.showSider
    }
  }

  componentWillReceiveProps(nextProps){
    if(nextProps.showSider != this.state.showSider){
      this.setState({
        showSider: nextProps.showSider
      })
    }
  }

  render() {
    return (
      <CSidebar
        show={this.state.showSider}
        onShowChange={()=>store.dispatch(onToggleSider())}
      >
        <CSidebarBrand className="d-md-down-none" to="/">
          <div className='header-sider-bar' >ADC </div>          
        </CSidebarBrand>
        <CSidebarNav>

          <CCreateElement
            items={navigation}
            components={{
              CSidebarNavDivider,
              CSidebarNavDropdown,
              CSidebarNavItem,
              CSidebarNavTitle
            }}
          />
        </CSidebarNav>
        <CSidebarMinimizer className="c-d-md-down-none" />
      </CSidebar >
    )
  }
}

export default connect(mapStateToProps)(TheSidebar);
