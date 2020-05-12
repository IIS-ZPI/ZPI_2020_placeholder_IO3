import React, { Component } from 'react';
import { Form, FormGroup, Input, Label, Button, Alert } from 'reactstrap';

export class NewProduct extends Component {
    constructor(props) {
        super(props);

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleChange = this.handleChange.bind(this);

        this.state = {
            product: {
                Name: "",
                Category: "",
                WholesalePrice: 0.0,
                GrossPrice: 0.0
            },
            error: "",
            success: ""
        }
    }

    componentDidMount() {

    }

    handleChange(event) {
        var newProduct = this.state.product;
        newProduct[event.target.name] = event.target.value
        this.setState({product: newProduct});
    }

    handleSubmit(event) {
        event.preventDefault();

        var newProduct = this.state.product;

        if (!(newProduct.Name.length > 0 && newProduct.Category.length > 0)) {
            this.setState({error: "Wrong input data"})
            return;
        }

        fetch("api/products", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Name: newProduct.Name,
                Category: newProduct.Category,
                WholesalePrice: parseFloat(newProduct.WholesalePrice),
                GrossPrice: parseFloat(newProduct.GrossPrice),
            })
        })
        .then(response => {
            if (!response.ok) {
                this.setState({error: "Something went wrong, please try again later"})
            } else {
                this.setState({
                    error: "",
                    success: "Product added successfully"
                })
            }
        })
    }

    renderForm() {
        var product = this.state.product
        
        return (
            <Form onSubmit={this.handleSubmit}>
                <FormGroup>
                    <Label>Name:</Label>
                    <Input type="text" name="Name" value={product.Name.value} onChange={this.handleChange} />
                </FormGroup>
                <FormGroup>
                    <Label>Category:</Label>
                    <Input type="text" name="Category" value={product.Category.value} onChange={this.handleChange} />
                </FormGroup>
                <FormGroup>
                    <Label>Wholesale price:</Label>
                    <Input type="number" name="WholesalePrice" value={product.WholesalePrice.value} onChange={this.handleChange} />
                </FormGroup>
                <FormGroup>
                    <Label>Gross price:</Label>
                    <Input type="number" name="GrossPrice" value={product.GrossPrice.value} onChange={this.handleChange} />
                </FormGroup>

                <Button onClick={this.handleSubmit}>Add</Button>
            </Form>
        );
    }

    render() {
        var alert;

        if (this.state.error !== "") {
            alert = <Alert color="danger">{this.state.error}</Alert>
        } else if (this.state.success !== "") {
            alert = <Alert color="success">{this.state.success}</Alert>
        }

        return (
            <div>
                {alert}  
                {this.renderForm()}
            </div>
        );
    }
}