import { Form, Input, InputNumber, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import React, { Component } from 'react';
import { API_TTMT_CREATE, API_TTMT_UPDATE } from "../../../constants/ApiConstant";
import AppUtil from "../../../utils/AppUtil";
import moment from 'moment';

const { Option } = Select;

const layout = {
    labelCol: { span: 6 },
};

class FormUpdate extends Component {
    constructor(props) {
        super(props);
        const me = this
        this.state = {
            data: props.data ? me.formatValue(props.data) : {},
            create: props.create,
            dataMac: _.get(props, 'dataMac'),
            dataHopDong: _.get(props, 'dataHopDong'),
            dataVehicle: _.get(props, 'dataVehicle')
        }
    }

    componentDidMount() {
        console.log("FormUpdate -> componentDidMount -> this.state.data", this.state.data)
        this.form.setFieldsValue({
            macId: _.get(this.state.data, 'mac.id'),
            hopDongId: _.get(this.state.data, 'hopDong.id'),
            vehicleId: _.get(this.state.data, 'vehicle.id')
        })
    }

    componentWillReceiveProps(nextProps) {
        if (!nextProps.create) {
            this.form.setFieldsValue({
                macId: _.get(nextProps.data, 'mac.id'),
                hopDongId: _.get(nextProps.data, 'hopDong.id'),
                vehicleId: _.get(nextProps.data, 'vehicle.id')
            })
        }
        if (nextProps.data !== this.state.data && !nextProps.create) {
            this.setState({
                data: this.formatValue(nextProps.data)
            })
            this.form.setFieldsValue(
                this.formatValue(nextProps.data)
            );
        }
        if (nextProps.create) {
            this.form.resetFields();
        }
    }

    formatValue(data) {
        const obj = {}
        Object.keys(data).forEach(function (key) {
            if (key == 'ngayTron') {
                obj[key] = moment(data[key])
            } else {
                obj[key] = data[key]
            }
        })
        return obj
    }

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_TTMT_CREATE, dataPost)
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

            Axios.post(AppUtil.GLOBAL_API_PATH + API_TTMT_UPDATE, dataPost)
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

    renderOptionsMac() {
        const { dataMac } = this.state
        _.map(dataMac, item => {
            return <Option value={item.id} >
                {item.macCode}
            </Option>
        })
    }

    renderOptionsHopDong() {
        const { dataHopDong } = this.state
        _.map(dataHopDong, item => {
            return <Option value={item.id} >
                {item.tenHopDong}
            </Option>
        })
    }

    renderOptionsMac() {
        const { dataVehicle } = this.state
        _.map(dataVehicle, item => {
            return <Option value={item.id} >
                {item.serialNumber}
            </Option>
        })
    }

    onChangeMac() {

    }

    onChangeHopDong() {

    }

    onChangeVehicle() {


    }

    render() {
        const { data, create, dataMac, dataHopDong, dataVehicle } = this.state;
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish}
                initialValues={create ? "" : data}>
                <Form.Item name="ngayTron" label="Ngày trộn">
                    <Input type={"datetime-local"} />
                </Form.Item>

                <Form.Item name="vehicleId" label="Số xe">
                    <Select
                        placeholder="Hợp đồng"
                        onChange={() => this.onChangeVehicle()}
                        allowClear
                    >
                        {_.map(dataVehicle, item => {
                            return <Option value={item.id} >
                                {item.serialNumber}
                            </Option>
                        })}
                    </Select>
                </Form.Item>

                <Form.Item name="hopDongId" label="Tên hợp đồng">
                    <Select
                        placeholder="Hợp đồng"
                        onChange={() => this.onChangeHopDong()}
                        allowClear
                    >
                        {_.map(dataHopDong, item => {
                            return <Option value={item.id} >
                                {item.tenHopDong}
                            </Option>
                        })}
                    </Select>
                </Form.Item>

                <Form.Item name="macId" label="Loại bê tông">
                    <Select
                        placeholder="Chọn loại bê tông"
                        onChange={() => this.onChangeMac()}
                        allowClear
                    >
                        {_.map(dataMac, item => {
                            return <Option value={item.id} >
                                {item.macCode}
                            </Option>
                        })}
                    </Select>
                </Form.Item>

                <Form.Item name="khoiLuong" label="Khối lượng">
                    <InputNumber />
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
