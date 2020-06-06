import React, { Component } from 'react';
import { Form, FormGroup, Input, Label, Button, Alert } from 'reactstrap';
import { Link } from 'react-router-dom';

export class ContactUs extends Component {

  render () {
    return (
      <div>
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
