import { gql } from 'apollo-server';

export const typeDefs = gql`
type Query {
    test: String
    users: [User]!
    user(id: ID!): User
}

type Mutation {
    registerUser(user: RegisterUser): User
    login(userCredentials: UserLogin): UserLoginResponse
}

type User {
    id: ID!
    userName: String!
    firstName: String!
    lastName: String!,
    email: String!,
    created: String!
}

type UserLoginResponse {
    token: String!
    user: User!
}

input RegisterUser {
    username: String!
    password: String!
    firstName: String!
    lastName: String!
    email: String!
}

input UserLogin {
    username: String!
    password: String!
}
`;