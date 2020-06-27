import { LOG_IN_SUCCESSFUL } from '../actions/appAction';

const initialState = {
    isAuthenticated: false,
};

export default (state = initialState, action) => {
    switch (action.type) {
        case LOG_IN_SUCCESSFUL:
            return {
                ...state,
                isAuthenticated: true,
                authData: action.payload
            };
        

        default:
            return state;
    }
};



