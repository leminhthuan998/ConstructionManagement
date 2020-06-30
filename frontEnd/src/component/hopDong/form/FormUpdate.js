import { Form, Input, InputNumber, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import React, { Component } from 'react';
import { API_HOP_DONG_CREATE, API_HOP_DONG_UPDATE } from "../../../constants/ApiConstant";
import AppUtil from "../../../utils/AppUtil";
const { Option } = Select;

const layout = {
    labelCol: { span: 6 },
};

class FormUpdate extends Component {
    constructor(props) {
        super(props);
        this.state = {
            data: props.data,
            create: props.create,
            dataMac: _.get(props, 'dataMac')
        }
    }

    componentDidMount() {
        this.form.setFieldsValue({
            macId: _.get(this.state.data, 'mac.id')
        })
    }

    componentWillReceiveProps(nextProps) {
        if(!nextProps.create){
            this.form.setFieldsValue({
                macId: _.get(nextProps.data, 'mac.id')
            })
        }
        if (nextProps.data !== this.state.data && !nextProps.create) {
            this.setState({
                data: nextProps.data
            })
            this.form.setFieldsValue(
                nextProps.data
            );           
        }
        if (nextProps.create) {
            this.form.resetFields();
        }
    }

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_HOP_DONG_CREATE, dataPost)
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

            Axios.post(AppUtil.GLOBAL_API_PATH + API_HOP_DONG_UPDATE, dataPost)
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

    onChangeMac() {

    }

    render() {
        const { data, create, dataMac } = this.state;
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish}
                initialValues={create ? "" : data}>
                <Form.Item name="tenHopDong" label="Tên hợp đồng">
                    <Input type={"text"} />
                </Form.Item>
                <Form.Item name="chuDauTu" label="Chủ đầu tư">
                    <Input type={"text"} />
                </Form.Item>
                <Form.Item name="nhaThau" label="Nhà thầu">
                    <Input />
                </Form.Item>
                <Form.Item name="nhaCungCapBeTong" label="Nhà cung cấp bê tông">
                    <Input />
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
            </Form>
        );
    }
}

export default FormUpdate;
