import {Modal} from 'antd';
import axios from 'axios';
import {toast} from 'react-toastify';
import React from 'react';
import {ExclamationCircleOutlined} from '@ant-design/icons';
import store from '../AppStore';
import {onDeleteConfirm} from '../application/actions/appAction';

const { confirm } = Modal;


export default class AppUtil {
    static GLOBAL_DOMAIN_PATH = "/";
    static GLOBAL_API_PATH = "/api";

    static post = (url, params = {}, config = {}) => {
        return axios.post(url, params);
    };

    static get = (url, params = {}, config = {}) => {
        return axios.get(url, { params });
    };

    static ToastSuccess(message = "Cập nhật thành công!") {
        toast.success(message, { autoClose: 2000, draggable: true });
    }

    static ToastError(message = "Đã có lỗi xảy ra!") {
        toast.error(message, { autoClose: 2000, draggable: true });
    }

    static showConfirm() {
        confirm({
            title: 'Bạn có chắc muốn xóa dữ liệu?',
            icon: <ExclamationCircleOutlined />,
            content: '',
            onOk() {
                store.dispatch(onDeleteConfirm(true))
            },
            onCancel() {
                store.dispatch(onDeleteConfirm(false))
            },
        });
    }
}
