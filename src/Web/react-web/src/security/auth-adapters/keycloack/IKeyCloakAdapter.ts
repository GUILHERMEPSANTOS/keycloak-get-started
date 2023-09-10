export interface IKeyCloakAdapter {
    validateIfAuthenticated(): boolean;
    init(): Promise<void>;
}