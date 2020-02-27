import gql from 'graphql-tag';

export class GraphQLQueries {
    public static login = gql`
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
        }`;
}