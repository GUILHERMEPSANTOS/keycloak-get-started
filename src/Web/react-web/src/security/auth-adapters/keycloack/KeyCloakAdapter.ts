import Keycloak, { KeycloakOnLoad } from "keycloak-js";
import { IKeyCloakAdapter } from "./IKeyCloakAdapter";

let initOptions = {
    url: 'http://localhost:8080/',
    realm: 'react-app-realm',
    clientId: 'react-app-client',
    onLoad: 'login-required', // check-sso | login-required
    KeycloakResponseType: 'code',
}

export class KeyCloakAdapter
    implements IKeyCloakAdapter {

    private static instance: IKeyCloakAdapter;
    private keycloakInstance: Keycloak;

    constructor() {
        this.keycloakInstance = new Keycloak(initOptions);
    }

    public static create(): IKeyCloakAdapter {
        if (!this.instance) {
            return this.instance = new KeyCloakAdapter();
        }
        return this.instance;
    }

    public async init() {
        await this.keycloakInstance
            .init({
                onLoad: initOptions.onLoad as KeycloakOnLoad,
                pkceMethod: 'S256',
            })
    }

    public validateIfAuthenticated(): boolean {
        return this.keycloakInstance?.authenticated || false;
    }
}