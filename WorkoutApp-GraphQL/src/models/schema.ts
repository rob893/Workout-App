import { GraphQLResolveInfo, GraphQLScalarType, GraphQLScalarTypeConfig } from 'graphql';
import { User } from './workout-api/user';
import { Exercise, Muscle, Equipment, ExerciseStep, ExerciseCategory } from './workout-api/exercise';
import { Workout, WorkoutInvitation, ScheduledWorkout } from './workout-api/workout';
export type Maybe<T> = T | null;
export type Exact<T extends { [key: string]: any }> = { [K in keyof T]: T[K] };
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
export type RequireFields<T, K extends keyof T> = { [X in Exclude<keyof T, K>]?: T[X] } &
  { [P in K]-?: NonNullable<T[P]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  DateTime: any;
};

export type SchemaMutationResponse = {
  success: Scalars['Boolean'];
};

export type SchemaNode = {
  id: Scalars['Int'];
};

export type SchemaEdge = {
  cursor: Scalars['String'];
};

export type SchemaCursorPaginatedResponse = {
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaQuery = {
  __typename?: 'Query';
  user?: Maybe<SchemaUser>;
  /** Get the list of users */
  users: SchemaUserConnection;
  exercise?: Maybe<SchemaExercise>;
  exercises: SchemaExerciseConnection;
  exerciseCategory?: Maybe<SchemaExerciseCategory>;
  exerciseCategories: SchemaExerciseCategoryConnection;
  muscle?: Maybe<SchemaMuscle>;
  muscles: SchemaMuscleConnection;
  equipment?: Maybe<SchemaEquipment>;
  allEquipment: SchemaEquipmentConnection;
  workout?: Maybe<SchemaWorkout>;
  workouts: SchemaWorkoutConnection;
  me?: Maybe<SchemaUser>;
};

export type SchemaQueryUserArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryUsersArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaQueryExerciseArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaQueryExerciseCategoryArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryExerciseCategoriesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaQueryMuscleArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryMusclesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaQueryEquipmentArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryAllEquipmentArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaQueryWorkoutArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryWorkoutsArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaMutation = {
  __typename?: 'Mutation';
  registerUser: SchemaRegisterUserResponse;
  login: SchemaUserLoginResponse;
  refreshToken: SchemaRefreshTokenResponse;
  createScheduledWorkout: SchemaCreateScheduledWorkoutResponse;
  startScheduledWorkout: SchemaStartScheduledWorkoutResponse;
};

export type SchemaMutationRegisterUserArgs = {
  user: SchemaRegisterUser;
};

export type SchemaMutationLoginArgs = {
  userCredentials: SchemaUserLogin;
};

export type SchemaMutationRefreshTokenArgs = {
  input: SchemaRefreshTokenInput;
};

export type SchemaMutationCreateScheduledWorkoutArgs = {
  newWorkout: SchemaNewScheduledWorkout;
};

export type SchemaMutationStartScheduledWorkoutArgs = {
  id: Scalars['Int'];
};

export type SchemaRefreshTokenResponse = SchemaMutationResponse & {
  __typename?: 'RefreshTokenResponse';
  success: Scalars['Boolean'];
  token: Scalars['String'];
  refreshToken: Scalars['String'];
};

export type SchemaRegisterUserResponse = SchemaMutationResponse & {
  __typename?: 'RegisterUserResponse';
  success: Scalars['Boolean'];
  user: SchemaUser;
};

export type SchemaUserLoginResponse = SchemaMutationResponse & {
  __typename?: 'UserLoginResponse';
  success: Scalars['Boolean'];
  token: Scalars['String'];
  refreshToken: Scalars['String'];
  user: SchemaUser;
};

export type SchemaCreateScheduledWorkoutResponse = SchemaMutationResponse & {
  __typename?: 'CreateScheduledWorkoutResponse';
  success: Scalars['Boolean'];
  scheduledWorkout: SchemaScheduledWorkout;
};

export type SchemaStartScheduledWorkoutResponse = SchemaMutationResponse & {
  __typename?: 'StartScheduledWorkoutResponse';
  success: Scalars['Boolean'];
  scheduledWorkout: SchemaScheduledWorkout;
};

export type SchemaUser = SchemaNode & {
  __typename?: 'User';
  id: Scalars['Int'];
  userName: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  email: Scalars['String'];
  created: Scalars['DateTime'];
  scheduledWorkouts: SchemaScheduledWorkoutConnection;
  ownedScheduledWorkouts: SchemaScheduledWorkoutConnection;
  favoriteExercises: SchemaExerciseConnection;
  sentWorkoutInvitations: SchemaWorkoutInvitationConnection;
  receivedWorkoutInvitations: SchemaWorkoutInvitationConnection;
};

export type SchemaUserCreatedArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaUserScheduledWorkoutsArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaUserOwnedScheduledWorkoutsArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaUserFavoriteExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaUserSentWorkoutInvitationsArgs = {
  filter?: Maybe<SchemaWorkoutInvitationFilter>;
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaUserReceivedWorkoutInvitationsArgs = {
  filter?: Maybe<SchemaWorkoutInvitationFilter>;
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaUserConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'UserConnection';
  edges: Array<SchemaUserEdge>;
  nodes: Array<SchemaUser>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaUserEdge = SchemaEdge & {
  __typename?: 'UserEdge';
  cursor: Scalars['String'];
  node: SchemaUser;
};

export type SchemaWorkoutInvitation = SchemaNode & {
  __typename?: 'WorkoutInvitation';
  id: Scalars['Int'];
  inviter: SchemaUser;
  invitee: SchemaUser;
  scheduledWorkout: SchemaScheduledWorkout;
  accepted: Scalars['Boolean'];
  declined: Scalars['Boolean'];
  status: Scalars['String'];
  respondedAtDateTime?: Maybe<Scalars['DateTime']>;
};

export type SchemaWorkoutInvitationRespondedAtDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaWorkoutInvitationConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'WorkoutInvitationConnection';
  edges: Array<SchemaWorkoutInvitationEdge>;
  nodes: Array<SchemaWorkoutInvitation>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaWorkoutInvitationEdge = SchemaEdge & {
  __typename?: 'WorkoutInvitationEdge';
  cursor: Scalars['String'];
  node: SchemaWorkoutInvitation;
};

export type SchemaWorkout = SchemaNode & {
  __typename?: 'Workout';
  id: Scalars['Int'];
  label: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdByUserId: Scalars['Int'];
  createdByUser: SchemaUser;
  createdOnDate: Scalars['DateTime'];
  lastModifiedDate: Scalars['DateTime'];
  shareable: Scalars['Boolean'];
  exerciseGroups: SchemaExerciseGroupConnection;
};

export type SchemaWorkoutCreatedOnDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaWorkoutLastModifiedDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaWorkoutExerciseGroupsArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaWorkoutConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'WorkoutConnection';
  edges: Array<SchemaWorkoutEdge>;
  nodes: Array<SchemaWorkout>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaWorkoutEdge = SchemaEdge & {
  __typename?: 'WorkoutEdge';
  cursor: Scalars['String'];
  node: SchemaWorkout;
};

export type SchemaExerciseGroup = SchemaNode & {
  __typename?: 'ExerciseGroup';
  id: Scalars['Int'];
  exerciseId: Scalars['Int'];
  exercise: SchemaExercise;
  sets: Scalars['Int'];
  repetitions: Scalars['Int'];
};

export type SchemaExerciseGroupConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'ExerciseGroupConnection';
  edges: Array<SchemaExerciseGroupEdge>;
  nodes: Array<SchemaExerciseGroup>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaExerciseGroupEdge = SchemaEdge & {
  __typename?: 'ExerciseGroupEdge';
  cursor: Scalars['String'];
  node: SchemaExerciseGroup;
};

export type SchemaScheduledWorkout = SchemaNode & {
  __typename?: 'ScheduledWorkout';
  id: Scalars['Int'];
  scheduledByUser: SchemaUser;
  workout: SchemaWorkout;
  startedDateTime?: Maybe<Scalars['DateTime']>;
  completedDateTime?: Maybe<Scalars['DateTime']>;
  scheduledDateTime: Scalars['DateTime'];
  customWorkout?: Maybe<Scalars['String']>;
  adHocExercises: SchemaExerciseGroupConnection;
  attendees: SchemaUserConnection;
};

export type SchemaScheduledWorkoutStartedDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaScheduledWorkoutCompletedDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaScheduledWorkoutScheduledDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaScheduledWorkoutAdHocExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaScheduledWorkoutAttendeesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaScheduledWorkoutConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'ScheduledWorkoutConnection';
  edges: Array<SchemaScheduledWorkoutEdge>;
  nodes: Array<SchemaScheduledWorkout>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaScheduledWorkoutEdge = SchemaEdge & {
  __typename?: 'ScheduledWorkoutEdge';
  cursor: Scalars['String'];
  node: SchemaScheduledWorkout;
};

export type SchemaExercise = SchemaNode & {
  __typename?: 'Exercise';
  id: Scalars['Int'];
  name: Scalars['String'];
  primaryMuscle?: Maybe<SchemaMuscle>;
  secondaryMuscle?: Maybe<SchemaMuscle>;
  exerciseSteps: Array<SchemaExerciseStep>;
  equipment: SchemaEquipmentConnection;
  exerciseCategorys: SchemaExerciseCategoryConnection;
};

export type SchemaExerciseEquipmentArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaExerciseExerciseCategorysArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaExerciseConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'ExerciseConnection';
  edges: Array<SchemaExerciseEdge>;
  nodes: Array<SchemaExercise>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaExerciseEdge = SchemaEdge & {
  __typename?: 'ExerciseEdge';
  cursor: Scalars['String'];
  node: SchemaExercise;
};

export type SchemaMuscle = SchemaNode & {
  __typename?: 'Muscle';
  id: Scalars['Int'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  primaryExercises: SchemaExerciseConnection;
  secondaryExercises: SchemaExerciseConnection;
};

export type SchemaMusclePrimaryExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaMuscleSecondaryExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaMuscleConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'MuscleConnection';
  edges: Array<SchemaMuscleEdge>;
  nodes: Array<SchemaMuscle>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaMuscleEdge = SchemaEdge & {
  __typename?: 'MuscleEdge';
  cursor: Scalars['String'];
  node: SchemaMuscle;
};

export type SchemaExerciseStep = {
  __typename?: 'ExerciseStep';
  exerciseStepNumber: Scalars['Int'];
  description: Scalars['String'];
};

export type SchemaEquipment = SchemaNode & {
  __typename?: 'Equipment';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises: SchemaExerciseConnection;
};

export type SchemaEquipmentExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaEquipmentConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'EquipmentConnection';
  edges: Array<SchemaEquipmentEdge>;
  nodes: Array<SchemaEquipment>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaEquipmentEdge = SchemaEdge & {
  __typename?: 'EquipmentEdge';
  cursor: Scalars['String'];
  node: SchemaEquipment;
};

export type SchemaExerciseCategory = SchemaNode & {
  __typename?: 'ExerciseCategory';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises: SchemaExerciseConnection;
};

export type SchemaExerciseCategoryExercisesArgs = {
  pagination?: Maybe<SchemaPaginationInput>;
};

export type SchemaExerciseCategoryConnection = SchemaCursorPaginatedResponse & {
  __typename?: 'ExerciseCategoryConnection';
  edges: Array<SchemaExerciseCategoryEdge>;
  nodes: Array<SchemaExerciseCategory>;
  pageInfo: SchemaPageInfo;
  totalCount?: Maybe<Scalars['Int']>;
};

export type SchemaExerciseCategoryEdge = SchemaEdge & {
  __typename?: 'ExerciseCategoryEdge';
  cursor: Scalars['String'];
  node: SchemaExerciseCategory;
};

export type SchemaPageInfo = {
  __typename?: 'PageInfo';
  startCursor: Scalars['String'];
  endCursor: Scalars['String'];
  hasNextPage: Scalars['Boolean'];
  hasPreviousPage: Scalars['Boolean'];
};

export type SchemaRegisterUser = {
  username: Scalars['String'];
  password: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  email: Scalars['String'];
};

export type SchemaUserLogin = {
  username: Scalars['String'];
  password: Scalars['String'];
  source: Scalars['String'];
};

export type SchemaNewScheduledWorkout = {
  workoutId: Scalars['Int'];
  scheduledDateTime: Scalars['DateTime'];
};

export type SchemaRefreshTokenInput = {
  token: Scalars['String'];
  refreshToken: Scalars['String'];
  source: Scalars['String'];
};

export type SchemaWorkoutInvitationFilter = {
  status?: Maybe<SchemaWorkoutInvitationStatus>;
};

export type SchemaPaginationInput = {
  first?: Maybe<Scalars['Int']>;
  after?: Maybe<Scalars['String']>;
  last?: Maybe<Scalars['Int']>;
  before?: Maybe<Scalars['String']>;
  includeTotal?: Maybe<Scalars['Boolean']>;
};

export enum SchemaWorkoutInvitationStatus {
  Accepted = 'ACCEPTED',
  Declined = 'DECLINED',
  Pending = 'PENDING'
}

export type WithIndex<TObject> = TObject & Record<string, any>;
export type ResolversObject<TObject> = WithIndex<TObject>;

export type ResolverTypeWrapper<T> = Promise<T> | T;

export type LegacyStitchingResolver<TResult, TParent, TContext, TArgs> = {
  fragment: string;
  resolve: ResolverFn<TResult, TParent, TContext, TArgs>;
};

export type NewStitchingResolver<TResult, TParent, TContext, TArgs> = {
  selectionSet: string;
  resolve: ResolverFn<TResult, TParent, TContext, TArgs>;
};
export type StitchingResolver<TResult, TParent, TContext, TArgs> =
  | LegacyStitchingResolver<TResult, TParent, TContext, TArgs>
  | NewStitchingResolver<TResult, TParent, TContext, TArgs>;
export type Resolver<TResult, TParent = {}, TContext = {}, TArgs = {}> =
  | ResolverFn<TResult, TParent, TContext, TArgs>
  | StitchingResolver<TResult, TParent, TContext, TArgs>;

export type ResolverFn<TResult, TParent, TContext, TArgs> = (
  parent: TParent,
  args: TArgs,
  context: TContext,
  info: GraphQLResolveInfo
) => Promise<TResult> | TResult;

export type SubscriptionSubscribeFn<TResult, TParent, TContext, TArgs> = (
  parent: TParent,
  args: TArgs,
  context: TContext,
  info: GraphQLResolveInfo
) => AsyncIterator<TResult> | Promise<AsyncIterator<TResult>>;

export type SubscriptionResolveFn<TResult, TParent, TContext, TArgs> = (
  parent: TParent,
  args: TArgs,
  context: TContext,
  info: GraphQLResolveInfo
) => TResult | Promise<TResult>;

export interface SubscriptionSubscriberObject<TResult, TKey extends string, TParent, TContext, TArgs> {
  subscribe: SubscriptionSubscribeFn<{ [key in TKey]: TResult }, TParent, TContext, TArgs>;
  resolve?: SubscriptionResolveFn<TResult, { [key in TKey]: TResult }, TContext, TArgs>;
}

export interface SubscriptionResolverObject<TResult, TParent, TContext, TArgs> {
  subscribe: SubscriptionSubscribeFn<any, TParent, TContext, TArgs>;
  resolve: SubscriptionResolveFn<TResult, any, TContext, TArgs>;
}

export type SubscriptionObject<TResult, TKey extends string, TParent, TContext, TArgs> =
  | SubscriptionSubscriberObject<TResult, TKey, TParent, TContext, TArgs>
  | SubscriptionResolverObject<TResult, TParent, TContext, TArgs>;

export type SubscriptionResolver<TResult, TKey extends string, TParent = {}, TContext = {}, TArgs = {}> =
  | ((...args: any[]) => SubscriptionObject<TResult, TKey, TParent, TContext, TArgs>)
  | SubscriptionObject<TResult, TKey, TParent, TContext, TArgs>;

export type TypeResolveFn<TTypes, TParent = {}, TContext = {}> = (
  parent: TParent,
  context: TContext,
  info: GraphQLResolveInfo
) => Maybe<TTypes> | Promise<Maybe<TTypes>>;

export type IsTypeOfResolverFn<T = {}> = (obj: T, info: GraphQLResolveInfo) => boolean | Promise<boolean>;

export type NextResolverFn<T> = () => Promise<T>;

export type DirectiveResolverFn<TResult = {}, TParent = {}, TContext = {}, TArgs = {}> = (
  next: NextResolverFn<TResult>,
  parent: TParent,
  args: TArgs,
  context: TContext,
  info: GraphQLResolveInfo
) => TResult | Promise<TResult>;

/** Mapping between all available schema types and the resolvers types */
export type SchemaResolversTypes = ResolversObject<{
  DateTime: ResolverTypeWrapper<Scalars['DateTime']>;
  MutationResponse:
    | SchemaResolversTypes['RefreshTokenResponse']
    | SchemaResolversTypes['RegisterUserResponse']
    | SchemaResolversTypes['UserLoginResponse']
    | SchemaResolversTypes['CreateScheduledWorkoutResponse']
    | SchemaResolversTypes['StartScheduledWorkoutResponse'];
  Boolean: ResolverTypeWrapper<Scalars['Boolean']>;
  Node:
    | SchemaResolversTypes['User']
    | SchemaResolversTypes['WorkoutInvitation']
    | SchemaResolversTypes['Workout']
    | SchemaResolversTypes['ExerciseGroup']
    | SchemaResolversTypes['ScheduledWorkout']
    | SchemaResolversTypes['Exercise']
    | SchemaResolversTypes['Muscle']
    | SchemaResolversTypes['Equipment']
    | SchemaResolversTypes['ExerciseCategory'];
  Int: ResolverTypeWrapper<Scalars['Int']>;
  Edge:
    | SchemaResolversTypes['UserEdge']
    | SchemaResolversTypes['WorkoutInvitationEdge']
    | SchemaResolversTypes['WorkoutEdge']
    | SchemaResolversTypes['ExerciseGroupEdge']
    | SchemaResolversTypes['ScheduledWorkoutEdge']
    | SchemaResolversTypes['ExerciseEdge']
    | SchemaResolversTypes['MuscleEdge']
    | SchemaResolversTypes['EquipmentEdge']
    | SchemaResolversTypes['ExerciseCategoryEdge'];
  String: ResolverTypeWrapper<Scalars['String']>;
  CursorPaginatedResponse:
    | SchemaResolversTypes['UserConnection']
    | SchemaResolversTypes['WorkoutInvitationConnection']
    | SchemaResolversTypes['WorkoutConnection']
    | SchemaResolversTypes['ExerciseGroupConnection']
    | SchemaResolversTypes['ScheduledWorkoutConnection']
    | SchemaResolversTypes['ExerciseConnection']
    | SchemaResolversTypes['MuscleConnection']
    | SchemaResolversTypes['EquipmentConnection']
    | SchemaResolversTypes['ExerciseCategoryConnection'];
  Query: ResolverTypeWrapper<{}>;
  Mutation: ResolverTypeWrapper<{}>;
  RefreshTokenResponse: ResolverTypeWrapper<SchemaRefreshTokenResponse>;
  RegisterUserResponse: ResolverTypeWrapper<
    Omit<SchemaRegisterUserResponse, 'user'> & { user: SchemaResolversTypes['User'] }
  >;
  UserLoginResponse: ResolverTypeWrapper<
    Omit<SchemaUserLoginResponse, 'user'> & { user: SchemaResolversTypes['User'] }
  >;
  CreateScheduledWorkoutResponse: ResolverTypeWrapper<
    Omit<SchemaCreateScheduledWorkoutResponse, 'scheduledWorkout'> & {
      scheduledWorkout: SchemaResolversTypes['ScheduledWorkout'];
    }
  >;
  StartScheduledWorkoutResponse: ResolverTypeWrapper<
    Omit<SchemaStartScheduledWorkoutResponse, 'scheduledWorkout'> & {
      scheduledWorkout: SchemaResolversTypes['ScheduledWorkout'];
    }
  >;
  User: ResolverTypeWrapper<User>;
  UserConnection: ResolverTypeWrapper<
    Omit<SchemaUserConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['UserEdge']>;
      nodes: Array<SchemaResolversTypes['User']>;
    }
  >;
  UserEdge: ResolverTypeWrapper<Omit<SchemaUserEdge, 'node'> & { node: SchemaResolversTypes['User'] }>;
  WorkoutInvitation: ResolverTypeWrapper<WorkoutInvitation>;
  WorkoutInvitationConnection: ResolverTypeWrapper<
    Omit<SchemaWorkoutInvitationConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['WorkoutInvitationEdge']>;
      nodes: Array<SchemaResolversTypes['WorkoutInvitation']>;
    }
  >;
  WorkoutInvitationEdge: ResolverTypeWrapper<
    Omit<SchemaWorkoutInvitationEdge, 'node'> & { node: SchemaResolversTypes['WorkoutInvitation'] }
  >;
  Workout: ResolverTypeWrapper<Workout>;
  WorkoutConnection: ResolverTypeWrapper<
    Omit<SchemaWorkoutConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['WorkoutEdge']>;
      nodes: Array<SchemaResolversTypes['Workout']>;
    }
  >;
  WorkoutEdge: ResolverTypeWrapper<Omit<SchemaWorkoutEdge, 'node'> & { node: SchemaResolversTypes['Workout'] }>;
  ExerciseGroup: ResolverTypeWrapper<
    Omit<SchemaExerciseGroup, 'exercise'> & { exercise: SchemaResolversTypes['Exercise'] }
  >;
  ExerciseGroupConnection: ResolverTypeWrapper<
    Omit<SchemaExerciseGroupConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['ExerciseGroupEdge']>;
      nodes: Array<SchemaResolversTypes['ExerciseGroup']>;
    }
  >;
  ExerciseGroupEdge: ResolverTypeWrapper<
    Omit<SchemaExerciseGroupEdge, 'node'> & { node: SchemaResolversTypes['ExerciseGroup'] }
  >;
  ScheduledWorkout: ResolverTypeWrapper<ScheduledWorkout>;
  ScheduledWorkoutConnection: ResolverTypeWrapper<
    Omit<SchemaScheduledWorkoutConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['ScheduledWorkoutEdge']>;
      nodes: Array<SchemaResolversTypes['ScheduledWorkout']>;
    }
  >;
  ScheduledWorkoutEdge: ResolverTypeWrapper<
    Omit<SchemaScheduledWorkoutEdge, 'node'> & { node: SchemaResolversTypes['ScheduledWorkout'] }
  >;
  Exercise: ResolverTypeWrapper<Exercise>;
  ExerciseConnection: ResolverTypeWrapper<
    Omit<SchemaExerciseConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['ExerciseEdge']>;
      nodes: Array<SchemaResolversTypes['Exercise']>;
    }
  >;
  ExerciseEdge: ResolverTypeWrapper<Omit<SchemaExerciseEdge, 'node'> & { node: SchemaResolversTypes['Exercise'] }>;
  Muscle: ResolverTypeWrapper<Muscle>;
  MuscleConnection: ResolverTypeWrapper<
    Omit<SchemaMuscleConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['MuscleEdge']>;
      nodes: Array<SchemaResolversTypes['Muscle']>;
    }
  >;
  MuscleEdge: ResolverTypeWrapper<Omit<SchemaMuscleEdge, 'node'> & { node: SchemaResolversTypes['Muscle'] }>;
  ExerciseStep: ResolverTypeWrapper<ExerciseStep>;
  Equipment: ResolverTypeWrapper<Equipment>;
  EquipmentConnection: ResolverTypeWrapper<
    Omit<SchemaEquipmentConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['EquipmentEdge']>;
      nodes: Array<SchemaResolversTypes['Equipment']>;
    }
  >;
  EquipmentEdge: ResolverTypeWrapper<Omit<SchemaEquipmentEdge, 'node'> & { node: SchemaResolversTypes['Equipment'] }>;
  ExerciseCategory: ResolverTypeWrapper<ExerciseCategory>;
  ExerciseCategoryConnection: ResolverTypeWrapper<
    Omit<SchemaExerciseCategoryConnection, 'edges' | 'nodes'> & {
      edges: Array<SchemaResolversTypes['ExerciseCategoryEdge']>;
      nodes: Array<SchemaResolversTypes['ExerciseCategory']>;
    }
  >;
  ExerciseCategoryEdge: ResolverTypeWrapper<
    Omit<SchemaExerciseCategoryEdge, 'node'> & { node: SchemaResolversTypes['ExerciseCategory'] }
  >;
  PageInfo: ResolverTypeWrapper<SchemaPageInfo>;
  RegisterUser: SchemaRegisterUser;
  UserLogin: SchemaUserLogin;
  NewScheduledWorkout: SchemaNewScheduledWorkout;
  RefreshTokenInput: SchemaRefreshTokenInput;
  WorkoutInvitationFilter: SchemaWorkoutInvitationFilter;
  PaginationInput: SchemaPaginationInput;
  WorkoutInvitationStatus: SchemaWorkoutInvitationStatus;
}>;

/** Mapping between all available schema types and the resolvers parents */
export type SchemaResolversParentTypes = ResolversObject<{
  DateTime: Scalars['DateTime'];
  MutationResponse:
    | SchemaResolversParentTypes['RefreshTokenResponse']
    | SchemaResolversParentTypes['RegisterUserResponse']
    | SchemaResolversParentTypes['UserLoginResponse']
    | SchemaResolversParentTypes['CreateScheduledWorkoutResponse']
    | SchemaResolversParentTypes['StartScheduledWorkoutResponse'];
  Boolean: Scalars['Boolean'];
  Node:
    | SchemaResolversParentTypes['User']
    | SchemaResolversParentTypes['WorkoutInvitation']
    | SchemaResolversParentTypes['Workout']
    | SchemaResolversParentTypes['ExerciseGroup']
    | SchemaResolversParentTypes['ScheduledWorkout']
    | SchemaResolversParentTypes['Exercise']
    | SchemaResolversParentTypes['Muscle']
    | SchemaResolversParentTypes['Equipment']
    | SchemaResolversParentTypes['ExerciseCategory'];
  Int: Scalars['Int'];
  Edge:
    | SchemaResolversParentTypes['UserEdge']
    | SchemaResolversParentTypes['WorkoutInvitationEdge']
    | SchemaResolversParentTypes['WorkoutEdge']
    | SchemaResolversParentTypes['ExerciseGroupEdge']
    | SchemaResolversParentTypes['ScheduledWorkoutEdge']
    | SchemaResolversParentTypes['ExerciseEdge']
    | SchemaResolversParentTypes['MuscleEdge']
    | SchemaResolversParentTypes['EquipmentEdge']
    | SchemaResolversParentTypes['ExerciseCategoryEdge'];
  String: Scalars['String'];
  CursorPaginatedResponse:
    | SchemaResolversParentTypes['UserConnection']
    | SchemaResolversParentTypes['WorkoutInvitationConnection']
    | SchemaResolversParentTypes['WorkoutConnection']
    | SchemaResolversParentTypes['ExerciseGroupConnection']
    | SchemaResolversParentTypes['ScheduledWorkoutConnection']
    | SchemaResolversParentTypes['ExerciseConnection']
    | SchemaResolversParentTypes['MuscleConnection']
    | SchemaResolversParentTypes['EquipmentConnection']
    | SchemaResolversParentTypes['ExerciseCategoryConnection'];
  Query: {};
  Mutation: {};
  RefreshTokenResponse: SchemaRefreshTokenResponse;
  RegisterUserResponse: Omit<SchemaRegisterUserResponse, 'user'> & { user: SchemaResolversParentTypes['User'] };
  UserLoginResponse: Omit<SchemaUserLoginResponse, 'user'> & { user: SchemaResolversParentTypes['User'] };
  CreateScheduledWorkoutResponse: Omit<SchemaCreateScheduledWorkoutResponse, 'scheduledWorkout'> & {
    scheduledWorkout: SchemaResolversParentTypes['ScheduledWorkout'];
  };
  StartScheduledWorkoutResponse: Omit<SchemaStartScheduledWorkoutResponse, 'scheduledWorkout'> & {
    scheduledWorkout: SchemaResolversParentTypes['ScheduledWorkout'];
  };
  User: User;
  UserConnection: Omit<SchemaUserConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['UserEdge']>;
    nodes: Array<SchemaResolversParentTypes['User']>;
  };
  UserEdge: Omit<SchemaUserEdge, 'node'> & { node: SchemaResolversParentTypes['User'] };
  WorkoutInvitation: WorkoutInvitation;
  WorkoutInvitationConnection: Omit<SchemaWorkoutInvitationConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['WorkoutInvitationEdge']>;
    nodes: Array<SchemaResolversParentTypes['WorkoutInvitation']>;
  };
  WorkoutInvitationEdge: Omit<SchemaWorkoutInvitationEdge, 'node'> & {
    node: SchemaResolversParentTypes['WorkoutInvitation'];
  };
  Workout: Workout;
  WorkoutConnection: Omit<SchemaWorkoutConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['WorkoutEdge']>;
    nodes: Array<SchemaResolversParentTypes['Workout']>;
  };
  WorkoutEdge: Omit<SchemaWorkoutEdge, 'node'> & { node: SchemaResolversParentTypes['Workout'] };
  ExerciseGroup: Omit<SchemaExerciseGroup, 'exercise'> & { exercise: SchemaResolversParentTypes['Exercise'] };
  ExerciseGroupConnection: Omit<SchemaExerciseGroupConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['ExerciseGroupEdge']>;
    nodes: Array<SchemaResolversParentTypes['ExerciseGroup']>;
  };
  ExerciseGroupEdge: Omit<SchemaExerciseGroupEdge, 'node'> & { node: SchemaResolversParentTypes['ExerciseGroup'] };
  ScheduledWorkout: ScheduledWorkout;
  ScheduledWorkoutConnection: Omit<SchemaScheduledWorkoutConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['ScheduledWorkoutEdge']>;
    nodes: Array<SchemaResolversParentTypes['ScheduledWorkout']>;
  };
  ScheduledWorkoutEdge: Omit<SchemaScheduledWorkoutEdge, 'node'> & {
    node: SchemaResolversParentTypes['ScheduledWorkout'];
  };
  Exercise: Exercise;
  ExerciseConnection: Omit<SchemaExerciseConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['ExerciseEdge']>;
    nodes: Array<SchemaResolversParentTypes['Exercise']>;
  };
  ExerciseEdge: Omit<SchemaExerciseEdge, 'node'> & { node: SchemaResolversParentTypes['Exercise'] };
  Muscle: Muscle;
  MuscleConnection: Omit<SchemaMuscleConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['MuscleEdge']>;
    nodes: Array<SchemaResolversParentTypes['Muscle']>;
  };
  MuscleEdge: Omit<SchemaMuscleEdge, 'node'> & { node: SchemaResolversParentTypes['Muscle'] };
  ExerciseStep: ExerciseStep;
  Equipment: Equipment;
  EquipmentConnection: Omit<SchemaEquipmentConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['EquipmentEdge']>;
    nodes: Array<SchemaResolversParentTypes['Equipment']>;
  };
  EquipmentEdge: Omit<SchemaEquipmentEdge, 'node'> & { node: SchemaResolversParentTypes['Equipment'] };
  ExerciseCategory: ExerciseCategory;
  ExerciseCategoryConnection: Omit<SchemaExerciseCategoryConnection, 'edges' | 'nodes'> & {
    edges: Array<SchemaResolversParentTypes['ExerciseCategoryEdge']>;
    nodes: Array<SchemaResolversParentTypes['ExerciseCategory']>;
  };
  ExerciseCategoryEdge: Omit<SchemaExerciseCategoryEdge, 'node'> & {
    node: SchemaResolversParentTypes['ExerciseCategory'];
  };
  PageInfo: SchemaPageInfo;
  RegisterUser: SchemaRegisterUser;
  UserLogin: SchemaUserLogin;
  NewScheduledWorkout: SchemaNewScheduledWorkout;
  RefreshTokenInput: SchemaRefreshTokenInput;
  WorkoutInvitationFilter: SchemaWorkoutInvitationFilter;
  PaginationInput: SchemaPaginationInput;
}>;

export type SchemaDateFormatDirectiveArgs = {
  defaultFormat?: Maybe<Scalars['String']>;
  defaultTimeZone?: Maybe<Scalars['String']>;
};

export type SchemaDateFormatDirectiveResolver<
  Result,
  Parent,
  ContextType = any,
  Args = SchemaDateFormatDirectiveArgs
> = DirectiveResolverFn<Result, Parent, ContextType, Args>;

export interface SchemaDateTimeScalarConfig extends GraphQLScalarTypeConfig<SchemaResolversTypes['DateTime'], any> {
  name: 'DateTime';
}

export type SchemaMutationResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['MutationResponse'] = SchemaResolversParentTypes['MutationResponse']
> = ResolversObject<{
  __resolveType: TypeResolveFn<
    | 'RefreshTokenResponse'
    | 'RegisterUserResponse'
    | 'UserLoginResponse'
    | 'CreateScheduledWorkoutResponse'
    | 'StartScheduledWorkoutResponse',
    ParentType,
    ContextType
  >;
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
}>;

export type SchemaNodeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Node'] = SchemaResolversParentTypes['Node']
> = ResolversObject<{
  __resolveType: TypeResolveFn<
    | 'User'
    | 'WorkoutInvitation'
    | 'Workout'
    | 'ExerciseGroup'
    | 'ScheduledWorkout'
    | 'Exercise'
    | 'Muscle'
    | 'Equipment'
    | 'ExerciseCategory',
    ParentType,
    ContextType
  >;
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
}>;

export type SchemaEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Edge'] = SchemaResolversParentTypes['Edge']
> = ResolversObject<{
  __resolveType: TypeResolveFn<
    | 'UserEdge'
    | 'WorkoutInvitationEdge'
    | 'WorkoutEdge'
    | 'ExerciseGroupEdge'
    | 'ScheduledWorkoutEdge'
    | 'ExerciseEdge'
    | 'MuscleEdge'
    | 'EquipmentEdge'
    | 'ExerciseCategoryEdge',
    ParentType,
    ContextType
  >;
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
}>;

export type SchemaCursorPaginatedResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['CursorPaginatedResponse'] = SchemaResolversParentTypes['CursorPaginatedResponse']
> = ResolversObject<{
  __resolveType: TypeResolveFn<
    | 'UserConnection'
    | 'WorkoutInvitationConnection'
    | 'WorkoutConnection'
    | 'ExerciseGroupConnection'
    | 'ScheduledWorkoutConnection'
    | 'ExerciseConnection'
    | 'MuscleConnection'
    | 'EquipmentConnection'
    | 'ExerciseCategoryConnection',
    ParentType,
    ContextType
  >;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
}>;

export type SchemaQueryResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Query'] = SchemaResolversParentTypes['Query']
> = ResolversObject<{
  user?: Resolver<
    Maybe<SchemaResolversTypes['User']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryUserArgs, 'id'>
  >;
  users?: Resolver<
    SchemaResolversTypes['UserConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryUsersArgs, never>
  >;
  exercise?: Resolver<
    Maybe<SchemaResolversTypes['Exercise']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExerciseArgs, 'id'>
  >;
  exercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExercisesArgs, never>
  >;
  exerciseCategory?: Resolver<
    Maybe<SchemaResolversTypes['ExerciseCategory']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExerciseCategoryArgs, 'id'>
  >;
  exerciseCategories?: Resolver<
    SchemaResolversTypes['ExerciseCategoryConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExerciseCategoriesArgs, never>
  >;
  muscle?: Resolver<
    Maybe<SchemaResolversTypes['Muscle']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryMuscleArgs, 'id'>
  >;
  muscles?: Resolver<
    SchemaResolversTypes['MuscleConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryMusclesArgs, never>
  >;
  equipment?: Resolver<
    Maybe<SchemaResolversTypes['Equipment']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryEquipmentArgs, 'id'>
  >;
  allEquipment?: Resolver<
    SchemaResolversTypes['EquipmentConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryAllEquipmentArgs, never>
  >;
  workout?: Resolver<
    Maybe<SchemaResolversTypes['Workout']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryWorkoutArgs, 'id'>
  >;
  workouts?: Resolver<
    SchemaResolversTypes['WorkoutConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaQueryWorkoutsArgs, never>
  >;
  me?: Resolver<Maybe<SchemaResolversTypes['User']>, ParentType, ContextType>;
}>;

export type SchemaMutationResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Mutation'] = SchemaResolversParentTypes['Mutation']
> = ResolversObject<{
  registerUser?: Resolver<
    SchemaResolversTypes['RegisterUserResponse'],
    ParentType,
    ContextType,
    RequireFields<SchemaMutationRegisterUserArgs, 'user'>
  >;
  login?: Resolver<
    SchemaResolversTypes['UserLoginResponse'],
    ParentType,
    ContextType,
    RequireFields<SchemaMutationLoginArgs, 'userCredentials'>
  >;
  refreshToken?: Resolver<
    SchemaResolversTypes['RefreshTokenResponse'],
    ParentType,
    ContextType,
    RequireFields<SchemaMutationRefreshTokenArgs, 'input'>
  >;
  createScheduledWorkout?: Resolver<
    SchemaResolversTypes['CreateScheduledWorkoutResponse'],
    ParentType,
    ContextType,
    RequireFields<SchemaMutationCreateScheduledWorkoutArgs, 'newWorkout'>
  >;
  startScheduledWorkout?: Resolver<
    SchemaResolversTypes['StartScheduledWorkoutResponse'],
    ParentType,
    ContextType,
    RequireFields<SchemaMutationStartScheduledWorkoutArgs, 'id'>
  >;
}>;

export type SchemaRefreshTokenResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['RefreshTokenResponse'] = SchemaResolversParentTypes['RefreshTokenResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  token?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  refreshToken?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaRegisterUserResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['RegisterUserResponse'] = SchemaResolversParentTypes['RegisterUserResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  user?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaUserLoginResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['UserLoginResponse'] = SchemaResolversParentTypes['UserLoginResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  token?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  refreshToken?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  user?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaCreateScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['CreateScheduledWorkoutResponse'] = SchemaResolversParentTypes['CreateScheduledWorkoutResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaStartScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['StartScheduledWorkoutResponse'] = SchemaResolversParentTypes['StartScheduledWorkoutResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaUserResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['User'] = SchemaResolversParentTypes['User']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  userName?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  firstName?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  lastName?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  email?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  created?: Resolver<
    SchemaResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserCreatedArgs, never>
  >;
  scheduledWorkouts?: Resolver<
    SchemaResolversTypes['ScheduledWorkoutConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserScheduledWorkoutsArgs, never>
  >;
  ownedScheduledWorkouts?: Resolver<
    SchemaResolversTypes['ScheduledWorkoutConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserOwnedScheduledWorkoutsArgs, never>
  >;
  favoriteExercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserFavoriteExercisesArgs, never>
  >;
  sentWorkoutInvitations?: Resolver<
    SchemaResolversTypes['WorkoutInvitationConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserSentWorkoutInvitationsArgs, never>
  >;
  receivedWorkoutInvitations?: Resolver<
    SchemaResolversTypes['WorkoutInvitationConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaUserReceivedWorkoutInvitationsArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaUserConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['UserConnection'] = SchemaResolversParentTypes['UserConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['UserEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['User']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaUserEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['UserEdge'] = SchemaResolversParentTypes['UserEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutInvitationResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['WorkoutInvitation'] = SchemaResolversParentTypes['WorkoutInvitation']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  inviter?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  invitee?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  accepted?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  declined?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  status?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  respondedAtDateTime?: Resolver<
    Maybe<SchemaResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<SchemaWorkoutInvitationRespondedAtDateTimeArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutInvitationConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['WorkoutInvitationConnection'] = SchemaResolversParentTypes['WorkoutInvitationConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['WorkoutInvitationEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['WorkoutInvitation']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutInvitationEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['WorkoutInvitationEdge'] = SchemaResolversParentTypes['WorkoutInvitationEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['WorkoutInvitation'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Workout'] = SchemaResolversParentTypes['Workout']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  label?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  description?: Resolver<Maybe<SchemaResolversTypes['String']>, ParentType, ContextType>;
  createdByUserId?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  createdByUser?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  createdOnDate?: Resolver<
    SchemaResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<SchemaWorkoutCreatedOnDateArgs, never>
  >;
  lastModifiedDate?: Resolver<
    SchemaResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<SchemaWorkoutLastModifiedDateArgs, never>
  >;
  shareable?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  exerciseGroups?: Resolver<
    SchemaResolversTypes['ExerciseGroupConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaWorkoutExerciseGroupsArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['WorkoutConnection'] = SchemaResolversParentTypes['WorkoutConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['WorkoutEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['Workout']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaWorkoutEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['WorkoutEdge'] = SchemaResolversParentTypes['WorkoutEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['Workout'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseGroupResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseGroup'] = SchemaResolversParentTypes['ExerciseGroup']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  exerciseId?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  exercise?: Resolver<SchemaResolversTypes['Exercise'], ParentType, ContextType>;
  sets?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  repetitions?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseGroupConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseGroupConnection'] = SchemaResolversParentTypes['ExerciseGroupConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['ExerciseGroupEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['ExerciseGroup']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseGroupEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseGroupEdge'] = SchemaResolversParentTypes['ExerciseGroupEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['ExerciseGroup'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaScheduledWorkoutResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ScheduledWorkout'] = SchemaResolversParentTypes['ScheduledWorkout']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  scheduledByUser?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  workout?: Resolver<SchemaResolversTypes['Workout'], ParentType, ContextType>;
  startedDateTime?: Resolver<
    Maybe<SchemaResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<SchemaScheduledWorkoutStartedDateTimeArgs, never>
  >;
  completedDateTime?: Resolver<
    Maybe<SchemaResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<SchemaScheduledWorkoutCompletedDateTimeArgs, never>
  >;
  scheduledDateTime?: Resolver<
    SchemaResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<SchemaScheduledWorkoutScheduledDateTimeArgs, never>
  >;
  customWorkout?: Resolver<Maybe<SchemaResolversTypes['String']>, ParentType, ContextType>;
  adHocExercises?: Resolver<
    SchemaResolversTypes['ExerciseGroupConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaScheduledWorkoutAdHocExercisesArgs, never>
  >;
  attendees?: Resolver<
    SchemaResolversTypes['UserConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaScheduledWorkoutAttendeesArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaScheduledWorkoutConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ScheduledWorkoutConnection'] = SchemaResolversParentTypes['ScheduledWorkoutConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['ScheduledWorkoutEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['ScheduledWorkout']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaScheduledWorkoutEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ScheduledWorkoutEdge'] = SchemaResolversParentTypes['ScheduledWorkoutEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Exercise'] = SchemaResolversParentTypes['Exercise']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  primaryMuscle?: Resolver<Maybe<SchemaResolversTypes['Muscle']>, ParentType, ContextType>;
  secondaryMuscle?: Resolver<Maybe<SchemaResolversTypes['Muscle']>, ParentType, ContextType>;
  exerciseSteps?: Resolver<Array<SchemaResolversTypes['ExerciseStep']>, ParentType, ContextType>;
  equipment?: Resolver<
    SchemaResolversTypes['EquipmentConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaExerciseEquipmentArgs, never>
  >;
  exerciseCategorys?: Resolver<
    SchemaResolversTypes['ExerciseCategoryConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaExerciseExerciseCategorysArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseConnection'] = SchemaResolversParentTypes['ExerciseConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['ExerciseEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseEdge'] = SchemaResolversParentTypes['ExerciseEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['Exercise'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaMuscleResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Muscle'] = SchemaResolversParentTypes['Muscle']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  description?: Resolver<Maybe<SchemaResolversTypes['String']>, ParentType, ContextType>;
  primaryExercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaMusclePrimaryExercisesArgs, never>
  >;
  secondaryExercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaMuscleSecondaryExercisesArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaMuscleConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['MuscleConnection'] = SchemaResolversParentTypes['MuscleConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['MuscleEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['Muscle']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaMuscleEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['MuscleEdge'] = SchemaResolversParentTypes['MuscleEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['Muscle'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseStepResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseStep'] = SchemaResolversParentTypes['ExerciseStep']
> = ResolversObject<{
  exerciseStepNumber?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  description?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaEquipmentResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Equipment'] = SchemaResolversParentTypes['Equipment']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaEquipmentExercisesArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaEquipmentConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['EquipmentConnection'] = SchemaResolversParentTypes['EquipmentConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['EquipmentEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['Equipment']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaEquipmentEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['EquipmentEdge'] = SchemaResolversParentTypes['EquipmentEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['Equipment'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseCategoryResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseCategory'] = SchemaResolversParentTypes['ExerciseCategory']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<
    SchemaResolversTypes['ExerciseConnection'],
    ParentType,
    ContextType,
    RequireFields<SchemaExerciseCategoryExercisesArgs, never>
  >;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseCategoryConnectionResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseCategoryConnection'] = SchemaResolversParentTypes['ExerciseCategoryConnection']
> = ResolversObject<{
  edges?: Resolver<Array<SchemaResolversTypes['ExerciseCategoryEdge']>, ParentType, ContextType>;
  nodes?: Resolver<Array<SchemaResolversTypes['ExerciseCategory']>, ParentType, ContextType>;
  pageInfo?: Resolver<SchemaResolversTypes['PageInfo'], ParentType, ContextType>;
  totalCount?: Resolver<Maybe<SchemaResolversTypes['Int']>, ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseCategoryEdgeResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseCategoryEdge'] = SchemaResolversParentTypes['ExerciseCategoryEdge']
> = ResolversObject<{
  cursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  node?: Resolver<SchemaResolversTypes['ExerciseCategory'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaPageInfoResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['PageInfo'] = SchemaResolversParentTypes['PageInfo']
> = ResolversObject<{
  startCursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  endCursor?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  hasNextPage?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  hasPreviousPage?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  __isTypeOf?: IsTypeOfResolverFn<ParentType>;
}>;

export type SchemaResolvers<ContextType = any> = ResolversObject<{
  DateTime?: GraphQLScalarType;
  MutationResponse?: SchemaMutationResponseResolvers;
  Node?: SchemaNodeResolvers;
  Edge?: SchemaEdgeResolvers;
  CursorPaginatedResponse?: SchemaCursorPaginatedResponseResolvers;
  Query?: SchemaQueryResolvers<ContextType>;
  Mutation?: SchemaMutationResolvers<ContextType>;
  RefreshTokenResponse?: SchemaRefreshTokenResponseResolvers<ContextType>;
  RegisterUserResponse?: SchemaRegisterUserResponseResolvers<ContextType>;
  UserLoginResponse?: SchemaUserLoginResponseResolvers<ContextType>;
  CreateScheduledWorkoutResponse?: SchemaCreateScheduledWorkoutResponseResolvers<ContextType>;
  StartScheduledWorkoutResponse?: SchemaStartScheduledWorkoutResponseResolvers<ContextType>;
  User?: SchemaUserResolvers<ContextType>;
  UserConnection?: SchemaUserConnectionResolvers<ContextType>;
  UserEdge?: SchemaUserEdgeResolvers<ContextType>;
  WorkoutInvitation?: SchemaWorkoutInvitationResolvers<ContextType>;
  WorkoutInvitationConnection?: SchemaWorkoutInvitationConnectionResolvers<ContextType>;
  WorkoutInvitationEdge?: SchemaWorkoutInvitationEdgeResolvers<ContextType>;
  Workout?: SchemaWorkoutResolvers<ContextType>;
  WorkoutConnection?: SchemaWorkoutConnectionResolvers<ContextType>;
  WorkoutEdge?: SchemaWorkoutEdgeResolvers<ContextType>;
  ExerciseGroup?: SchemaExerciseGroupResolvers<ContextType>;
  ExerciseGroupConnection?: SchemaExerciseGroupConnectionResolvers<ContextType>;
  ExerciseGroupEdge?: SchemaExerciseGroupEdgeResolvers<ContextType>;
  ScheduledWorkout?: SchemaScheduledWorkoutResolvers<ContextType>;
  ScheduledWorkoutConnection?: SchemaScheduledWorkoutConnectionResolvers<ContextType>;
  ScheduledWorkoutEdge?: SchemaScheduledWorkoutEdgeResolvers<ContextType>;
  Exercise?: SchemaExerciseResolvers<ContextType>;
  ExerciseConnection?: SchemaExerciseConnectionResolvers<ContextType>;
  ExerciseEdge?: SchemaExerciseEdgeResolvers<ContextType>;
  Muscle?: SchemaMuscleResolvers<ContextType>;
  MuscleConnection?: SchemaMuscleConnectionResolvers<ContextType>;
  MuscleEdge?: SchemaMuscleEdgeResolvers<ContextType>;
  ExerciseStep?: SchemaExerciseStepResolvers<ContextType>;
  Equipment?: SchemaEquipmentResolvers<ContextType>;
  EquipmentConnection?: SchemaEquipmentConnectionResolvers<ContextType>;
  EquipmentEdge?: SchemaEquipmentEdgeResolvers<ContextType>;
  ExerciseCategory?: SchemaExerciseCategoryResolvers<ContextType>;
  ExerciseCategoryConnection?: SchemaExerciseCategoryConnectionResolvers<ContextType>;
  ExerciseCategoryEdge?: SchemaExerciseCategoryEdgeResolvers<ContextType>;
  PageInfo?: SchemaPageInfoResolvers<ContextType>;
}>;

export type SchemaDirectiveResolvers<ContextType = any> = ResolversObject<{
  dateFormat?: SchemaDateFormatDirectiveResolver<any, any, ContextType>;
}>;
