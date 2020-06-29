import {Form, Input} from 'antd';
import React, {Component} from 'react';
import Axios from 'axios';
import _ from 'lodash'
import {API_CREATE_USER, API_MAC_UPDATE} from "../../../constants/ApiConstant";
import AppUtil from "../../../utils/AppUtil";

const layout = {
    labelCol: { span: 6 },
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
                    serialNumber: '',
                    description: ''
                }
            );
        }
    }

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_CREATE_USER, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Cập nhật dữ liệu thành công!');
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
            Axios.post(AppUtil.GLOBAL_API_PATH + API_MAC_UPDATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Tạo mới dữ liệu thành công!');
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
                <Form.Item name="name" label="Tên phương tiện" >
                    <Input />
                </Form.Item>
                <Form.Item name="serialNumber" label="Biển số" >
                    <Input />
                </Form.Item>
                <Form.Item name="description" label="Mô tả" >
                    <TextArea />
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
