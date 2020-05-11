import React, { Component } from 'react';
import Select from 'react-select';
import {
    Link,
} from 'react-router-dom';

export class Products extends Component {
    constructor(props) {
        super(props);

        this.state = {
            products: [],
        }
    }

    componentDidMount() {
        this.populateTaxData()
    }

    static renderProductsTable(products) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Category</th>
                        <th>Wholesale price</th>
                        <th>Gross price</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.id}>
                            <td><Link to={"/products/" + product.id}>{product.name}</Link></td>
                            <td>{product.category}</td>
                            <td>${Number(product.wholesalePrice).toFixed(2)}</td>
                            <td>${Number(product.grossPrice).toFixed(2)}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        const { selectedState } = this.state;

        return (
            <div>
                <h1>Products</h1>
                {Products.renderProductsTable(this.state.products)}
            </div>
        );
    }

    async populateTaxData() {
        const response = await fetch('api/products/');
        const data = await response.json();
        this.setState({ products: data });
    }
}