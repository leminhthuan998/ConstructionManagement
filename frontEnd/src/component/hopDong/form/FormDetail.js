import { Form, Input, Select } from 'antd';
import React, { Component } from 'react';
import _ from 'lodash'
const layout = {
  labelCol: { span: 5 },
};

const { Option } = Select;

class FormDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: props.data
    }
  }

  componentDidMount() {
    this.form.setFieldsValue({
      macCode: _.get(this.state.data, 'mac.macCode')
    })
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.data !== this.state.data) {
      const dataSet =Object.assign(nextProps.data, { macCode: _.get(nextProps.data, 'mac.macCode')})      
      this.setState({
        data: nextProps.data
      })
      this.form.setFieldsValue(
        dataSet
      );     
    }
  }

  render() {
    const { data } = this.state
    return (
      <Form ref={c => this.form = c} {...layout} name="basic" initialValues={this.state.data}>
        <Form.Item name="tenHopDong" label="Tên hợp đồng">
          <Input type={"text"} disabled={true} />
        </Form.Item>
        <Form.Item name="chuDauTu" label="Chủ đầu tư">
          <Input type={"text"} disabled={true} />
        </Form.Item>
        <Form.Item name="nhaThau" label="Nhà thầu">
          <Input disabled={true} />
        </Form.Item>
        <Form.Item name="nhaCungCapBeTong" label="Nhà cung cấp bê tông">
          <Input disabled={true} />
        </Form.Item>
        <Form.Item name="macCode" label="Loại bê tông">
          <Input disabled={true} />
        </Form.Item>

      </Form>
    );
  }
}

export default FormDetail;
