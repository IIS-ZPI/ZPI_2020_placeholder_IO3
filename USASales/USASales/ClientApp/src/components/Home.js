import React, { Component } from 'react';
import { Form, FormGroup, Input, Label, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <div className="row p-3">
          <h1 className="col-12 text-secondary">What is it?</h1>
          <p className="col-12" style={{fontSize: "1.5rem"}}>
          A service that allows you to sell products in the USA in such a way that keep the same price in each state.
          </p>

          <div className="col-12 text-center">
          <Link className="p-3" to={"/taxes/"}><button type="button" class="btn btn-info btn-lg"  style={{fontSize: "1.5rem"}}>Taxes</button></Link>
          <Link className="p-3" to={"/products/"}><button type="button" class="btn btn-success btn-lg"  style={{fontSize: "1.5rem"}}>Products</button></Link>
          </div>
        </div>
        
      </div>
    );
  }
}
