import React, { Component } from 'react';
import { Alert } from 'reactstrap';
import { faSort } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    Link,
} from 'react-router-dom';
import './Products.css';
import './../custom.css';

export class Products extends Component {
    constructor(props) {
        super(props);

        this.removeProduct = this.removeProduct.bind(this)
        this.onSort = this.onSort.bind(this)

        this.state = {
            products: [],
            error: "",
            tableSort: [
                {'name': 0},
                {'category': 0},
                {'wholesalePrice': 0},
                {'grossPrice': 0},
            ]
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

    onSort(event, sortKey, direction){
        const data = this.state.products;
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

    renderProductsTable() {
        var products = this.state.products;
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>
                            Name 
                            <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'name', this.state.tableSort['name'] ? 'desc' : 'asc')} />
                        </th>
                        <th>
                            Category 
                            <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'category', this.state.tableSort['category'] ? 'desc' : 'asc')} />
                        </th>
                        <th>
                            Wholesale price 
                            <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'wholesalePrice', this.state.tableSort['wholesalePrice'] ? 'desc' : 'asc')} />
                        </th>
                        <th>
                            Gross price 
                            <FontAwesomeIcon icon={faSort} className="clickable" onClick={e => this.onSort(e, 'grossPrice', this.state.tableSort['grossPrice'] ? 'desc' : 'asc')} />
                        </th>
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