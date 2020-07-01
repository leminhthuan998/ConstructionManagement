import React, { Component } from 'react';
import {Form, Input, Button, Select, InputNumber} from 'antd';
const layout = {
    labelCol: { span: 6 },
    // wrapperCol: { span: 16 },
};
const { TextArea } = Input;

class FormDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data
        }
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.data !== this.state.data) {
            this.setState({
                data: nextProps.data
            })
            this.form.setFieldsValue(
                nextProps.data
            );
        }
    }

    render() {
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" initialValues={this.state.data}>
              <Form.Item name="name" label="Tên chức vụ">
                <Input disabled={true}/>
              </Form.Item>
              <Form.Item name="concurrencyStamp" label="Concurrency Stamp">
                <Input disabled/>
              </Form.Item>
              <Form.Item name="normalizedName" label="Normalized Name">
                <Input disabled/>
              </Form.Item>
            </Form>
        );
    }
}

export default FormDetail;
