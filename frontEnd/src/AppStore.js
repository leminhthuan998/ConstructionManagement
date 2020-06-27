import { applyMiddleware, combineReducers, createStore } from "redux";
import { routerMiddleware } from "react-router-redux";
import thunk from "redux-thunk";
import appReducer from "./application/reducers/appReducer";


import history from './history';

/**
 * @param history
 * @returns {Store<any, AnyAction> & Store<S & {}, A> & {dispatch: Dispatch<A>}}
 */
const createAppStore = (authService, history) => {
    const reducers = combineReducers({
        root: appReducer
    });

    const middleWares = [
        thunk,
        routerMiddleware(history),
    ];

    return createStore(
        reducers,
        applyMiddleware(...middleWares)
    );
};


const store = createAppStore({}, history);

store.history = history;
export default store;