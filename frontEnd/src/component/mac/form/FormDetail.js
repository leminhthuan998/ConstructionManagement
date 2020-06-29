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
              <Form.Item name="macCode" label="MAC CODE">
                <Input type={"text"}  disabled={true}/>
              </Form.Item>
              <Form.Item name="macName" label="Tên loại bê tông">
                <Input type={"text"} />
              </Form.Item>
              <Form.Item name="tuoi" label="Tuổi">
                <InputNumber />
              </Form.Item>
              <Form.Item name="doSut" label="Độ sụt">
                <InputNumber />
              </Form.Item>
              <Form.Item name="cat" label="Cát">
                <InputNumber />
              </Form.Item>
              <Form.Item name="xiMang" label="Xi Măng">
                <InputNumber />
              </Form.Item>
              <Form.Item name="da" label="Đá">
                <InputNumber />
              </Form.Item>
              <Form.Item name="pg" label="Phụ gia">
                <InputNumber />
              </Form.Item>
              <Form.Item name="nuoc" label="Nước">
                <InputNumber />
              </Form.Item>
              <Form.Item name="note" label="Mô tả">
                <TextArea />
              </Form.Item>
            </Form>
        );
    }
}

export default FormDetail;
