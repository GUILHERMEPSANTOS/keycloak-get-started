export interface IAuthService {
    init(onAuthenticatedCallback: Function): Promise<void>
    validateIfAuthenticated(): boolean;
}