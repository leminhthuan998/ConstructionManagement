import { Form, Input, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import React, { Component } from 'react';
import { API_LOAIVT_CREATE, API_LOAIVT_UPDATE } from "../../../constants/ApiConstant";
import AppUtil from "../../../utils/AppUtil";

const layout = {
    labelCol: { span: 3 },

};

class FormUpdate extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            create: props.create,
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
            this.form.resetFields();
        }
    }

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_LOAIVT_CREATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Tạo mới dữ liệu thành công!');
                        this.props.loadData && this.props.loadData()
                    } else {
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

            Axios.post(AppUtil.GLOBAL_API_PATH + API_LOAIVT_UPDATE, dataPost)
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
        const { data, create, dataMac } = this.state;
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish}
                initialValues={create ? "" : data}>
                <Form.Item name="name" label="Tên loại vật tư">
                    <Input type={"text"} />
                </Form.Item>
                <Form.Item name="description" label="Mô tả">
                    <Input.TextArea type={"text"} />
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
