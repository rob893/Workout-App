import { gql } from 'apollo-server';

export const typeDefs = gql`
  scalar DateTime

  directive @dateFormat(defaultFormat: String, defaultTimeZone: String) on FIELD_DEFINITION

  type Query {
    test: String!
    user(id: Int!): User
    "Get the list of users"
    users: [User]!
    exercise(id: Int!): Exercise
    exercises: [Exercise!]!
    exerciseCategory(id: Int!): ExerciseCategory
    exerciseCategories: [ExerciseCategory!]!
    muscle(id: Int!): Muscle
    muscles: [Muscle!]!
    equipment(id: Int!): Equipment
    allEquipment: [Equipment!]!
    workout(id: Int!): Workout
    workouts: [Workout!]!
    me: User
  }

  type Mutation {
    registerUser(user: RegisterUser!): RegisterUserResponse!
    login(userCredentials: UserLogin!): UserLoginResponse!
    refreshToken(input: RefreshTokenInput!): RefreshTokenResponse!
    createScheduledWorkout(newWorkout: NewScheduledWorkout!): CreateScheduledWorkoutResponse!
    startScheduledWorkout(id: Int!): StartScheduledWorkoutResponse!
  }

  type RefreshTokenResponse {
    token: String!
    refreshToken: String!
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
    created(format: String, timeZone: String): DateTime!
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    roles: [UserRole]
    createdWorkouts: [Workout]
    scheduledWorkouts: [ScheduledWorkout]
    ownedScheduledWorkouts: [ScheduledWorkout]
    favoriteExercises: [Exercise]
  }

  type UserRole {
    name: String!
  }

  type WorkoutInvitation {
    inviter: User!
    invitee: User!
    scheduledWorkout: ScheduledWorkout!
    accepted: Boolean!
    declined: Boolean!
    status: String!
    respondedAtDateTime(format: String, timeZone: String): DateTime
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
  }

  type Workout {
    id: Int!
    label: String!
    description: String
    createdByUserId: Int!
    createdByUser: User!
    createdOnDate(format: String, timeZone: String): DateTime!
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    lastModifiedDate(format: String, timeZone: String): DateTime!
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    shareable: Boolean!
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
    scheduledByUser: User!
    workout: Workout!
    startedDateTime(format: String, timeZone: String): DateTime
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    completedDateTime(format: String, timeZone: String): DateTime
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    scheduledDateTime(format: String, timeZone: String): DateTime!
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    customWorkout: String
    adHocExercises: [ExerciseGroup]
    attendees: [User]
  }

  type Exercise {
    id: Int!
    name: String!
    primaryMuscle: Muscle
    secondaryMuscle: Muscle
    exerciseSteps: [ExerciseStep!]!
    equipment: [Equipment!]!
    exerciseCategorys: [ExerciseCategory!]!
  }

  type Muscle {
    id: Int!
    name: String!
    description: String
    primaryExercises: [Exercise!]!
    secondaryExercises: [Exercise]
  }

  type ExerciseStep {
    exerciseStepNumber: Int!
    description: String!
  }

  type Equipment {
    id: Int!
    name: String!
    exercises: [Exercise]!
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
    scheduledDateTime: DateTime!
  }

  input RefreshTokenInput {
    token: String!
    refreshToken: String!
    source: String!
  }
`;
