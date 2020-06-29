import React, { Component } from 'react';
import { Form, Input, Button, Select } from 'antd';
const layout = {
    labelCol: { span: 7 },
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
                <Form.Item name="name" label="Tên vật tư" >
                    <Input disabled />
                </Form.Item>
                <Form.Item name="inputDate" label="Ngày nhập" >
                    <Input disabled />
                </Form.Item>
                <Form.Item name="supplier" label="Nhà cung cấp" >
                    <Input disabled />
                </Form.Item>
                <Form.Item name="inputWeight" label="Khối lượng nhập" >
                    <Input disabled />
                </Form.Item>
                <Form.Item name="realWeight" label="Khối lượng thực tế" >
                    <Input disabled />
                </Form.Item>
                
            </Form>
        );
    }
}

export default FormDetail;