import React, { Component } from 'react';
import Select from 'react-select';

export class Product extends Component {
    constructor(props) {
        super(props);

        this.id = this.props.match.params.id;

        this.state = {
            product: {
                name: "Test product",
                basePrice: 15,
                states: [
                    {
                        name: "Alabama",
                        taxPercentage: 23,
                        price: 15+15*0.23
                    },
                    {
                        name: "Test",
                        taxPercentage: 24,
                        price: 15+15*0.4
                    }
                ]
            }
        }
    }

    componentDidMount() {
        this.populateTaxData('Alabama')
    }

    static renderProductTable(product) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Tax</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    {product.states.map(product =>
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{Number(product.taxPercentage).toFixed(2)}%</td>
                            <td>${Number(product.price).toFixed(2)}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    handleStateSelectionChange = selectedState => {
        this.setState(
            { selectedState }
        );

        this.populateTaxData(selectedState.value);
    };

    render() {
        const { selectedState } = this.state;

        return (
            <div>
                <div className="row">
                    <div className="col-6"><h2>{this.state.product.name}</h2></div>
                    <div className="col-6"><h4>Base price: {this.state.product.basePrice}$</h4></div>
                </div>
                
                
                
                {Product.renderProductTable(this.state.product)}
            </div>
        );
    }

    async populateTaxData(state) {
        const response = await fetch('api/product/' + state);
        const data = await response.json();
        this.setState({ product: data });
    }
}