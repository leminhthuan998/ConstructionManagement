import React, { Component } from 'react'
import {
    TheContent,
    TheSidebar,
    TheFooter,
    TheHeader
} from './index'
import { connect } from 'react-redux';
import AppUtil from '../utils/AppUtil';
import { API_CHECK_LOGIN } from '../constants/ApiConstant'
import Axios from 'axios';

const Login = React.lazy(() => import('../views/pages/login/Login'));

const mapStateToProps = (state) => {
    return {
        isAuthenticated: state.root.isAuthenticated,
    };
};

class TheLayout extends Component {
    constructor(props) {
        super(props)
        this.state = {
            isAuthenticated: props.isAuthenticated,
        }
    } 
    render() {
        return (
            this.props.isAuthenticated == true ?
            <div className="c-app c-default-layout">
                <TheSidebar />
                <div className="c-wrapper">
                    <TheHeader />
                    <div className="c-body">
                        <TheContent />
                    </div>
                    <TheFooter />
                </div>
            </div> : <Login/>
        )
    }


}

export default connect(mapStateToProps)(TheLayout)
