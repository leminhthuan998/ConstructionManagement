import { Form, Input, Select } from 'antd';
import React, { Component } from 'react';
import _ from 'lodash';
import moment from 'moment';

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
            ngayTron: moment.utc(_.get(this.state.data, 'thongTinMeTron.ngayTron')).format("DD/MM/YYYY HH:mm"),
            serialNumber: _.get(this.state.data, 'thongTinMeTron.vehicle.serialNumber'),
            tenHopDong: _.get(this.state.data, 'thongTinMeTron.hopDong.tenHopDong'),
            macCode: _.get(this.state.data, 'thongTinMeTron.mac.macCode'),
            khoiLuong: _.get(this.state.data, 'thongTinMeTron.khoiLuong'),
            troBay: _.get(this.state.data, 'troBay'),
            da1: _.get(this.state.data, 'da1'),
            da2: _.get(this.state.data, 'da2'),
            cat1: _.get(this.state.data, 'cat1'),
            cat2: _.get(this.state.data, 'cat2'),
            xiMang1: _.get(this.state.data, 'xiMang1'),
            xiMang2: _.get(this.state.data, 'xiMang2'),
            nuoc: _.get(this.state.data, 'nuoc'),
            phuGia1: _.get(this.state.data, 'phuGia1'),
            phuGia2: _.get(this.state.data, 'phuGia2')
        })
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.data !== this.state.data) {
            const dataSet = Object.assign(nextProps.data, {
                ngayTron: _.get(nextProps.data, 'thongTinMeTron.ngayTron'),
                serialNumber: _.get(this.state.data, 'thongTinMeTron.vehicle.serialNumber'),
                tenHopDong: _.get(this.state.data, 'thongTinMeTron.hopDong.tenHopDong'),
                macCode: _.get(this.state.data, 'thongTinMeTron.mac.macCode'),
                khoiLuong: _.get(this.state.data, 'thongTinMeTron.khoiLuong'),
                troBay: _.get(this.state.data, 'troBay'),
                da1: _.get(this.state.data, 'da1'),
                da2: _.get(this.state.data, 'da2'),
                cat1: _.get(this.state.data, 'cat1'),
                cat2: _.get(this.state.data, 'cat2'),
                xiMang1: _.get(this.state.data, 'xiMang1'),
                xiMang2: _.get(this.state.data, 'xiMang2'),
                nuoc: _.get(this.state.data, 'nuoc'),
                phuGia1: _.get(this.state.data, 'phuGia1'),
                phuGia2: _.get(this.state.data, 'phuGia2')
            })
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
                    <Input type={"text"} disabled={true} />
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
                <Form.Item name="troBay" label="Tro bay">
                    <Input disabled={true} />
                </Form.Item>
                <Form.Item name="da1" label="Đá 1">
                    <Input type={"text"} disabled={true} />
                </Form.Item>
                <Form.Item name="da2" label="Đá 2">
                    <Input type={"text"} disabled={true} />
                </Form.Item>
                <Form.Item name="cat1" label="Cát 1">
                    <Input type={"text"} disabled={true} />
                </Form.Item>
                <Form.Item name="cat2" label="Cát 2">
                    <Input disabled={true} />
                </Form.Item>
                <Form.Item name="xiMang1" label="Xi măng 1">
                    <Input disabled={true} />
                </Form.Item>
                <Form.Item name="xiMang2" label="Xi măng 2">
                    <Input disabled={true} />
                </Form.Item>
                <Form.Item name="nuoc" label="Nước">
                    <Input disabled={true} />
                </Form.Item>
                <Form.Item name="phuGia1" label="Phụ gia 1">
                    <Input type={"text"} disabled={true} />
                </Form.Item>
                <Form.Item name="phuGia2" label="Phụ gia 2">
                    <Input type={"text"} disabled={true} />
                </Form.Item>
            </Form>
        );
    }
}

export default FormDetail;
