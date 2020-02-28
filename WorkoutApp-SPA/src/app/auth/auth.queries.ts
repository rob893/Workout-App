import gql from 'graphql-tag';


const login = gql`
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

export {
    login
};