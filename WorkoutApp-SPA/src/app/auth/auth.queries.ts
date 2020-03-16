import gql from 'graphql-tag';

export const login = gql`
    mutation Login($userCredentials: UserLogin!) {
        login(userCredentials: $userCredentials) {
            token
            refreshToken
            user {
                id
                userName
                firstName
                lastName
                email
                created
            }
        }
    }
`;

export const registerUser = gql`
    mutation RegisterUser($user: RegisterUser!) {
        registerUser(user: $user) {
            user {
                id
                userName
                firstName
                lastName
                email
                created
            }
        }
    }
`;

export const getUser = gql`
    query GetUser($userId: Int!) {
        user(id: $userId) {
            firstName
            lastName
            userName
            email
        }
    }
`;

export const refreshToken = gql`
    mutation RefreshToken($refreshTokenInput: RefreshTokenInput!) {
        refreshToken(input: $refreshTokenInput) {
            refreshToken
            token
        }
    }
`;
