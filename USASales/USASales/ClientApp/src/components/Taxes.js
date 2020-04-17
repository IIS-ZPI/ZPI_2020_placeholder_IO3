import React, { Component } from 'react';
import Select from 'react-select';

const statesOptions = [
    { value: 'Alabama', label: 'Alabama' },
    { value: 'Alaska', label: 'Alaska' },
    { value: 'Arizona', label: 'Arizona' },
    { value: 'Arkansas', label: 'Arkansas' },
    { value: 'California', label: 'California' },
    { value: 'Colorado', label: 'Colorado' },
    { value: 'Connecticut', label: 'Connecticut' },
    { value: 'Delaware', label: 'Delaware' },
    { value: 'District of Columbia', label: 'District of Columbia' },
    { value: 'Florida', label: 'Florida' },
    { value: 'Georgia', label: 'Georgia' },
    { value: 'Guam', label: 'Guam' },
    { value: 'Hawaii', label: 'Hawaii' },
    { value: 'Idaho', label: 'Idaho' },
    { value: 'Illinois', label: 'Illinois' },
    { value: 'Indiana', label: 'Indiana' },
    { value: 'Iowa', label: 'Iowa' },
    { value: 'Kansas', label: 'Kansas' },
    { value: 'Kentucky', label: 'Kentucky' },
    { value: 'Louisiana', label: 'Louisiana' },
    { value: 'Maine', label: 'Maine' },
    { value: 'Maryland', label: 'Maryland' },
    { value: 'Massachusetts', label: 'Massachusetts' },
    { value: 'Michigan', label: 'Michigan' },
    { value: 'Minnesota', label: 'Minnesota' },
    { value: 'Mississippi', label: 'Mississippi' },
    { value: 'Missouri', label: 'Missouri' },
    { value: 'Montana', label: 'Montana' },
    { value: 'Nebraska', label: 'Nebraska' },
    { value: 'Nevada', label: 'Nevada' },
    { value: 'New Hampshire', label: 'New Hampshire' },
    { value: 'New Jersey', label: 'New Jersey' },
    { value: 'New Mexico', label: 'New Mexico' },
    { value: 'New York', label: 'New York' },
    { value: 'North Carolina', label: 'North Carolina' },
    { value: 'North Dakota', label: 'North Dakota' },
    { value: 'Ohio', label: 'Ohio' },
    { value: 'Oklahoma', label: 'Oklahoma' },
    { value: 'Oregon', label: 'Oregon' },
    { value: 'Pennsylvania', label: 'Pennsylvania' },
    { value: 'Puerto Rico', label: 'Puerto Rico' },
    { value: 'Rhode Island', label: 'Rhode Island' },
    { value: 'South Carolina', label: 'South Carolina' },
    { value: 'South Dakota', label: 'South Dakota' },
    { value: 'Tennessee', label: 'Tennessee' },
    { value: 'Texas', label: 'Texas' },
    { value: 'Utah', label: 'Utah' },
    { value: 'Vermont', label: 'Vermont' },
    { value: 'Virginia', label: 'Virginia' },
    { value: 'Washington', label: 'Washington' },
    { value: 'West Virginia', label: 'West Virginia' },
    { value: 'Wisconsin', label: 'Wisconsin' },
    { value: 'Wyoming', label: 'Wyoming' }
];

export class Taxes extends Component {
    constructor(props) {
        super(props);

        this.state = {
            taxes: [],
            selectedState: 'Alabama'
        }
    }

    componentDidMount() {
        this.populateTaxData('Alabama')
    }

    static renderTaxesTable(taxes) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Category</th>
                        <th>Tax</th>
                        <th>Threshold</th>
                    </tr>
                </thead>
                <tbody>
                    {taxes.map(tax =>
                        <tr key={tax.id}>
                            <td>{tax.category}</td>
                            <td>{Number(tax.taxPercentage).toFixed(2)}%</td>
                            <td>${Number(tax.thresholdUsd).toFixed(2)}</td>
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
                <h1>Taxes</h1>
                <Select
                    value={selectedState}
                    onChange={this.handleStateSelectionChange}
                    options={statesOptions}
                    placeholder={this.state.selectedState}
                />
                {Taxes.renderTaxesTable(this.state.taxes)}
            </div>
        );
    }

    async populateTaxData(state) {
        const response = await fetch('api/taxes/' + state);
        const data = await response.json();
        this.setState({ taxes: data });
    }
}