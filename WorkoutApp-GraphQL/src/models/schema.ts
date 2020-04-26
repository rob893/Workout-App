import { GraphQLResolveInfo, GraphQLScalarType, GraphQLScalarTypeConfig } from 'graphql';
import { User } from './workout-api/User';
import { ExerciseDetailed, Muscle, Equipment, ExerciseStep, ExerciseCategory } from './workout-api/Exercise';
import { WorkoutDetailed, WorkoutInvitation } from './workout-api/Workout';
export type Maybe<T> = T | null;
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

export type SchemaQuery = {
  __typename?: 'Query';
  test: Scalars['String'];
  user?: Maybe<SchemaUser>;
  /** Get the list of users */
  users: Array<Maybe<SchemaUser>>;
  exercise?: Maybe<SchemaExercise>;
  exercises: Array<SchemaExercise>;
  exerciseCategory?: Maybe<SchemaExerciseCategory>;
  exerciseCategories: Array<SchemaExerciseCategory>;
  muscle?: Maybe<SchemaMuscle>;
  muscles: Array<SchemaMuscle>;
  equipment?: Maybe<SchemaEquipment>;
  allEquipment: Array<SchemaEquipment>;
  workout?: Maybe<SchemaWorkout>;
  workouts: Array<SchemaWorkout>;
  me?: Maybe<SchemaUser>;
};

export type SchemaQueryUserArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryExerciseArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryExerciseCategoryArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryMuscleArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryEquipmentArgs = {
  id: Scalars['Int'];
};

export type SchemaQueryWorkoutArgs = {
  id: Scalars['Int'];
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

export type SchemaUser = {
  __typename?: 'User';
  id: Scalars['Int'];
  userName: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  email: Scalars['String'];
  created: Scalars['DateTime'];
  createdWorkouts: Array<SchemaWorkout>;
  scheduledWorkouts: Array<SchemaScheduledWorkout>;
  ownedScheduledWorkouts: Array<SchemaScheduledWorkout>;
  favoriteExercises: Array<SchemaExercise>;
  sentWorkoutInvitations: Array<SchemaWorkoutInvitation>;
  receivedWorkoutInvitations: Array<SchemaWorkoutInvitation>;
};

export type SchemaUserCreatedArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaWorkoutInvitation = {
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

export type SchemaWorkout = {
  __typename?: 'Workout';
  id: Scalars['Int'];
  label: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdByUserId: Scalars['Int'];
  createdByUser: SchemaUser;
  createdOnDate: Scalars['DateTime'];
  lastModifiedDate: Scalars['DateTime'];
  shareable: Scalars['Boolean'];
  exerciseGroups: Array<SchemaExerciseGroup>;
};

export type SchemaWorkoutCreatedOnDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaWorkoutLastModifiedDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type SchemaExerciseGroup = {
  __typename?: 'ExerciseGroup';
  id: Scalars['Int'];
  exerciseId: Scalars['Int'];
  exercise: SchemaExercise;
  sets: Scalars['Int'];
  repetitions: Scalars['Int'];
};

export type SchemaScheduledWorkout = {
  __typename?: 'ScheduledWorkout';
  id: Scalars['Int'];
  scheduledByUser: SchemaUser;
  workout: SchemaWorkout;
  startedDateTime?: Maybe<Scalars['DateTime']>;
  completedDateTime?: Maybe<Scalars['DateTime']>;
  scheduledDateTime: Scalars['DateTime'];
  customWorkout?: Maybe<Scalars['String']>;
  adHocExercises: Array<SchemaExerciseGroup>;
  attendees: Array<SchemaUser>;
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

export type SchemaExercise = {
  __typename?: 'Exercise';
  id: Scalars['Int'];
  name: Scalars['String'];
  primaryMuscle?: Maybe<SchemaMuscle>;
  secondaryMuscle?: Maybe<SchemaMuscle>;
  exerciseSteps: Array<SchemaExerciseStep>;
  equipment: Array<SchemaEquipment>;
  exerciseCategorys: Array<SchemaExerciseCategory>;
};

export type SchemaMuscle = {
  __typename?: 'Muscle';
  id: Scalars['Int'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  primaryExercises: Array<SchemaExercise>;
  secondaryExercises: Array<SchemaExercise>;
};

export type SchemaExerciseStep = {
  __typename?: 'ExerciseStep';
  exerciseStepNumber: Scalars['Int'];
  description: Scalars['String'];
};

export type SchemaEquipment = {
  __typename?: 'Equipment';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises: Array<SchemaExercise>;
};

export type SchemaExerciseCategory = {
  __typename?: 'ExerciseCategory';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises: Array<SchemaExercise>;
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

export type WithIndex<TObject> = TObject & Record<string, any>;
export type ResolversObject<TObject> = WithIndex<TObject>;

export type ResolverTypeWrapper<T> = Promise<T> | T;

export type StitchingResolver<TResult, TParent, TContext, TArgs> = {
  fragment: string;
  resolve: ResolverFn<TResult, TParent, TContext, TArgs>;
};

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

export type isTypeOfResolverFn<T = {}> = (obj: T, info: GraphQLResolveInfo) => boolean | Promise<boolean>;

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
  String: ResolverTypeWrapper<Scalars['String']>;
  Boolean: ResolverTypeWrapper<Scalars['Boolean']>;
  DateTime: ResolverTypeWrapper<Scalars['DateTime']>;
  MutationResponse:
    | SchemaResolversTypes['RefreshTokenResponse']
    | SchemaResolversTypes['RegisterUserResponse']
    | SchemaResolversTypes['UserLoginResponse']
    | SchemaResolversTypes['CreateScheduledWorkoutResponse']
    | SchemaResolversTypes['StartScheduledWorkoutResponse'];
  Query: ResolverTypeWrapper<{}>;
  Int: ResolverTypeWrapper<Scalars['Int']>;
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
  WorkoutInvitation: ResolverTypeWrapper<WorkoutInvitation>;
  Workout: ResolverTypeWrapper<WorkoutDetailed>;
  ExerciseGroup: ResolverTypeWrapper<
    Omit<SchemaExerciseGroup, 'exercise'> & { exercise: SchemaResolversTypes['Exercise'] }
  >;
  ScheduledWorkout: ResolverTypeWrapper<
    Omit<SchemaScheduledWorkout, 'scheduledByUser' | 'workout' | 'adHocExercises' | 'attendees'> & {
      scheduledByUser: SchemaResolversTypes['User'];
      workout: SchemaResolversTypes['Workout'];
      adHocExercises: Array<SchemaResolversTypes['ExerciseGroup']>;
      attendees: Array<SchemaResolversTypes['User']>;
    }
  >;
  Exercise: ResolverTypeWrapper<ExerciseDetailed>;
  Muscle: ResolverTypeWrapper<Muscle>;
  ExerciseStep: ResolverTypeWrapper<ExerciseStep>;
  Equipment: ResolverTypeWrapper<Equipment>;
  ExerciseCategory: ResolverTypeWrapper<ExerciseCategory>;
  RegisterUser: SchemaRegisterUser;
  UserLogin: SchemaUserLogin;
  NewScheduledWorkout: SchemaNewScheduledWorkout;
  RefreshTokenInput: SchemaRefreshTokenInput;
}>;

/** Mapping between all available schema types and the resolvers parents */
export type SchemaResolversParentTypes = ResolversObject<{
  String: Scalars['String'];
  Boolean: Scalars['Boolean'];
  DateTime: Scalars['DateTime'];
  MutationResponse:
    | SchemaResolversParentTypes['RefreshTokenResponse']
    | SchemaResolversParentTypes['RegisterUserResponse']
    | SchemaResolversParentTypes['UserLoginResponse']
    | SchemaResolversParentTypes['CreateScheduledWorkoutResponse']
    | SchemaResolversParentTypes['StartScheduledWorkoutResponse'];
  Query: {};
  Int: Scalars['Int'];
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
  WorkoutInvitation: WorkoutInvitation;
  Workout: WorkoutDetailed;
  ExerciseGroup: Omit<SchemaExerciseGroup, 'exercise'> & { exercise: SchemaResolversParentTypes['Exercise'] };
  ScheduledWorkout: Omit<SchemaScheduledWorkout, 'scheduledByUser' | 'workout' | 'adHocExercises' | 'attendees'> & {
    scheduledByUser: SchemaResolversParentTypes['User'];
    workout: SchemaResolversParentTypes['Workout'];
    adHocExercises: Array<SchemaResolversParentTypes['ExerciseGroup']>;
    attendees: Array<SchemaResolversParentTypes['User']>;
  };
  Exercise: ExerciseDetailed;
  Muscle: Muscle;
  ExerciseStep: ExerciseStep;
  Equipment: Equipment;
  ExerciseCategory: ExerciseCategory;
  RegisterUser: SchemaRegisterUser;
  UserLogin: SchemaUserLogin;
  NewScheduledWorkout: SchemaNewScheduledWorkout;
  RefreshTokenInput: SchemaRefreshTokenInput;
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

export type SchemaQueryResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Query'] = SchemaResolversParentTypes['Query']
> = ResolversObject<{
  test?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  user?: Resolver<
    Maybe<SchemaResolversTypes['User']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryUserArgs, 'id'>
  >;
  users?: Resolver<Array<Maybe<SchemaResolversTypes['User']>>, ParentType, ContextType>;
  exercise?: Resolver<
    Maybe<SchemaResolversTypes['Exercise']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExerciseArgs, 'id'>
  >;
  exercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  exerciseCategory?: Resolver<
    Maybe<SchemaResolversTypes['ExerciseCategory']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryExerciseCategoryArgs, 'id'>
  >;
  exerciseCategories?: Resolver<Array<SchemaResolversTypes['ExerciseCategory']>, ParentType, ContextType>;
  muscle?: Resolver<
    Maybe<SchemaResolversTypes['Muscle']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryMuscleArgs, 'id'>
  >;
  muscles?: Resolver<Array<SchemaResolversTypes['Muscle']>, ParentType, ContextType>;
  equipment?: Resolver<
    Maybe<SchemaResolversTypes['Equipment']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryEquipmentArgs, 'id'>
  >;
  allEquipment?: Resolver<Array<SchemaResolversTypes['Equipment']>, ParentType, ContextType>;
  workout?: Resolver<
    Maybe<SchemaResolversTypes['Workout']>,
    ParentType,
    ContextType,
    RequireFields<SchemaQueryWorkoutArgs, 'id'>
  >;
  workouts?: Resolver<Array<SchemaResolversTypes['Workout']>, ParentType, ContextType>;
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
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaRegisterUserResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['RegisterUserResponse'] = SchemaResolversParentTypes['RegisterUserResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  user?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaUserLoginResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['UserLoginResponse'] = SchemaResolversParentTypes['UserLoginResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  token?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  refreshToken?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  user?: Resolver<SchemaResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaCreateScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['CreateScheduledWorkoutResponse'] = SchemaResolversParentTypes['CreateScheduledWorkoutResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaStartScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['StartScheduledWorkoutResponse'] = SchemaResolversParentTypes['StartScheduledWorkoutResponse']
> = ResolversObject<{
  success?: Resolver<SchemaResolversTypes['Boolean'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<SchemaResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  createdWorkouts?: Resolver<Array<SchemaResolversTypes['Workout']>, ParentType, ContextType>;
  scheduledWorkouts?: Resolver<Array<SchemaResolversTypes['ScheduledWorkout']>, ParentType, ContextType>;
  ownedScheduledWorkouts?: Resolver<Array<SchemaResolversTypes['ScheduledWorkout']>, ParentType, ContextType>;
  favoriteExercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  sentWorkoutInvitations?: Resolver<Array<SchemaResolversTypes['WorkoutInvitation']>, ParentType, ContextType>;
  receivedWorkoutInvitations?: Resolver<Array<SchemaResolversTypes['WorkoutInvitation']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  exerciseGroups?: Resolver<Array<SchemaResolversTypes['ExerciseGroup']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  adHocExercises?: Resolver<Array<SchemaResolversTypes['ExerciseGroup']>, ParentType, ContextType>;
  attendees?: Resolver<Array<SchemaResolversTypes['User']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
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
  equipment?: Resolver<Array<SchemaResolversTypes['Equipment']>, ParentType, ContextType>;
  exerciseCategorys?: Resolver<Array<SchemaResolversTypes['ExerciseCategory']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaMuscleResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Muscle'] = SchemaResolversParentTypes['Muscle']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  description?: Resolver<Maybe<SchemaResolversTypes['String']>, ParentType, ContextType>;
  primaryExercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  secondaryExercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseStepResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseStep'] = SchemaResolversParentTypes['ExerciseStep']
> = ResolversObject<{
  exerciseStepNumber?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  description?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaEquipmentResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['Equipment'] = SchemaResolversParentTypes['Equipment']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaExerciseCategoryResolvers<
  ContextType = any,
  ParentType extends SchemaResolversParentTypes['ExerciseCategory'] = SchemaResolversParentTypes['ExerciseCategory']
> = ResolversObject<{
  id?: Resolver<SchemaResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<SchemaResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<Array<SchemaResolversTypes['Exercise']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type SchemaResolvers<ContextType = any> = ResolversObject<{
  DateTime?: GraphQLScalarType;
  MutationResponse?: SchemaMutationResponseResolvers;
  Query?: SchemaQueryResolvers<ContextType>;
  Mutation?: SchemaMutationResolvers<ContextType>;
  RefreshTokenResponse?: SchemaRefreshTokenResponseResolvers<ContextType>;
  RegisterUserResponse?: SchemaRegisterUserResponseResolvers<ContextType>;
  UserLoginResponse?: SchemaUserLoginResponseResolvers<ContextType>;
  CreateScheduledWorkoutResponse?: SchemaCreateScheduledWorkoutResponseResolvers<ContextType>;
  StartScheduledWorkoutResponse?: SchemaStartScheduledWorkoutResponseResolvers<ContextType>;
  User?: SchemaUserResolvers<ContextType>;
  WorkoutInvitation?: SchemaWorkoutInvitationResolvers<ContextType>;
  Workout?: SchemaWorkoutResolvers<ContextType>;
  ExerciseGroup?: SchemaExerciseGroupResolvers<ContextType>;
  ScheduledWorkout?: SchemaScheduledWorkoutResolvers<ContextType>;
  Exercise?: SchemaExerciseResolvers<ContextType>;
  Muscle?: SchemaMuscleResolvers<ContextType>;
  ExerciseStep?: SchemaExerciseStepResolvers<ContextType>;
  Equipment?: SchemaEquipmentResolvers<ContextType>;
  ExerciseCategory?: SchemaExerciseCategoryResolvers<ContextType>;
}>;

export type SchemaDirectiveResolvers<ContextType = any> = ResolversObject<{
  dateFormat?: SchemaDateFormatDirectiveResolver<any, any, ContextType>;
}>;
