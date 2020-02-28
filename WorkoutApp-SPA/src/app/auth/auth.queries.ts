import gql from 'graphql-tag';


export const login = gql`
    mutation Login($userCredentials: UserLogin!) {
        login(userCredentials: $userCredentials) {
            token
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
