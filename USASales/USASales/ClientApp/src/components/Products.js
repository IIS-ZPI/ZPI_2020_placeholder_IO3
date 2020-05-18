import React, { Component } from 'react';
import { Alert } from 'reactstrap';
import {
    Link,
} from 'react-router-dom';
import './Products.css';

export class Products extends Component {
    constructor(props) {
        super(props);

        this.removeProduct = this.removeProduct.bind(this)

        this.state = {
            products: [],
            error: "",
        }
    }

    componentDidMount() {
        this.populateTaxData()
    }

    removeProduct(event) {
        fetch("api/products/" + event.target.id, {
            method: "DELETE",
        })
        .then(response => {
            if (!response.ok) {
                this.setState({error: "Something went wrong, please try again later"})
            } else {
                window.location.reload();
            }
        })
    }

    renderProductsTable() {
        var products = this.state.products;
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Category</th>
                        <th>Wholesale price</th>
                        <th>Gross price</th>
                        <th>Edit</th>
                        <th>Remove</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.id}>
                            <td><Link to={"/products/details/" + product.id}>{product.name}</Link></td>
                            <td>{product.category}</td>
                            <td>${Number(product.wholesalePrice).toFixed(2)}</td>
                            <td>${Number(product.grossPrice).toFixed(2)}</td>
                            <td className="editBtn"><Link to={"/products/edit/" + product.id}>Edit</Link></td>
                            <td className="removeBtn" id={product.id} onClick={this.removeProduct}>Remove</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        var alert;

        if (this.state.error !== "") {
            alert = <Alert color="danger">{this.state.error}</Alert>
        }

        return (
            <div>
                <div className="row"><h1>Products</h1></div>
                {alert}  
                    
                {this.renderProductsTable()}
            </div>
        );
    }

    async populateTaxData() {
        const response = await fetch('api/products/');
        const data = await response.json();
        this.setState({ products: data });
    }
}