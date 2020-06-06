import React, { Component } from 'react';
import { Form, FormGroup, Input, Label, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';

export class AboutUs extends Component {

  render () {
    return (
      <div>
        <div className="row p-3">
          <h1 className="col-12 text-secondary">About Us</h1>
          <p className="col-12" style={{fontSize: "1.3rem"}}>
          We are students studying at Lodz University of Technology<br /><br />

          Krzysztof Karasiński - Full-stack developer<br />
          Bartłomiej Górkiewicz - Front-end developer<br />
          Tomasz Siekacz - Back-end developer<br />
          </p>
        </div>
      </div>
    );
  }
}
