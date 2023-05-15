import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import axios from 'axios';

axios.interceptors.response.use(function (response) {
    console.log("ok");
    return response;
  }, function (error) {
    
    console.log("error")
  });


ReactDOM.render(<App />, document.getElementById('root'));