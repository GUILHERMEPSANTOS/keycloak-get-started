import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'

import AuthService from './security/auth/AuthService.tsx';

const root = ReactDOM.createRoot(document.getElementById('root')!);

const renderApp = () =>
  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>,
  )

AuthService
  .create()
  .init(renderApp);