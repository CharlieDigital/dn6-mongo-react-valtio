import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './App'
import { OpenAPI } from './services';

OpenAPI.BASE = 'http://localhost:5009';

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
)
