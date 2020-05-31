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
          <p className="col-12">
          A service that allows you to sell products in the USA in such a way that keep the same price in each state.
          </p>

          <div className="col-12 text-center">
          <Link className="p-3" to={"/taxes/"}><button type="button" class="btn btn-info btn-lg">Taxes</button></Link>
          <Link className="p-3" to={"/products/"}><button type="button" class="btn btn-success btn-lg">Products</button></Link>
          </div>
        </div>
        <div className="row p-3">
          <h1 className="col-12 text-secondary">About Us</h1>
          <p className="col-12">
          We are students studying at Lodz University of Technology<br /><br />

          Krzysztof Karasiński - Full-stack developer<br />
          Bartłomiej Górkiewicz - Front-end developer<br />
          Tomasz Siekacz - Back-end developer<br />
          </p>
        </div>
        <div className="row p-3">
          <h1 className="col-12 text-secondary">Contact Us</h1>
          <Form className='col-12'>
            <FormGroup>
              <Label>Full Name</Label>
              <Input type="text" name="Name" />
            </FormGroup>
            <FormGroup>
              <Label>Email address</Label>
              <Input type="email" name="Email" />
            </FormGroup>
            <FormGroup>
              <Label>Message</Label>
              <Input type="textarea" rows="7" name="Message" />
            </FormGroup>
            <Button>Submit</Button>
          </Form>
        </div>
      </div>
    );
  }
}
