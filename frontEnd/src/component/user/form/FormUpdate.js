import { Form, Input, InputNumber } from 'antd';
import React, { Component } from 'react';
import Axios from 'axios';
import _ from 'lodash'
import {API_USER_CREATE, API_USER_UPDATE } from "../../../constants/ApiConstant";
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
          userName: null,
          email: null,
          fullName: null,
        }
      );
    }
  }

  onFinish = () => {
    const { data } = this.state
    const dataPost = this.form.getFieldsValue()
    if (this.props.create) {
      Axios.post(AppUtil.GLOBAL_API_PATH + API_USER_CREATE, dataPost)
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
      Axios.post(AppUtil.GLOBAL_API_PATH + API_USER_UPDATE, dataPost)
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
    const { data, create } = this.state;
    return (
      <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish}
        initialValues={create ? "" : data}>
        <Form.Item name="userName" label="UserName">
          <Input />
        </Form.Item>
        <Form.Item name="email" label="Email">
          <Input />
        </Form.Item>
        <Form.Item name="fullName" label="Full Name">
          <Input />
        </Form.Item>
        
      </Form>
    );
  }
}

export default FormUpdate;
