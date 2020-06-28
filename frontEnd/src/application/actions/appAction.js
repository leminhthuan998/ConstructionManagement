//constants
export const LOG_IN_SUCCESSFUL = 'root.LOG_IN_SUCCESSFUL';
export const TOGGLE_SIDER = 'root.toggleMenu';

export const ON_DELETE = 'root.ON_DELETE';

//func

export const loginSuccessFull = (authData) => {
    return {
        type: LOG_IN_SUCCESSFUL,
        payload: {authData}
    };
};

export function onToggleSider() {
    return {
        type: TOGGLE_SIDER,
    };
}

export function onDeleteConfirm (payload)   {
    return {
        type: ON_DELETE,
        payload
    };
}
