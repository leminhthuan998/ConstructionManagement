import axios from 'axios';
import { toast } from 'react-toastify';

export default class AppUtil {
    static GLOBAL_DOMAIN_PATH = "/";
    static GLOBAL_API_PATH = "/api";

    static post = (url, params = {}, config = {}) => {
        return axios.post(url, params);
    };

    static get = (url, params = {}, config = {}) => {
        return axios.get(url, { params});
    };

    static ToastSuccess(message = "Cập nhật thành công!") {
        toast.success(message, {autoClose: 2000, draggable: true});
    }
    
    static ToastError(message = "Đã có lỗi xảy ra!") {
        toast.error(message, {autoClose: 2000, draggable: true});
    }
}