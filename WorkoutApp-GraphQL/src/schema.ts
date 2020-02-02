import { gql } from 'apollo-server';

export const typeDefs = gql`
type User {
    id: ID!
    firstName: String
    lastName: String,
    age: Int
}
type Query {
    test: String
    users: [User]!
    user(id: ID!): User
}
input RegisterUser {
    firstName: String
    lastName: String
    age: Int
}
type Mutation {
    registerUser(user: RegisterUser): User
}
`;