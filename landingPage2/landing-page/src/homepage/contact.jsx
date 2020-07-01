import React, { Component } from "react";
import {
  GoogleMap,
  Marker, withGoogleMap, withScriptjs
} from "react-google-maps";

const MyMapComponent = withScriptjs(
  withGoogleMap(props => {
    return (
      <GoogleMap
        defaultZoom={16}
        defaultCenter={{ lat: 12.080436, lng: 109.188756 }}
      >
        <Marker position={{ lat: 12.080436, lng: 109.188756 }} />
      </GoogleMap>
    );
  })
);
export class Contact extends Component {
  static propTypes = {

  }



  render() {
    return (
      <div>
        <div id="contact">
          <div className="container">
            <div className="section-title">
              <h2 style={{display:'flex', justifyContent:'center'}}>Liên hệ</h2>
              <p style={{display:'flex', justifyContent:'center'}}>Hãy liên lạc với chúng tôi qua các kênh thông tin sau.</p>
            </div>
            <div className="col-md-8">
              <div className="row">

                <MyMapComponent
                  googleMapURL="https://maps.googleapis.com/maps/api/js?key=AIzaSyALLEWvdI0oAzAPfEydwednIX6173KnBUQ&v=3.exp&libraries=geometry,drawing,places"
                  loadingElement={<div style={{ height: `100%` }} />}
                  containerElement={<div style={{ height: `400px` }} />}
                  mapElement={<div style={{ height: `100%` }} />}
                />
              </div>
            </div>
            <div className="col-md-3 col-md-offset-1 contact-info" style={{ marginTop: 60 }}>
              <div className="contact-item">
                {/* <h3>Contact Info</h3> */}
                <p><span><i className="fa fa-map-marker"></i> Address</span>{this.props.data ? this.props.data.address : 'loading'}</p>
              </div>
              <div className="contact-item">
                <p><span><i className="fa fa-phone"></i> Phone</span> {this.props.data ? this.props.data.phone : 'loading'}</p>
              </div>
              <div className="contact-item">
                <p><span><i className="fa fa-envelope-o"></i> Email</span> {this.props.data ? this.props.data.email : 'loading'}</p>
              </div>
            </div>
            <div className="col-md-12">
              <div className="row">
                <div className="social">
                  <ul>
                    <li><a href={this.props.data ? this.props.data.facebook : '/'}><i className="fa fa-facebook"></i></a></li>
                    <li><a href={this.props.data ? this.props.data.twitter : '/'}><i className="fa fa-twitter"></i></a></li>
                    <li><a href={this.props.data ? this.props.data.youtube : '/'}><i className="fa fa-youtube"></i></a></li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div id="footer">
          <div className="container text-center">
            <p>&copy; 2020 Bản quyền thuộc về Công ty xây dựng Ánh Dương. </p>
          </div>
        </div>
      </div>
    )
  }
}

export default Contact
