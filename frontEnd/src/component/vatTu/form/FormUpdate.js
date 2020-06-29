import { Form, Input } from 'antd';
import React, { Component } from 'react';
import { API_VEHICLE_UPDATE, API_VEHICLE_CREATE, API_VAT_TU_UPDATE, API_VAT_TU_CREATE } from '../../../constants/ApiConstant';
import Axios from 'axios';
import AppUtil from '../../../utils/AppUtil';
import _ from 'lodash'
const layout = {
    labelCol: { span: 7 },
};
const { TextArea } = Input;
class FormUpdate extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            create: props.create
        }
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.data !== this.state.data && !nextProps.create) {
            this.setState({
                data: nextProps.data
            })
            this.form.setFieldsValue(
                nextProps.data
            );
        }
        if (nextProps.create) {
            this.form.setFieldsValue(
                {
                    name: '',
                    inputDate: '',
                    supplier: '',
                    inputWeight: '',
                    realWeight: ''
                }
            );
        }
    }

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_VAT_TU_CREATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Tạo mới dữ liệu thành công!');
                        this.props.loadData && this.props.loadData()
                    }
                    else {
                        AppUtil.ToastError(_.get(data.result, 'SerialNumber.errors[0].errorMessage'));
                    }
                })
                .catch(() => {
                    AppUtil.ToastError();
                })
                .finally(() => {

                });
        } else {
            dataPost.id = data.id
            Axios.post(AppUtil.GLOBAL_API_PATH + API_VAT_TU_UPDATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Cập nhật dữ liệu thành công!');
                        this.props.loadData && this.props.loadData()
                    }
                })
                .catch(() => {
                    AppUtil.ToastError();
                })
                .finally(() => {

                });
        }

    };

    render() {
        const { data, create } = this.state
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish} initialValues={create ? "" : data}>
                <Form.Item name="name" label="Tên vật tư" >
                    <Input />
                </Form.Item>
                <Form.Item name="inputDate" label="Ngày nhập" >
                    <Input />
                </Form.Item>
                <Form.Item name="supplier" label="Nhà cung cấp" >
                    <Input />
                </Form.Item>
                <Form.Item name="inputWeight" label="Khối lượng nhập" >
                    <Input />
                </Form.Item>
                <Form.Item name="realWeight" label="Khối lượng thực tế" >
                    <Input />
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
