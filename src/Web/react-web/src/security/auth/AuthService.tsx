import { IAuthService } from './IAuthService';
import { IKeyCloakAdapter } from "../auth-adapters/keycloack/IKeyCloakAdapter";
import { KeyCloakAdapter } from "../auth-adapters/keycloack/KeyCloakAdapter";


class AuthService implements IAuthService {
    private keycloakAdapter: IKeyCloakAdapter;
    private static instance: AuthService;

    constructor() {
        this.keycloakAdapter = KeyCloakAdapter.create();
    }

    public static create(): AuthService {
        if (!AuthService.instance) {
            AuthService.instance = new AuthService()
        }

        return AuthService.instance;
    }

    public async init(onAuthenticatedCallback: Function) {
        await this.keycloakAdapter.init();

        const isAuthenticated = this.validateIfAuthenticated();

        if (isAuthenticated) {
            onAuthenticatedCallback()
        }
        else {
            alert("non authenticated")
        }
    }

    public validateIfAuthenticated(): boolean {
        return this.keycloakAdapter.validateIfAuthenticated();
    }
}

export default AuthService;