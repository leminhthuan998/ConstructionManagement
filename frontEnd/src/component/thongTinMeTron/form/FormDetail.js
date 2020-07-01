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
      macCode: _.get(this.state.data, 'mac.macCode'),
      tenHopDong: _.get(this.state.data, 'hopDong.tenHopDong'),
      serialNumber: _.get(this.state.data, 'vehicle.serialNumber')
    })
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.data !== this.state.data) {
      const dataSet =Object.assign(nextProps.data, { macCode: _.get(nextProps.data, 'mac.macCode'), tenHopDong: _.get(this.state.data, 'hopDong.tenHopDong'), serialNumber: _.get(this.state.data, 'vehicle.serialNumber')})      
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
        <Form.Item name="ngayTron" label="Ngày trộn">
          <Input type={"datetime-local"} disabled={true} />
        </Form.Item>
        <Form.Item name="serialNumber" label="Số xe">
          <Input type={"text"} disabled={true} />
        </Form.Item>
        <Form.Item name="tenHopDong" label="Tên hợp đồng">
          <Input disabled={true} />
        </Form.Item>
        <Form.Item name="macCode" label="Loại bê tông">
          <Input disabled={true} />
        </Form.Item>
        <Form.Item name="khoiLuong" label="Khối lượng">
          <Input disabled={true} />
        </Form.Item>

      </Form>
    );
  }
}

export default FormDetail;
