import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import KeyCloakService from './security/KeycloakService.tsx';

const root = ReactDOM.createRoot(document.getElementById('root')!);

const renderApp = () =>
  root.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>,
  )

KeyCloakService.CallLogin(renderApp);