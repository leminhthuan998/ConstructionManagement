import React, { Component } from 'react'
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import {App} from "./homepage/App";

const AppRouter = () => (
    <Router>
        <div>
        <Route path="/"  exact component={App} />
        </div>
    </Router>
  );
  
  export default AppRouter;
  