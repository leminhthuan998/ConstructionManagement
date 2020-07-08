import { Form, Input, InputNumber, Select } from 'antd';
import Axios from 'axios';
import _ from 'lodash';
import React, { Component } from 'react';
import { API_THANH_PHAN_CAN_UPDATE } from "../../../constants/ApiConstant";
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
            dataMeTron: _.get(props, 'dataMeTron')
        }
    }

    componentDidMount() {
        this.form.setFieldsValue({
            meTronId: _.get(this.state.data, 'thongTinMeTron.id')
        })
    }

    componentWillReceiveProps(nextProps) {
        if(!nextProps.create){
            this.form.setFieldsValue({
                meTronId: _.get(this.state.data, 'thongTinMeTron.id')
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
            // Axios.post(AppUtil.GLOBAL_API_PATH + API_HOP_DONG_CREATE, dataPost)
            //     .then(res => {
            //         const { data } = res;
            //         if (data.success) {
            //             AppUtil.ToastSuccess('Tạo mới dữ liệu thành công!');
            //             this.props.loadData && this.props.loadData()
            //         } else {
            //             AppUtil.ToastError(_.get(data.result, 'SerialNumber.errors[0].errorMessage'));
            //         }
            //     })
            //     .catch(() => {
            //         AppUtil.ToastError();
            //     })
            //     .finally(() => {

            //     });
        } else {
            dataPost.id = data.id
            Axios.post(AppUtil.GLOBAL_API_PATH + API_THANH_PHAN_CAN_UPDATE, dataPost)
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

    // renderOptionsMac() {
    //     const { dataMeTron } = this.state
    //     _.map(dataMeTron, item => {
    //         return <Option value={item.id} >
    //             {item.macCode}
    //         </Option>
    //     })
    // }

    onChangeMac() {

    }

    render() {
        const { data, create, dataMeTron } = this.state;
        return (
            <Form ref={c => this.form = c} {...layout} name="basic" onFinish={this.onFinish}
                initialValues={create ? "" : data}>
                <Form.Item name="troBay" label="Tro bay">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="da1" label="Đá 1">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="da2" label="Đá 2">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="cat1" label="Cát 1">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="cat2" label="Cát 2">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="xiMang1" label="Xi măng 1">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="xiMang2" label="Xi măng 2">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="nuoc" label="Nước">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="phuGia1" label="Phụ gia 1">
                    <InputNumber/>
                </Form.Item>
                <Form.Item name="phuGia2" label="Phụ gia 2">
                    <InputNumber/>
                </Form.Item>
            </Form>
        );
    }
}

export default FormUpdate;
