import { gql } from 'apollo-server';

export const typeDefs = gql`
  scalar DateTime

  directive @dateFormat(defaultFormat: String, defaultTimeZone: String) on FIELD_DEFINITION

  interface MutationResponse {
    success: Boolean!
  }

  interface Node {
    id: Int!
  }

  interface Edge {
    cursor: String!
  }

  interface CursorPaginatedResponse {
    pageInfo: PageInfo!
    totalCount: Int
  }

  type Query {
    user(id: Int!): User
    "Get the list of users"
    users(pagination: PaginationInput): UserConnection!
    exercise(id: Int!): Exercise
    exercises(pagination: PaginationInput): ExerciseConnection!
    exerciseCategory(id: Int!): ExerciseCategory
    exerciseCategories(pagination: PaginationInput): ExerciseCategoryConnection!
    muscle(id: Int!): Muscle
    muscles(pagination: PaginationInput): MuscleConnection!
    equipment(id: Int!): Equipment
    allEquipment(pagination: PaginationInput): EquipmentConnection!
    workout(id: Int!): Workout
    workouts(pagination: PaginationInput): WorkoutConnection!
    me: User
  }

  type Mutation {
    registerUser(user: RegisterUser!): RegisterUserResponse!
    login(userCredentials: UserLogin!): UserLoginResponse!
    refreshToken(input: RefreshTokenInput!): RefreshTokenResponse!
    createScheduledWorkout(newWorkout: NewScheduledWorkout!): CreateScheduledWorkoutResponse!
    startScheduledWorkout(id: Int!): StartScheduledWorkoutResponse!
  }

  type RefreshTokenResponse implements MutationResponse {
    success: Boolean!
    token: String!
    refreshToken: String!
  }

  type RegisterUserResponse implements MutationResponse {
    success: Boolean!
    user: User!
  }

  type UserLoginResponse implements MutationResponse {
    success: Boolean!
    token: String!
    refreshToken: String!
    user: User!
  }

  type CreateScheduledWorkoutResponse implements MutationResponse {
    success: Boolean!
    scheduledWorkout: ScheduledWorkout!
  }

  type StartScheduledWorkoutResponse implements MutationResponse {
    success: Boolean!
    scheduledWorkout: ScheduledWorkout!
  }

  type User implements Node {
    id: Int!
    userName: String!
    firstName: String!
    lastName: String!
    email: String!
    created(format: String, timeZone: String): DateTime!
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
    scheduledWorkouts(pagination: PaginationInput): ScheduledWorkoutConnection!
    ownedScheduledWorkouts(pagination: PaginationInput): ScheduledWorkoutConnection!
    favoriteExercises(pagination: PaginationInput): ExerciseConnection!
    sentWorkoutInvitations(filter: WorkoutInvitationFilter, pagination: PaginationInput): WorkoutInvitationConnection!
    receivedWorkoutInvitations(
      filter: WorkoutInvitationFilter
      pagination: PaginationInput
    ): WorkoutInvitationConnection!
  }

  type UserConnection implements CursorPaginatedResponse {
    edges: [UserEdge!]!
    nodes: [User!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type UserEdge implements Edge {
    cursor: String!
    node: User!
  }

  type WorkoutInvitation implements Node {
    id: Int!
    inviter: User!
    invitee: User!
    scheduledWorkout: ScheduledWorkout!
    accepted: Boolean!
    declined: Boolean!
    status: String!
    respondedAtDateTime(format: String, timeZone: String): DateTime
      @dateFormat(defaultFormat: "yyyy-MM-dd'T'HH:mm:ss.SSSxxx", defaultTimeZone: "00:00")
  }

  type WorkoutInvitationConnection implements CursorPaginatedResponse {
    edges: [WorkoutInvitationEdge!]!
    nodes: [WorkoutInvitation!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type WorkoutInvitationEdge implements Edge {
    cursor: String!
    node: WorkoutInvitation!
  }

  type Workout implements Node {
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
    exerciseGroups(pagination: PaginationInput): ExerciseGroupConnection!
  }

  type WorkoutConnection implements CursorPaginatedResponse {
    edges: [WorkoutEdge!]!
    nodes: [Workout!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type WorkoutEdge implements Edge {
    cursor: String!
    node: Workout!
  }

  type ExerciseGroup implements Node {
    id: Int!
    exerciseId: Int!
    exercise: Exercise!
    sets: Int!
    repetitions: Int!
  }

  type ExerciseGroupConnection implements CursorPaginatedResponse {
    edges: [ExerciseGroupEdge!]!
    nodes: [ExerciseGroup!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type ExerciseGroupEdge implements Edge {
    cursor: String!
    node: ExerciseGroup!
  }

  type ScheduledWorkout implements Node {
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
    adHocExercises(pagination: PaginationInput): ExerciseGroupConnection!
    attendees(pagination: PaginationInput): UserConnection!
  }

  type ScheduledWorkoutConnection implements CursorPaginatedResponse {
    edges: [ScheduledWorkoutEdge!]!
    nodes: [ScheduledWorkout!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type ScheduledWorkoutEdge implements Edge {
    cursor: String!
    node: ScheduledWorkout!
  }

  type Exercise implements Node {
    id: Int!
    name: String!
    primaryMuscle: Muscle
    secondaryMuscle: Muscle
    exerciseSteps: [ExerciseStep!]!
    equipment(pagination: PaginationInput): EquipmentConnection!
    exerciseCategorys(pagination: PaginationInput): ExerciseCategoryConnection!
  }

  type ExerciseConnection implements CursorPaginatedResponse {
    edges: [ExerciseEdge!]!
    nodes: [Exercise!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type ExerciseEdge implements Edge {
    cursor: String!
    node: Exercise!
  }

  type Muscle implements Node {
    id: Int!
    name: String!
    description: String
    primaryExercises(pagination: PaginationInput): ExerciseConnection!
    secondaryExercises(pagination: PaginationInput): ExerciseConnection!
  }

  type MuscleConnection implements CursorPaginatedResponse {
    edges: [MuscleEdge!]!
    nodes: [Muscle!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type MuscleEdge implements Edge {
    cursor: String!
    node: Muscle!
  }

  type ExerciseStep {
    exerciseStepNumber: Int!
    description: String!
  }

  type Equipment implements Node {
    id: Int!
    name: String!
    exercises(pagination: PaginationInput): ExerciseConnection!
  }

  type EquipmentConnection implements CursorPaginatedResponse {
    edges: [EquipmentEdge!]!
    nodes: [Equipment!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type EquipmentEdge implements Edge {
    cursor: String!
    node: Equipment!
  }

  type ExerciseCategory implements Node {
    id: Int!
    name: String!
    exercises(pagination: PaginationInput): ExerciseConnection!
  }

  type ExerciseCategoryConnection implements CursorPaginatedResponse {
    edges: [ExerciseCategoryEdge!]!
    nodes: [ExerciseCategory!]!
    pageInfo: PageInfo!
    totalCount: Int
  }

  type ExerciseCategoryEdge implements Edge {
    cursor: String!
    node: ExerciseCategory!
  }

  type PageInfo {
    startCursor: String!
    endCursor: String!
    hasNextPage: Boolean!
    hasPreviousPage: Boolean!
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

  input WorkoutInvitationFilter {
    status: WorkoutInvitationStatus
  }

  input PaginationInput {
    first: Int
    after: String
    last: Int
    before: String
    includeTotal: Boolean
  }

  enum WorkoutInvitationStatus {
    ACCEPTED
    DECLINED
    PENDING
  }
`;
