import { DatePicker, Form, Input } from 'antd';
import moment from 'moment';
import React, { Component } from 'react';
import _ from 'lodash'

const layout = {
    labelCol: { span: 4 },
};
class FormDetail extends Component {
    constructor(props) {
        super(props);
        const me = this
        this.state = {
            data: props.data ? me.formatValue(props.data) : {}
        }
    }

    componentDidMount() {
        this.form.setFieldsValue({
            loaiVatTu: _.get(this.state.data, 'loaiVatTu.name')
        })
    }

    formatValue(data) {
        const obj = {}
        Object.keys(data).forEach(function (key) {
            if (key == 'inputDate') {
                obj[key] = moment(data[key])
            } else {
                obj[key] = data[key]
            }
        })
        return obj
    }

    componentWillReceiveProps(nextProps) {
        const me = this
        if (nextProps.data && nextProps.data !== this.state.data) {
            const dataSet = Object.assign(nextProps.data, { loaiVatTu: _.get(nextProps.data, 'loaiVatTu.name') })
            this.setState({
                data: me.formatValue(nextProps.data)
            })
            this.form.setFieldsValue(
                me.formatValue(dataSet)
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
                    <DatePicker disabled format={'DD/MM/YYYY'} />
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
                <Form.Item name="loaiVatTu" label="Loại vật tư" >
                    <Input disabled />
                </Form.Item>

            </Form>
        );
    }
}

export default FormDetail;