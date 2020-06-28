import React, { Component, Fragment } from 'react';
import { HashRouter, Route, Switch, Redirect } from 'react-router-dom';
import './scss/style.scss';
import { Provider } from 'react-redux';
import appStore from "./AppStore";
import { ToastContainer } from 'react-toastify';

const loading = (
  <div className="pt-3 text-center">
    <div className="sk-spinner sk-spinner-pulse"></div>
  </div>
)

// Containers
const TheLayout = React.lazy(() => import('./containers/TheLayout'));

// Pages
const Login = React.lazy(() => import('./views/pages/login/Login'));
const Register = React.lazy(() => import('./views/pages/register/Register'));
const Page404 = React.lazy(() => import('./views/pages/page404/Page404'));
const Page500 = React.lazy(() => import('./views/pages/page500/Page500'));

class App extends Component {

  render() {
    return (
      <Fragment>
        <ToastContainer position='top-right' />
        <Provider store={appStore}>
          <HashRouter>
            <React.Suspense fallback={loading}>
              <Switch>
                <Route path="/" name="Home" render={props => <TheLayout {...props} />} />
                <Route exact path="/login" name="Login Page" render={props => <Login {...props} />} />
                <Route exact path="/register" name="Register Page" render={props => <Register {...props} />} />
                <Route  path="*" name="Page 404" component={Page404} />
                <Route exact path="/500" name="Page 500" render={props => <Page500 {...props} />} />
              </Switch>
            </React.Suspense>
          </HashRouter>
        </Provider>
      </Fragment>

    );
  }
}

export default App;
