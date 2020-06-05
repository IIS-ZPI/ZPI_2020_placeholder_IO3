import React, { Component } from 'react';
import Select from 'react-select';
import { faSort } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './../custom.css';

export class Product extends Component {
    constructor(props) {
        super(props);

        this.id = this.props.match.params.id;
        this.onSort = this.onSort.bind(this)

        this.state = {
			quantity: 1,
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
            },
            tableSort: [
                {'state': 0},
                {'taxPercentage': 0},
                {'margin': 0},
                {'netPrice': 0},
            ]
        }
		
		this.handleTaxChange = this.handleTaxChange.bind(this);
    }

    componentDidMount() {
        this.populateTaxData(this.id, 1)
    }

    onSort(event, sortKey, direction){
        const data = this.state.product.priceInStates;
        const tableSort = this.state.tableSort;

        if (direction == 'asc') {
            if (typeof data[0][sortKey] == "number") {
                data.sort((a,b) => a[sortKey] - b[sortKey]);
            } else {
                data.sort((a,b) => a[sortKey].localeCompare(b[sortKey]))
            }
            tableSort[sortKey] = 1;
        } else {
            if (typeof data[0][sortKey] == "number") {
                data.sort((a,b) => a[sortKey] - b[sortKey]).reverse();
            } else {
                data.sort((a,b) => a[sortKey].localeCompare(b[sortKey])).reverse()
            }
            tableSort[sortKey] = 0;
        }
        this.setState({data})
    }

    renderProductTable() {
        const product = this.state.product;
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Name <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'state', this.state.tableSort['state'] ? 'desc' : 'asc')} /></th>
                        <th>Tax <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'taxPercentage', this.state.tableSort['taxPercentage'] ? 'desc' : 'asc')} /></th>
                        <th>Margin <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'margin', this.state.tableSort['margin'] ? 'desc' : 'asc')} /></th>
                        <th>Net price <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'netPrice', this.state.tableSort['netPrice'] ? 'desc' : 'asc')} /></th>
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
	
	handleTaxChange(e) {
		this.setState({quantity: e.target.value});
		this.populateTaxData(this.id, e.target.value);
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

              <div class="form-row">
                Show prices in states for product quantity: <input type="number" value={this.state.quantity} min="1" class="form-control" onChange={this.handleTaxChange} />
              </div>
                    {this.renderProductTable()}
            </div>
        );
    }

    async populateTaxData(id, quantity) {
        const response = await fetch('api/products/' + id + "/" + quantity);
        const data = await response.json();
        this.setState({ product: data });
    }
}