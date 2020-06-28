import { LOG_IN_SUCCESSFUL, TOGGLE_SIDER, ON_DELETE } from '../actions/appAction';
import { isMobile } from 'react-device-detect';

const initialState = {
    isAuthenticated: false,
    showSider: isMobile ? false : true,
    onDelete: false
};

export default (state = initialState, action) => {
    switch (action.type) {
        case LOG_IN_SUCCESSFUL:
            return {
                ...state,
                isAuthenticated: true,
                authData: action.payload
            };
        case TOGGLE_SIDER:
            return {
                ...state,
                showSider: !state.showSider
            };
        case ON_DELETE:
            return {
                ...state,
                onDelete: action.payload
            };
        default:
            return state;
    }
};



