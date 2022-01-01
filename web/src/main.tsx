import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './App'
import { OpenAPI } from './services';

OpenAPI.BASE = import.meta.env.VITE_API_ENDPOINT as string;

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
)
