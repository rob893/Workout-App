import { gql } from 'apollo-server';

export const typeDefs = gql`
type Query {
    test: String
    users: [User]!
    user(id: ID!): User
    exercise(id: ID!): Exercise
    exercises: [Exercise]
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

type Exercise {
    id: ID!
    name: String!
    primaryMuscle: Muscle
    secondaryMuscle: Muscle
    exerciseSteps: [ExerciseStep]
    equipment: Equipment
    exerciseCategorys: [ExerciseCategory]
}

type Muscle {
    id: ID!
    name: String!
}

type ExerciseStep {
    exerciseStepNumber: Int!
    description: String!
}

type Equipment {
    id: ID!
    name: String!
}

type ExerciseCategory {
    id: ID!
    name: String!
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