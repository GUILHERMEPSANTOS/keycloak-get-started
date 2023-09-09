import Keycloak, { KeycloakOnLoad } from "keycloak-js";

let initOptions = {
    url: 'http://localhost:8080/',
    realm: 'react-app-realm',
    clientId: 'react-app-client',
    onLoad: 'login-required', // check-sso | login-required
    KeycloakResponseType: 'code',
}


const keycloakInstance = new Keycloak(initOptions);

const Login = (onAuthenticatedCallback: Function) => {
    keycloakInstance
        .init({
            onLoad: initOptions.onLoad as KeycloakOnLoad,
            pkceMethod: 'S256',
        })
        .then((authenticated) => {
            authenticated ? onAuthenticatedCallback() : alert("non authenticated");
        })
        .catch((error) => {
            console.dir(error);
            console.log(`keycloak init exception: ${error}`);
        });
};

const KeyCloakService = {
    CallLogin: Login,
};

export default KeyCloakService;