import { gql } from 'apollo-server';

export const typeDefs = gql`
type Query {
    test: String
    users: [User]!
    user(id: Int!): User
    exercise(id: Int!): Exercise
    exercises: [Exercise]
    muscles: [Muscle]
    muscle(id: Int!): Muscle
    allEquipment: [Equipment]
    equipment(id: Int!): Equipment
    me: User
}

type Mutation {
    registerUser(user: RegisterUser!): RegisterUserPayload!
    login(userCredentials: UserLogin!): UserLoginPayload!
    createScheduledWorkout(newWorkout: NewScheduledWorkout!): CreateScheduledWorkoutPayload!
    startScheduledWorkout(id: Int!): StartScheduledWorkoutPayload!
}

type RegisterUserPayload {
    user: User!
}

type UserLoginPayload {
    token: String!
    user: User!
}

type CreateScheduledWorkoutPayload {
    scheduledWorkout: ScheduledWorkout!
}

type StartScheduledWorkoutPayload {
    scheduledWorkout: ScheduledWorkout!
}

type User {
    id: Int!
    userName: String!
    firstName: String!
    lastName: String!,
    email: String!,
    created: String!
    createdWorkouts: [Workout]
    scheduledWorkouts: [ScheduledWorkout]
}

type Workout {
    id: Int!
    label: String!
    color: String
    createdByUserId: Int!
    createdOnDate: String
    lastModifiedDate: String
    shareable: Boolean
    exerciseGroups: [ExerciseGroup]
}

type ExerciseGroup {
    id: Int!
    exercise: Exercise!
    sets: Int!
    repetitions: Int!
}

type ScheduledWorkout {
    id: Int!
    user: User!
    workout: Workout!
    startedDateTime: String
    completedDateTime: String
    scheduledDateTime: String!
    adHocExercises: [ExerciseGroup]
    extraSchUsrWoAttendees: [User]
}

type Exercise {
    id: Int!
    name: String!
    primaryMuscle: Muscle
    secondaryMuscle: Muscle
    exerciseSteps: [ExerciseStep]
    equipment: [Equipment]
    exerciseCategorys: [ExerciseCategory]
}

type Muscle {
    id: Int!
    name: String!
    exercises: [Exercise]
}

type ExerciseStep {
    exerciseStepNumber: Int!
    description: String!
}

type Equipment {
    id: Int!
    name: String!
    exercises: [Exercise]
}

type ExerciseCategory {
    id: Int!
    name: String!
    exercises: [Exercise]
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

input NewScheduledWorkout {
    workoutId: Int!
    scheduledDateTime: String!
}
`;