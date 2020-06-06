import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Taxes } from './components/Taxes';
import { AboutUs } from './components/AboutUs';
import { ContactUs } from './components/ContactUs';
import { Products } from './components/Products';
import { Product } from './components/Product';
import { NewProduct } from './components/NewProduct';
import { EditProduct } from './components/EditProduct';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/taxes' component={Taxes} />
        <Route path='/about' component={AboutUs} />
        <Route path='/contact' component={ContactUs} />
        <Route exact path='/products' component={Products} />
        <Route path='/products/add' component={NewProduct} />
        <Route path='/products/edit/:id' component={EditProduct} />
        <Route path='/products/details/:id' component={Product} />
      </Layout>
    );
  }
}
