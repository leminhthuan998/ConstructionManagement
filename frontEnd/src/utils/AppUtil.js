import axios from 'axios';
import { toast } from 'react-toastify';

export default class AppUtil {
    static GLOBAL_DOMAIN_PATH = window.location.protocol + "//" + window.location.host;


    static post = (url, params = {}, config = {}) => {
        return axios.post(url, params, { baseURL: this.GLOBAL_API_PATH });
    };

    static get = (url, params = {}, config = {}) => {
        return axios.get(url, { params, baseURL: this.GLOBAL_API_PATH });
    };

    static ToastSuccess(message = "Cập nhật thành công!") {
        toast.success(message);
    }
    
    static ToastError(message = "Đã có lỗi xảy ra!") {
        toast.error(message);
    }
}