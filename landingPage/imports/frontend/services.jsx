import React, { Component } from 'react'

export class Services extends Component {
  render() {
    return (
        <div id="services" className="text-center">
        <div className="container">
          <div className="section-title">
            <h2>Dịch vụ</h2>
            <p>Cam kết đem lại sự hài lòng tuyệt đối cho khách hàng.</p>
          </div>
          <div className="row">

          {this.props.data ? this.props.data.map(d => 
              <div className="col-md-4"> <i className={d.icon}></i>
              <div className="service-desc">
                <h3>{d.name}</h3>
                <p>{d.text}</p>
              </div>
            </div>
          ): 'loading'}
          
           
          
        
            </div>
          </div>
        </div>
    )
  }
}

export default Services
