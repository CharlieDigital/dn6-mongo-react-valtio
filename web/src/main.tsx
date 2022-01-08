import React from 'react'
import ReactDOM from 'react-dom'
import './index.css'
import App from './App'
import { OpenAPI } from './services';
import { BrowserRouter } from 'react-router-dom';
import Amplify from 'aws-amplify';

// The base URL for the backend API
OpenAPI.BASE = import.meta.env.VITE_API_ENDPOINT as string;

// Configure AWS Amplify for Cognito
Amplify.configure(
{
    aws_cognito_identity_pool_id: import.meta.env.VITE_AWS_IDENTITY_POOL_ID,
    aws_cognito_region: import.meta.env.VITE_AWS_REGION,
    aws_user_pools_id: import.meta.env.VITE_AWS_USER_POOL_ID,
    aws_user_pools_web_client_id: import.meta.env.VITE_AWS_APP_CLIENT_ID,
    oauth: {
        domain: import.meta.env.VITE_AWS_OAUTH_DOMAIN,
        scope: [
            "phone",
            "email",
            "openid",
            "profile",
            "aws.cognito.signin.user.admin"
        ],
        redirectSignIn: "http://localhost:3000",
        redirectSignOut: "http://localhost:3000",
        responseType: "code"
    }
});

ReactDOM.render(
    <React.StrictMode>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </React.StrictMode>,
    document.getElementById('root')
)
