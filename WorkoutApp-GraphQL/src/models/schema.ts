import { GraphQLResolveInfo, GraphQLScalarType, GraphQLScalarTypeConfig } from 'graphql';
import { DeepPartial } from 'utility-types';
export type Maybe<T> = T | null;
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

export type Query = {
  __typename?: 'Query';
  test: Scalars['String'];
  user?: Maybe<User>;
  /** Get the list of users */
  users: Array<Maybe<User>>;
  exercise?: Maybe<Exercise>;
  exercises: Array<Exercise>;
  exerciseCategory?: Maybe<ExerciseCategory>;
  exerciseCategories: Array<ExerciseCategory>;
  muscle?: Maybe<Muscle>;
  muscles: Array<Muscle>;
  equipment?: Maybe<Equipment>;
  allEquipment: Array<Equipment>;
  workout?: Maybe<Workout>;
  workouts: Array<Workout>;
  me?: Maybe<User>;
};

export type QueryUserArgs = {
  id: Scalars['Int'];
};

export type QueryExerciseArgs = {
  id: Scalars['Int'];
};

export type QueryExerciseCategoryArgs = {
  id: Scalars['Int'];
};

export type QueryMuscleArgs = {
  id: Scalars['Int'];
};

export type QueryEquipmentArgs = {
  id: Scalars['Int'];
};

export type QueryWorkoutArgs = {
  id: Scalars['Int'];
};

export type Mutation = {
  __typename?: 'Mutation';
  registerUser: RegisterUserResponse;
  login: UserLoginResponse;
  refreshToken: RefreshTokenResponse;
  createScheduledWorkout: CreateScheduledWorkoutResponse;
  startScheduledWorkout: StartScheduledWorkoutResponse;
};

export type MutationRegisterUserArgs = {
  user: RegisterUser;
};

export type MutationLoginArgs = {
  userCredentials: UserLogin;
};

export type MutationRefreshTokenArgs = {
  input: RefreshTokenInput;
};

export type MutationCreateScheduledWorkoutArgs = {
  newWorkout: NewScheduledWorkout;
};

export type MutationStartScheduledWorkoutArgs = {
  id: Scalars['Int'];
};

export type RefreshTokenResponse = {
  __typename?: 'RefreshTokenResponse';
  token: Scalars['String'];
  refreshToken: Scalars['String'];
};

export type RegisterUserResponse = {
  __typename?: 'RegisterUserResponse';
  user: User;
};

export type UserLoginResponse = {
  __typename?: 'UserLoginResponse';
  token: Scalars['String'];
  refreshToken: Scalars['String'];
  user: User;
};

export type CreateScheduledWorkoutResponse = {
  __typename?: 'CreateScheduledWorkoutResponse';
  scheduledWorkout: ScheduledWorkout;
};

export type StartScheduledWorkoutResponse = {
  __typename?: 'StartScheduledWorkoutResponse';
  scheduledWorkout: ScheduledWorkout;
};

export type User = {
  __typename?: 'User';
  id: Scalars['Int'];
  userName: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  email: Scalars['String'];
  created: Scalars['DateTime'];
  roles?: Maybe<Array<Maybe<UserRole>>>;
  createdWorkouts?: Maybe<Array<Maybe<Workout>>>;
  scheduledWorkouts?: Maybe<Array<Maybe<ScheduledWorkout>>>;
  ownedScheduledWorkouts?: Maybe<Array<Maybe<ScheduledWorkout>>>;
  favoriteExercises?: Maybe<Array<Maybe<Exercise>>>;
};

export type UserCreatedArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type UserRole = {
  __typename?: 'UserRole';
  name: Scalars['String'];
};

export type WorkoutInvitation = {
  __typename?: 'WorkoutInvitation';
  inviter: User;
  invitee: User;
  scheduledWorkout: ScheduledWorkout;
  accepted: Scalars['Boolean'];
  declined: Scalars['Boolean'];
  status: Scalars['String'];
  respondedAtDateTime?: Maybe<Scalars['DateTime']>;
};

export type WorkoutInvitationRespondedAtDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type Workout = {
  __typename?: 'Workout';
  id: Scalars['Int'];
  label: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  createdByUserId: Scalars['Int'];
  createdByUser: User;
  createdOnDate: Scalars['DateTime'];
  lastModifiedDate: Scalars['DateTime'];
  shareable: Scalars['Boolean'];
  exerciseGroups?: Maybe<Array<Maybe<ExerciseGroup>>>;
};

export type WorkoutCreatedOnDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type WorkoutLastModifiedDateArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type ExerciseGroup = {
  __typename?: 'ExerciseGroup';
  id: Scalars['Int'];
  exercise: Exercise;
  sets: Scalars['Int'];
  repetitions: Scalars['Int'];
};

export type ScheduledWorkout = {
  __typename?: 'ScheduledWorkout';
  id: Scalars['Int'];
  scheduledByUser: User;
  workout: Workout;
  startedDateTime?: Maybe<Scalars['DateTime']>;
  completedDateTime?: Maybe<Scalars['DateTime']>;
  scheduledDateTime: Scalars['DateTime'];
  customWorkout?: Maybe<Scalars['String']>;
  adHocExercises?: Maybe<Array<Maybe<ExerciseGroup>>>;
  attendees?: Maybe<Array<Maybe<User>>>;
};

export type ScheduledWorkoutStartedDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type ScheduledWorkoutCompletedDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type ScheduledWorkoutScheduledDateTimeArgs = {
  format?: Maybe<Scalars['String']>;
  timeZone?: Maybe<Scalars['String']>;
};

export type Exercise = {
  __typename?: 'Exercise';
  id: Scalars['Int'];
  name: Scalars['String'];
  primaryMuscle?: Maybe<Muscle>;
  secondaryMuscle?: Maybe<Muscle>;
  exerciseSteps: Array<ExerciseStep>;
  equipment: Array<Equipment>;
  exerciseCategorys: Array<ExerciseCategory>;
};

export type Muscle = {
  __typename?: 'Muscle';
  id: Scalars['Int'];
  name: Scalars['String'];
  description?: Maybe<Scalars['String']>;
  primaryExercises: Array<Exercise>;
  secondaryExercises?: Maybe<Array<Maybe<Exercise>>>;
};

export type ExerciseStep = {
  __typename?: 'ExerciseStep';
  exerciseStepNumber: Scalars['Int'];
  description: Scalars['String'];
};

export type Equipment = {
  __typename?: 'Equipment';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises: Array<Maybe<Exercise>>;
};

export type ExerciseCategory = {
  __typename?: 'ExerciseCategory';
  id: Scalars['Int'];
  name: Scalars['String'];
  exercises?: Maybe<Array<Maybe<Exercise>>>;
};

export type RegisterUser = {
  username: Scalars['String'];
  password: Scalars['String'];
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  email: Scalars['String'];
};

export type UserLogin = {
  username: Scalars['String'];
  password: Scalars['String'];
  source: Scalars['String'];
};

export type NewScheduledWorkout = {
  workoutId: Scalars['Int'];
  scheduledDateTime: Scalars['DateTime'];
};

export type RefreshTokenInput = {
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
export type ResolversTypes = ResolversObject<{
  String: ResolverTypeWrapper<DeepPartial<Scalars['String']>>;
  Boolean: ResolverTypeWrapper<DeepPartial<Scalars['Boolean']>>;
  DateTime: ResolverTypeWrapper<DeepPartial<Scalars['DateTime']>>;
  Query: ResolverTypeWrapper<{}>;
  Int: ResolverTypeWrapper<DeepPartial<Scalars['Int']>>;
  Mutation: ResolverTypeWrapper<{}>;
  RefreshTokenResponse: ResolverTypeWrapper<DeepPartial<RefreshTokenResponse>>;
  RegisterUserResponse: ResolverTypeWrapper<DeepPartial<RegisterUserResponse>>;
  UserLoginResponse: ResolverTypeWrapper<DeepPartial<UserLoginResponse>>;
  CreateScheduledWorkoutResponse: ResolverTypeWrapper<DeepPartial<CreateScheduledWorkoutResponse>>;
  StartScheduledWorkoutResponse: ResolverTypeWrapper<DeepPartial<StartScheduledWorkoutResponse>>;
  User: ResolverTypeWrapper<DeepPartial<User>>;
  UserRole: ResolverTypeWrapper<DeepPartial<UserRole>>;
  WorkoutInvitation: ResolverTypeWrapper<DeepPartial<WorkoutInvitation>>;
  Workout: ResolverTypeWrapper<DeepPartial<Workout>>;
  ExerciseGroup: ResolverTypeWrapper<DeepPartial<ExerciseGroup>>;
  ScheduledWorkout: ResolverTypeWrapper<DeepPartial<ScheduledWorkout>>;
  Exercise: ResolverTypeWrapper<DeepPartial<Exercise>>;
  Muscle: ResolverTypeWrapper<DeepPartial<Muscle>>;
  ExerciseStep: ResolverTypeWrapper<DeepPartial<ExerciseStep>>;
  Equipment: ResolverTypeWrapper<DeepPartial<Equipment>>;
  ExerciseCategory: ResolverTypeWrapper<DeepPartial<ExerciseCategory>>;
  RegisterUser: ResolverTypeWrapper<DeepPartial<RegisterUser>>;
  UserLogin: ResolverTypeWrapper<DeepPartial<UserLogin>>;
  NewScheduledWorkout: ResolverTypeWrapper<DeepPartial<NewScheduledWorkout>>;
  RefreshTokenInput: ResolverTypeWrapper<DeepPartial<RefreshTokenInput>>;
}>;

/** Mapping between all available schema types and the resolvers parents */
export type ResolversParentTypes = ResolversObject<{
  String: DeepPartial<Scalars['String']>;
  Boolean: DeepPartial<Scalars['Boolean']>;
  DateTime: DeepPartial<Scalars['DateTime']>;
  Query: {};
  Int: DeepPartial<Scalars['Int']>;
  Mutation: {};
  RefreshTokenResponse: DeepPartial<RefreshTokenResponse>;
  RegisterUserResponse: DeepPartial<RegisterUserResponse>;
  UserLoginResponse: DeepPartial<UserLoginResponse>;
  CreateScheduledWorkoutResponse: DeepPartial<CreateScheduledWorkoutResponse>;
  StartScheduledWorkoutResponse: DeepPartial<StartScheduledWorkoutResponse>;
  User: DeepPartial<User>;
  UserRole: DeepPartial<UserRole>;
  WorkoutInvitation: DeepPartial<WorkoutInvitation>;
  Workout: DeepPartial<Workout>;
  ExerciseGroup: DeepPartial<ExerciseGroup>;
  ScheduledWorkout: DeepPartial<ScheduledWorkout>;
  Exercise: DeepPartial<Exercise>;
  Muscle: DeepPartial<Muscle>;
  ExerciseStep: DeepPartial<ExerciseStep>;
  Equipment: DeepPartial<Equipment>;
  ExerciseCategory: DeepPartial<ExerciseCategory>;
  RegisterUser: DeepPartial<RegisterUser>;
  UserLogin: DeepPartial<UserLogin>;
  NewScheduledWorkout: DeepPartial<NewScheduledWorkout>;
  RefreshTokenInput: DeepPartial<RefreshTokenInput>;
}>;

export type DateFormatDirectiveArgs = {
  defaultFormat?: Maybe<Scalars['String']>;
  defaultTimeZone?: Maybe<Scalars['String']>;
};

export type DateFormatDirectiveResolver<
  Result,
  Parent,
  ContextType = any,
  Args = DateFormatDirectiveArgs
> = DirectiveResolverFn<Result, Parent, ContextType, Args>;

export interface DateTimeScalarConfig extends GraphQLScalarTypeConfig<ResolversTypes['DateTime'], any> {
  name: 'DateTime';
}

export type QueryResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Query'] = ResolversParentTypes['Query']
> = ResolversObject<{
  test?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  user?: Resolver<Maybe<ResolversTypes['User']>, ParentType, ContextType, RequireFields<QueryUserArgs, 'id'>>;
  users?: Resolver<Array<Maybe<ResolversTypes['User']>>, ParentType, ContextType>;
  exercise?: Resolver<
    Maybe<ResolversTypes['Exercise']>,
    ParentType,
    ContextType,
    RequireFields<QueryExerciseArgs, 'id'>
  >;
  exercises?: Resolver<Array<ResolversTypes['Exercise']>, ParentType, ContextType>;
  exerciseCategory?: Resolver<
    Maybe<ResolversTypes['ExerciseCategory']>,
    ParentType,
    ContextType,
    RequireFields<QueryExerciseCategoryArgs, 'id'>
  >;
  exerciseCategories?: Resolver<Array<ResolversTypes['ExerciseCategory']>, ParentType, ContextType>;
  muscle?: Resolver<Maybe<ResolversTypes['Muscle']>, ParentType, ContextType, RequireFields<QueryMuscleArgs, 'id'>>;
  muscles?: Resolver<Array<ResolversTypes['Muscle']>, ParentType, ContextType>;
  equipment?: Resolver<
    Maybe<ResolversTypes['Equipment']>,
    ParentType,
    ContextType,
    RequireFields<QueryEquipmentArgs, 'id'>
  >;
  allEquipment?: Resolver<Array<ResolversTypes['Equipment']>, ParentType, ContextType>;
  workout?: Resolver<Maybe<ResolversTypes['Workout']>, ParentType, ContextType, RequireFields<QueryWorkoutArgs, 'id'>>;
  workouts?: Resolver<Array<ResolversTypes['Workout']>, ParentType, ContextType>;
  me?: Resolver<Maybe<ResolversTypes['User']>, ParentType, ContextType>;
}>;

export type MutationResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Mutation'] = ResolversParentTypes['Mutation']
> = ResolversObject<{
  registerUser?: Resolver<
    ResolversTypes['RegisterUserResponse'],
    ParentType,
    ContextType,
    RequireFields<MutationRegisterUserArgs, 'user'>
  >;
  login?: Resolver<
    ResolversTypes['UserLoginResponse'],
    ParentType,
    ContextType,
    RequireFields<MutationLoginArgs, 'userCredentials'>
  >;
  refreshToken?: Resolver<
    ResolversTypes['RefreshTokenResponse'],
    ParentType,
    ContextType,
    RequireFields<MutationRefreshTokenArgs, 'input'>
  >;
  createScheduledWorkout?: Resolver<
    ResolversTypes['CreateScheduledWorkoutResponse'],
    ParentType,
    ContextType,
    RequireFields<MutationCreateScheduledWorkoutArgs, 'newWorkout'>
  >;
  startScheduledWorkout?: Resolver<
    ResolversTypes['StartScheduledWorkoutResponse'],
    ParentType,
    ContextType,
    RequireFields<MutationStartScheduledWorkoutArgs, 'id'>
  >;
}>;

export type RefreshTokenResponseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['RefreshTokenResponse'] = ResolversParentTypes['RefreshTokenResponse']
> = ResolversObject<{
  token?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  refreshToken?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type RegisterUserResponseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['RegisterUserResponse'] = ResolversParentTypes['RegisterUserResponse']
> = ResolversObject<{
  user?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type UserLoginResponseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['UserLoginResponse'] = ResolversParentTypes['UserLoginResponse']
> = ResolversObject<{
  token?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  refreshToken?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  user?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type CreateScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['CreateScheduledWorkoutResponse'] = ResolversParentTypes['CreateScheduledWorkoutResponse']
> = ResolversObject<{
  scheduledWorkout?: Resolver<ResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type StartScheduledWorkoutResponseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['StartScheduledWorkoutResponse'] = ResolversParentTypes['StartScheduledWorkoutResponse']
> = ResolversObject<{
  scheduledWorkout?: Resolver<ResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type UserResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['User'] = ResolversParentTypes['User']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  userName?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  firstName?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  lastName?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  email?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  created?: Resolver<ResolversTypes['DateTime'], ParentType, ContextType, RequireFields<UserCreatedArgs, never>>;
  roles?: Resolver<Maybe<Array<Maybe<ResolversTypes['UserRole']>>>, ParentType, ContextType>;
  createdWorkouts?: Resolver<Maybe<Array<Maybe<ResolversTypes['Workout']>>>, ParentType, ContextType>;
  scheduledWorkouts?: Resolver<Maybe<Array<Maybe<ResolversTypes['ScheduledWorkout']>>>, ParentType, ContextType>;
  ownedScheduledWorkouts?: Resolver<Maybe<Array<Maybe<ResolversTypes['ScheduledWorkout']>>>, ParentType, ContextType>;
  favoriteExercises?: Resolver<Maybe<Array<Maybe<ResolversTypes['Exercise']>>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type UserRoleResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['UserRole'] = ResolversParentTypes['UserRole']
> = ResolversObject<{
  name?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type WorkoutInvitationResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['WorkoutInvitation'] = ResolversParentTypes['WorkoutInvitation']
> = ResolversObject<{
  inviter?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  invitee?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  scheduledWorkout?: Resolver<ResolversTypes['ScheduledWorkout'], ParentType, ContextType>;
  accepted?: Resolver<ResolversTypes['Boolean'], ParentType, ContextType>;
  declined?: Resolver<ResolversTypes['Boolean'], ParentType, ContextType>;
  status?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  respondedAtDateTime?: Resolver<
    Maybe<ResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<WorkoutInvitationRespondedAtDateTimeArgs, never>
  >;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type WorkoutResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Workout'] = ResolversParentTypes['Workout']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  label?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  description?: Resolver<Maybe<ResolversTypes['String']>, ParentType, ContextType>;
  createdByUserId?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  createdByUser?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  createdOnDate?: Resolver<
    ResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<WorkoutCreatedOnDateArgs, never>
  >;
  lastModifiedDate?: Resolver<
    ResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<WorkoutLastModifiedDateArgs, never>
  >;
  shareable?: Resolver<ResolversTypes['Boolean'], ParentType, ContextType>;
  exerciseGroups?: Resolver<Maybe<Array<Maybe<ResolversTypes['ExerciseGroup']>>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type ExerciseGroupResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['ExerciseGroup'] = ResolversParentTypes['ExerciseGroup']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  exercise?: Resolver<ResolversTypes['Exercise'], ParentType, ContextType>;
  sets?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  repetitions?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type ScheduledWorkoutResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['ScheduledWorkout'] = ResolversParentTypes['ScheduledWorkout']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  scheduledByUser?: Resolver<ResolversTypes['User'], ParentType, ContextType>;
  workout?: Resolver<ResolversTypes['Workout'], ParentType, ContextType>;
  startedDateTime?: Resolver<
    Maybe<ResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<ScheduledWorkoutStartedDateTimeArgs, never>
  >;
  completedDateTime?: Resolver<
    Maybe<ResolversTypes['DateTime']>,
    ParentType,
    ContextType,
    RequireFields<ScheduledWorkoutCompletedDateTimeArgs, never>
  >;
  scheduledDateTime?: Resolver<
    ResolversTypes['DateTime'],
    ParentType,
    ContextType,
    RequireFields<ScheduledWorkoutScheduledDateTimeArgs, never>
  >;
  customWorkout?: Resolver<Maybe<ResolversTypes['String']>, ParentType, ContextType>;
  adHocExercises?: Resolver<Maybe<Array<Maybe<ResolversTypes['ExerciseGroup']>>>, ParentType, ContextType>;
  attendees?: Resolver<Maybe<Array<Maybe<ResolversTypes['User']>>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type ExerciseResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Exercise'] = ResolversParentTypes['Exercise']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  primaryMuscle?: Resolver<Maybe<ResolversTypes['Muscle']>, ParentType, ContextType>;
  secondaryMuscle?: Resolver<Maybe<ResolversTypes['Muscle']>, ParentType, ContextType>;
  exerciseSteps?: Resolver<Array<ResolversTypes['ExerciseStep']>, ParentType, ContextType>;
  equipment?: Resolver<Array<ResolversTypes['Equipment']>, ParentType, ContextType>;
  exerciseCategorys?: Resolver<Array<ResolversTypes['ExerciseCategory']>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type MuscleResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Muscle'] = ResolversParentTypes['Muscle']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  description?: Resolver<Maybe<ResolversTypes['String']>, ParentType, ContextType>;
  primaryExercises?: Resolver<Array<ResolversTypes['Exercise']>, ParentType, ContextType>;
  secondaryExercises?: Resolver<Maybe<Array<Maybe<ResolversTypes['Exercise']>>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type ExerciseStepResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['ExerciseStep'] = ResolversParentTypes['ExerciseStep']
> = ResolversObject<{
  exerciseStepNumber?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  description?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type EquipmentResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['Equipment'] = ResolversParentTypes['Equipment']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<Array<Maybe<ResolversTypes['Exercise']>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type ExerciseCategoryResolvers<
  ContextType = any,
  ParentType extends ResolversParentTypes['ExerciseCategory'] = ResolversParentTypes['ExerciseCategory']
> = ResolversObject<{
  id?: Resolver<ResolversTypes['Int'], ParentType, ContextType>;
  name?: Resolver<ResolversTypes['String'], ParentType, ContextType>;
  exercises?: Resolver<Maybe<Array<Maybe<ResolversTypes['Exercise']>>>, ParentType, ContextType>;
  __isTypeOf?: isTypeOfResolverFn<ParentType>;
}>;

export type Resolvers<ContextType = any> = ResolversObject<{
  DateTime?: GraphQLScalarType;
  Query?: QueryResolvers<ContextType>;
  Mutation?: MutationResolvers<ContextType>;
  RefreshTokenResponse?: RefreshTokenResponseResolvers<ContextType>;
  RegisterUserResponse?: RegisterUserResponseResolvers<ContextType>;
  UserLoginResponse?: UserLoginResponseResolvers<ContextType>;
  CreateScheduledWorkoutResponse?: CreateScheduledWorkoutResponseResolvers<ContextType>;
  StartScheduledWorkoutResponse?: StartScheduledWorkoutResponseResolvers<ContextType>;
  User?: UserResolvers<ContextType>;
  UserRole?: UserRoleResolvers<ContextType>;
  WorkoutInvitation?: WorkoutInvitationResolvers<ContextType>;
  Workout?: WorkoutResolvers<ContextType>;
  ExerciseGroup?: ExerciseGroupResolvers<ContextType>;
  ScheduledWorkout?: ScheduledWorkoutResolvers<ContextType>;
  Exercise?: ExerciseResolvers<ContextType>;
  Muscle?: MuscleResolvers<ContextType>;
  ExerciseStep?: ExerciseStepResolvers<ContextType>;
  Equipment?: EquipmentResolvers<ContextType>;
  ExerciseCategory?: ExerciseCategoryResolvers<ContextType>;
}>;

/**
 * @deprecated
 * Use "Resolvers" root object instead. If you wish to get "IResolvers", add "typesPrefix: I" to your config.
 */
export type IResolvers<ContextType = any> = Resolvers<ContextType>;
export type DirectiveResolvers<ContextType = any> = ResolversObject<{
  dateFormat?: DateFormatDirectiveResolver<any, any, ContextType>;
}>;

/**
 * @deprecated
 * Use "DirectiveResolvers" root object instead. If you wish to get "IDirectiveResolvers", add "typesPrefix: I" to your config.
 */
export type IDirectiveResolvers<ContextType = any> = DirectiveResolvers<ContextType>;
