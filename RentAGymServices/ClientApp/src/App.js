import React, { Component } from 'react';
// import { Route } from 'react-router';
import {Switch, Route, Redirect} from 'react-router-dom'
import { Layout } from './components/Layout';
// import Homepage from './pages/homepage/Homepage';
// import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import {SignInAndSignUp} from './pages/sign-in-and-sign-up/SignInAndSignUp.js'

import Homepage from './pages/homepage/Homepage';
import SearchResults from './pages/searchResults/SearchResults';


import {auth, createUserProfileDocument} from './firebase/firebase.utils'

import './custom.css'

export default class App extends Component {
  static displayName = App.name;
  unsubscribeFromAuth = null;

  constructor() {
    super();

    this.state = {
      currentUser: null
    }
  }

  componentDidMount(){
    this.unsubscribeFromAuth = auth.onAuthStateChanged(user => {
      this.setState({currentUser : user});
    });
  }

  componentWillUnmount() {
    this.unsubscribeFromAuth();
  }

  render () {
    return (
      <Layout>
        <Switch>
            <Route exact path='/' component={Homepage} />
            <Route path='/counter' component={Counter} /> 
            <Route path='/results' component={SearchResults} />
            <Route exact path='/signin' 
                   render={() => this.props.currentUser ? (
                     <Redirect to='/' />
                     ) : (
                       <SignInAndSignUp/> ) }/>
        </Switch>

{/* 
        <Route exact path='/' component={Homepage} />
        <Route path='/counter' component={Counter} /> */}
      </Layout>
    );
  }
}
