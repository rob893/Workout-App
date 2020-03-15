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
        workouts: [Workout]!
        workout(id: Int!): Workout!
        me: User
    }

    type Mutation {
        registerUser(user: RegisterUser!): RegisterUserResponse!
        login(userCredentials: UserLogin!): UserLoginResponse!
        createScheduledWorkout(newWorkout: NewScheduledWorkout!): CreateScheduledWorkoutResponse!
        startScheduledWorkout(id: Int!): StartScheduledWorkoutResponse!
    }

    type RegisterUserResponse {
        user: User!
    }

    type UserLoginResponse {
        token: String!
        refreshToken: String!
        user: User!
    }

    type CreateScheduledWorkoutResponse {
        scheduledWorkout: ScheduledWorkout!
    }

    type StartScheduledWorkoutResponse {
        scheduledWorkout: ScheduledWorkout!
    }

    type User {
        id: Int!
        userName: String!
        firstName: String!
        lastName: String!
        email: String!
        created: String!
        createdWorkouts: [Workout]
        scheduledWorkouts: [ScheduledWorkout]
    }

    type Workout {
        id: Int!
        label: String!
        description: String
        color: String
        createdByUserId: Int!
        createdByUser: User
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
        description: String
        primaryExercises: [Exercise]
        secondaryExercises: [Exercise]
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
        source: String!
    }

    input NewScheduledWorkout {
        workoutId: Int!
        scheduledDateTime: String!
    }
`;
