export interface User {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    created: string;
}

export interface UserToRegister {
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    email: string;
}

export interface UserLogin {
    username: string;
    password: string;
    source: string;
}

export interface UserLoginResponse {
    token: string;
    user: User;
}

export interface JwtClaims {
    nameid?: string;
    unique_name?: string;
    role?: string[];
    nbf?: number;
    exp?: number;
    iat?: number;
    iss?: string;
    aud?: string;
}
