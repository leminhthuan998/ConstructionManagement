import { DatePicker, Form, Input, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import React, { Component } from 'react';
import { API_VAT_TU_CREATE, API_VAT_TU_UPDATE } from '../../../constants/ApiConstant';
import AppUtil from '../../../utils/AppUtil';
import moment from 'moment';

const layout = {
    labelCol: { span: 4 },
};
const { TextArea } = Input;
class FormUpdate extends Component {
    constructor(props) {
        super(props);
        const me = this
        this.state = {
            data: props.data ? me.formatValue(props.data) : {},
            create: props.create,
            dataLVT: _.get(props, 'dataLVT')

        }
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
        if (nextProps.data && nextProps.data !== this.state.data && !nextProps.create) {
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

    onFinish = () => {
        const { data } = this.state
        const dataPost = this.form.getFieldsValue()
        if (this.props.create) {
            Axios.post(AppUtil.GLOBAL_API_PATH + API_VAT_TU_CREATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Tạo mới dữ liệu thành công!');
                        this.props.loadData && this.props.loadData()
                    }
                    else {
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
            Axios.post(AppUtil.GLOBAL_API_PATH + API_VAT_TU_UPDATE, dataPost)
                .then(res => {
                    const { data } = res;
                    if (data.success) {
                        AppUtil.ToastSuccess('Cập nhật dữ liệu thành công!');
                        this.props.loadData && this.props.loadData()
                    }
                    else {
                        AppUtil.ToastError(_.get(data.result, 'SerialNumber.errors[0].errorMessage'));
                    }
                })
                .catch(() => {
                    AppUtil.ToastError();
                })
                .finally(() => {

                });
        }

    };

    render() {
        const { data, create, dataLVT } = this.state
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish} initialValues={create ? "" : data}>
                <Form.Item name="name" label="Tên vật tư" >
                    <Input />
                </Form.Item>
                <Form.Item name="inputDate" label="Ngày nhập" >
                    <DatePicker  format={'DD/MM/YYYY'} />
                </Form.Item>
                <Form.Item name="supplier" label="Nhà cung cấp" >
                    <Input />
                </Form.Item>
                <Form.Item name="inputWeight" label="Khối lượng nhập" >
                    <Input />
                </Form.Item>
                <Form.Item name="realWeight" label="Khối lượng thực tế" >
                    <Input />
                </Form.Item>
                <Form.Item name="loaiVatTuId" label="Loại vật tư">
                    <Select
                        placeholder="Chọn loại vật tư"
                        // onChange={() => this.onChangeMac()}
                        allowClear
                    >
                        {_.map(dataLVT, item => {
                            return <Select.Option value={item.id} >
                                {item.name}
                            </Select.Option>
                        })}
                    </Select>
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
