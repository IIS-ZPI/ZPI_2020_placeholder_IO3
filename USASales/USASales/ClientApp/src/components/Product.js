import React, { Component } from 'react';
import Select from 'react-select';

export class Product extends Component {
    constructor(props) {
        super(props);

        this.id = this.props.match.params.id;

        this.state = {
            product: {
                "product": {
                    "id": 0,
                    "name": "",
                    "category": "",
                    "wholesalePrice": 0,
                    "grossPrice": 0
                },
                "priceInStates": [
                    {
                        "wholesalePrice": 0,
                        "margin": 0,
                        "netPrice": 0,
                        "taxPercentage": 0,
                        "taxValue": 0,
                        "grossPrice": 0,
                        "state": ""
                    }
                ]
            }
        }
    }

    componentDidMount() {
        this.populateTaxData(this.id)
    }

    static renderProductTable(product) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Tax</th>
                        <th>Margin</th>
                        <th>Net price</th>
                    </tr>
                </thead>
                <tbody>
                    {product.priceInStates.map(state =>
                        <tr key={state.id}>
                            <td>{state.state}</td>
                            <td>{Number(state.taxPercentage).toFixed(2)}%</td>
                            <td>${Number(state.margin).toFixed(2)}</td>
                            <td>${Number(state.netPrice).toFixed(2)}</td>
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
				<h1>{this.state.product.product.name}</h1>
				<h2>{this.state.product.product.category}</h2>
				<div className="col mb-3">
					<div className="row">
						Wholesale price: ${this.state.product.product.wholesalePrice}
					</div>
					<div className="row">
						Gross price: ${this.state.product.product.grossPrice}
					</div>
				</div>
                
                {Product.renderProductTable(this.state.product)}
            </div>
        );
    }

    async populateTaxData(id) {
        const response = await fetch('api/products/' + id);
        const data = await response.json();
        this.setState({ product: data });
    }
}