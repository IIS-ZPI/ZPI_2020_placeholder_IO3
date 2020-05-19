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
            categories: [],
            error: "",
            success: ""
        }
    }

    componentDidMount() {
        this.fetchAllCategories();
    }

    fetchAllCategories() {
        fetch("api/categories")
        .then(response => response.json())
        .then(response => {
            this.setState({
                categories: response
            })
        })
        .catch(error => {
            this.setState({
                error: "Couldn't fetch data from api/categories",
                success: ""
            })
        })
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
                CategoryId: parseInt(newProduct.Category),
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
        var product = this.state.product;
        var categories = this.state.categories.map(c => <option value={c.id}>{c.name}</option>);
        
        return (
            <Form onSubmit={this.handleSubmit}>
                <FormGroup>
                    <Label>Name:</Label>
                    <Input type="text" name="Name" value={product.Name.value} onChange={this.handleChange} />
                </FormGroup>
                <FormGroup>
                    <Label>Category:</Label>
                    <Input  type="select" name="Category" value={product.Category.value} onChange={this.handleChange}>
                        {categories}
                    </Input>
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