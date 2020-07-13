import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import {
    CButton,
    CCard,
    CCardBody,
    CCardGroup,
    CCol,
    CContainer,
    CForm,
    CInput,
    CInputGroup,
    CInputGroupPrepend,
    CInputGroupText,
    CRow
} from '@coreui/react'
import CIcon from '@coreui/icons-react'
import Axios from 'axios';
import AppUtil from '../../../utils/AppUtil';
import { API_LOGIN, API_CHECK_LOGIN } from '../../../constants/ApiConstant'
import store from '../../../AppStore';
import { loginSuccessFull } from '../../../application/actions/appAction';
import Loading from '../Loading';
import history from '../../../history';

class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            model: {
                username: '',
                password: '',
            },
            loading: true
        };
    }

    componentDidMount() {
        this.checkLogin();
    }

    checkLogin() {
        Axios.get(AppUtil.GLOBAL_API_PATH + API_CHECK_LOGIN)
            .then((res => {
                const { data } = res;
                this.setState({
                    loading: false
                });
                if (data.success) {
                    try {
                        store.dispatch(loginSuccessFull(data.data))
                    } catch (error) {
                    } finally {
                    }
                }
            }))
            .catch(() => {
                this.setState({
                    loading: false
                });
                // ApiUtil.error();
            })
            .finally(() => {
                this.setState({
                    loading: false
                });
            });
    }

    submitForm = (e) => {
        this.setState({
            loading: true
        });
        e.preventDefault();
        Axios.post(AppUtil.GLOBAL_API_PATH + API_LOGIN, this.state.model)
            .then((response) => {
                const { data } = response;
                if (!data.success) {
                    AppUtil.ToastError("Đăng nhập không thành công!")
                }
                if (data.success) {
                    AppUtil.ToastSuccess("Đăng nhập thành công!")
                    history.push("/dashboard")

                }
            })
            .catch(() => {
                AppUtil.ToastError("Đăng nhập không thành công!")
            })
            .finally(() => {
                setTimeout(() => {
                    this.checkLogin();
                }, 500);
            });
    }

    onChangeInput(val, name) {
        const { model } = this.state;
        model[name] = val;
        this.setState({
            model
        });
    }

    render() {
        const { username, password } = this.state
        return (
            <div className="c-app c-default-layout flex-row align-items-center">
                <CContainer>
                    <CRow className="justify-content-center">
                        <CCol md="6">
                            <CCardGroup>
                                <CCard className="p-4">
                                    <CCardBody>
                                        <CForm onSubmit={this.submitForm}>
                                            <h1>Login</h1>
                                            <p className="text-muted">Sign In to your account</p>
                                            <CInputGroup className="mb-3">
                                                <CInputGroupPrepend>
                                                    <CInputGroupText>
                                                        <CIcon name="cil-user" />
                                                    </CInputGroupText>
                                                </CInputGroupPrepend>
                                                <CInput value={username}
                                                    type="text" placeholder="Username"
                                                    autoComplete="username"
                                                    onChange={(e) => this.onChangeInput(e.target.value, 'username')}
                                                />
                                            </CInputGroup>
                                            <CInputGroup className="mb-4" >
                                                <CInputGroupPrepend>
                                                    <CInputGroupText>
                                                        <CIcon name="cil-lock-locked" />
                                                    </CInputGroupText>
                                                </CInputGroupPrepend>
                                                <CInput value={password}
                                                    onChange={(e) => this.onChangeInput(e.target.value, 'password')}
                                                    type="password" placeholder="Password" autoComplete="current-password" />
                                            </CInputGroup>

                                            <div style={{ display: 'flex', justifyContent: 'flex-end', flex: 1, marginTop: -20 }}>
                                                <CButton color="link" className="px-0">Forgot password?</CButton>
                                            </div>
                                            <div style={{ display: 'flex', justifyContent: 'center', flex: 1 }}>
                                                <CButton onClick={this.submitForm} type="submit" color="primary" className="px-4">Login</CButton>
                                            </div>
                                            {/* <CCol style={{display:'flex', justifyContent:'center'}} xs="8">
                                                    <CButton onClick={this.submitForm} type="submit" color="primary" className="px-4">Login</CButton>
                                                </CCol>
                                                <CCol xs="4" className="text-right">
                                                    <CButton color="link" className="px-0">Forgot password?</CButton>
                                                </CCol> */}

                                        </CForm>
                                    </CCardBody>
                                </CCard>
                                {/* <CCard className="text-white bg-primary py-5 d-md-down-none" style={{ width: '44%' }}>
                                    <CCardBody className="text-center">
                                        <div>
                                            <h2>Sign up</h2>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut
                        labore et dolore magna aliqua.</p>
                                            <Link to="/register">
                                                <CButton color="primary" className="mt-3" active tabIndex={-1}>Register Now!</CButton>
                                            </Link>
                                        </div>
                                    </CCardBody>
                                </CCard> */}
                            </CCardGroup>
                        </CCol>
                    </CRow>
                </CContainer>
                {this.state.loading ? <div style={{ zIndex: 9999, position: "fixed", background: "#fff", top: 0, left: 0, width: "100%", height: "100%" }}><Loading /></div> : null}

            </div>
        )
    }

}

export default Login
