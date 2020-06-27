//constants
export const LOG_IN_SUCCESSFUL = 'root.LOG_IN_SUCCESSFUL';


//func

export const loginSuccessFull = (authData) => {
    return {
        type: LOG_IN_SUCCESSFUL,
        payload: {authData}
    };
};

