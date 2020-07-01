import React, { Component } from 'react'

export class Navigation extends Component {


  render() {
    return (
      <nav id="menu" className="navbar navbar-default fixed-top">
        <div className="container">
          <div className="navbar-header">
            <button type="button" className="navbar-toggler collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"> <span className="sr-only">Toggle navigation</span> <span className="icon-bar"></span> <span className="icon-bar"></span> <span className="icon-bar"></span> </button>
            <a className="navbar-brand page-scroll" href="#page-top">React Landing Page</a> </div>

          <div className="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul className="navbar-nav mr-auto mt-2 mt-lg-0">
              <li class="nav-item"><a href="#features" className="page-scroll">Features</a></li>
              <li class="nav-item"><a href="#about" className="page-scroll">About</a></li>
              <li class="nav-item"><a href="#services" className="page-scroll">Services</a></li>
              <li class="nav-item"><a href="#portfolio" className="page-scroll">Gallery</a></li>
              <li class="nav-item"><a href="#testimonials" className="page-scroll">Testimonials</a></li>
              <li class="nav-item"><a href="#team" className="page-scroll">Team</a></li>
              <li class="nav-item"><a href="#contact" className="page-scroll">Contact</a></li>
            </ul>
          </div>
        </div>
      </nav>
    )
  }
}

export default Navigation