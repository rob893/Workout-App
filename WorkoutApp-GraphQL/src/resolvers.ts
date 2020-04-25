import { UserToRegister, UserLogin, User, UserLoginResponse } from './models/workout-api/User';
import { ExerciseDetailed, Muscle, Equipment, ExerciseCategory } from './models/workout-api/Exercise';
import { WorkoutAppContext } from './models/WorkoutAppContext';
import { GraphQLScalarType, Kind } from 'graphql';
import { Resolvers } from './models/schema';

export const resolvers: Resolvers<WorkoutAppContext> = {
  DateTime: new GraphQLScalarType({
    name: 'DateTime',
    description: 'Scalar type that represents a DateTime object',
    serialize(value: string | number | Date) {
      if (typeof value !== 'string' && typeof value !== 'number' && !(value instanceof Date)) {
        throw new Error(`Expected value to be either a string, number, or Date. Got ${typeof value} instead.`);
      }

      if (typeof value === 'number' || value instanceof Date) {
        return new Date(value).toISOString();
      }

      return value;
    },
    parseValue(value: string | number | Date) {
      if (typeof value !== 'string' && typeof value !== 'number' && !(value instanceof Date)) {
        throw new Error(`Expected value to be either a string, number, or Date. Got ${typeof value} instead.`);
      }

      return new Date(value);
    },
    parseLiteral(ast) {
      switch (ast.kind) {
        case Kind.INT:
          return new Date(ast.value);
        case Kind.STRING:
          return new Date(ast.value);
      }

      return null;
    }
  }),
  Query: {
    test() {
      return 'Hello World and stuff!';
    },

    users(_root, _args, { dataSources: { userAPI } }): Promise<User[]> {
      return userAPI.getAllUsers();
    },

    user(_root, { id }, { dataSources: { userAPI } }): Promise<User | null> {
      return userAPI.getUserById(id);
    },

    exercises(_root, _args, { dataSources: { exerciseAPI } }): Promise<ExerciseDetailed[]> {
      return exerciseAPI.getExercises();
    },

    exercise(_root, { id }, { dataSources: { exerciseAPI } }): Promise<ExerciseDetailed | null> {
      return exerciseAPI.getExerciseById(id);
    },

    muscles(_root, _args, { dataSources: { muscleAPI } }): Promise<Muscle[]> {
      return muscleAPI.getMuscles();
    },

    muscle(_root, { id }, { dataSources: { muscleAPI } }): Promise<Muscle | null> {
      return muscleAPI.getMuscleById(id);
    },

    allEquipment(_root, _args, { dataSources: { equipmentAPI } }): Promise<Equipment[]> {
      return equipmentAPI.getAllEquipment();
    },

    equipment(_root, { id }, { dataSources: { equipmentAPI } }): Promise<Equipment | null> {
      return equipmentAPI.getEquipmentById(id);
    },

    workouts(_root, _args, { dataSources: { workoutAPI } }): Promise<any[]> {
      return workoutAPI.getWorkoutsDetailed();
    },

    workout(_root, { id }, { dataSources: { workoutAPI } }): Promise<any> {
      return workoutAPI.getWorkoutDetailed(id);
    },

    me(_root, _args, { claims: { nameid }, dataSources: { userAPI } }): Promise<User | null> {
      return nameid ? userAPI.getUserById(parseInt(nameid)) : Promise.resolve(null);
    }
  },
  Mutation: {
    async registerUser(
      root,
      { user }: { user: UserToRegister },
      { dataSources: { userAPI } }
    ): Promise<{ user: User }> {
      const newUser = await userAPI.registerUser(user);
      return { user: newUser };
    },

    login(root, { userCredentials }, { dataSources: { userAPI } }): Promise<UserLoginResponse> {
      return userAPI.login(userCredentials);
    },

    refreshToken(root, { input }, { dataSources: { userAPI } }) {
      return userAPI.refreshToken(input);
    },

    async createScheduledWorkout(root, { newWorkout }, { dataSources: { workoutAPI } }) {
      const createdWorkout = await workoutAPI.createScheduledWorkout(newWorkout);
      return { scheduledWorkout: createdWorkout };
    },

    async startScheduledWorkout(root, { id }, { dataSources: { workoutAPI } }) {
      const workout = await workoutAPI.startScheduledWorkout(id);
      return { scheduledWorkout: workout };
    }
  },
  User: {
    scheduledWorkouts(user, args, { dataSources: { userAPI } }) {
      if (!user.id) {
        throw new Error();
      }
      return userAPI.getScheduledWorkoutsForUser(user.id);
    }
  },
  ExerciseGroup: {
    async exercise(root, args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercise = exercises.find(ex => ex.id === root.exercise?.id);

      if (!exercise) {
        throw new Error('No exercise found for exercise group');
      }

      return exercise;
    }
  },
  Muscle: {
    async primaryExercises(root, args, { dataSources: { exerciseAPI } }): Promise<ExerciseDetailed[]> {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForMuscle = exercises.filter(
        exercise => exercise.primaryMuscle && exercise.primaryMuscle.id === root.id
      );

      return exercisesForMuscle;
    },

    async secondaryExercises(root, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForMuscle = exercises.filter(
        exercise => exercise.secondaryMuscle && exercise.secondaryMuscle.id === root.id
      );

      return exercisesForMuscle;
    }
  },
  Equipment: {
    async exercises(root, _args, { dataSources: { exerciseAPI } }): Promise<ExerciseDetailed[]> {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForEquipment = exercises.filter(exercise =>
        exercise.equipment.some(equipment => equipment.id === root.id)
      );

      return exercisesForEquipment;
    }
  },
  ExerciseCategory: {
    async exercises(root, args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForCategory = exercises.filter(exercise =>
        exercise.exerciseCategorys.some(category => category.id === root.id)
      );

      return exercisesForCategory;
    }
  }
};
