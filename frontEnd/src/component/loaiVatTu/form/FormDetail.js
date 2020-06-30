import { Form, Input, Select } from 'antd';
import React, { Component } from 'react';
import _ from 'lodash'
const layout = {
    labelCol: { span: 3 },
};

const { Option } = Select;

class FormDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data
        }
    }

    componentDidMount(){
      this.form.setFieldsValue({
        macCode: _.get(this.state.data,'mac.macCode')
      })
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.data !== this.state.data) {
            this.setState({
                data: nextProps.data
            })
            this.form.setFieldsValue(
                nextProps.data
            );
            this.form.setFieldsValue({
              macCode: _.get(nextProps.data,'mac.macCode')
            })
        }
    }

    render() {
      const {data} = this.state
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" initialValues={this.state.data}>
              <Form.Item name="name" label="Tên loại vật tư">
                <Input type={"text"}  disabled={true}/>
              </Form.Item>
              <Form.Item name="description" label="Mô tả">
                <Input.TextArea type={"text"} disabled={true}/>
              </Form.Item>
             
            </Form>
        );
    }
}

export default FormDetail;
