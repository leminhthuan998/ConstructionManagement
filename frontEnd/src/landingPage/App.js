


import React, { Component } from 'react'
import Navigation from './Navigation';
import Header from './Header';
// import Features from './features';
// import About from './about';
// import Services from './services';
// import Gallery from './gallery';
// import Testimonials from './testimonials';
// import Team from './Team';
// import Contact from './contact';
import './style/style.css'
// import './style/bootstrap.css'

// import 'bootstrap/dist/css/bootstrap.min.css';

const $ = require('jquery');
window.$ = $;
window.jQuery = $;

export class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            resumeData: {
                Header: {
                    paragraph: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis sed dapibus leo nec ornare diam sed commodo nibh ante facilisis bibendum.",
                    title: "Công ty VLXD ADC"
                }
            },
        }
    }



    // getResumeData() {
    //     $.ajax({
    //         url: '/data.json',
    //         dataType: 'json',
    //         cache: false,
    //         success: function (data) {
    //             this.setState({ resumeData: data });
    //         }.bind(this),
    //         error: function (xhr, status, err) {
    //             console.log(err);
    //             alert(err);
    //         }
    //     });
    // }


    render() {
        return (

            <div>

                <Navigation />
                <Header data={this.state.resumeData.Header} />
                {/* <Features data={this.state.resumeData.Features} />
            <About data={this.state.resumeData.About} />
            <Services data={this.state.resumeData.Services} />
            <Gallery />
            <Testimonials data={this.state.resumeData.Testimonials} />
            <Team data={this.state.resumeData.Team} />
            <Contact data={this.state.resumeData.Contact} /> */}
            </div>
        )

    }
}

export default App
