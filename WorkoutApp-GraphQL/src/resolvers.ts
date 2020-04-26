import { WorkoutAppContext } from './models/WorkoutAppContext';
import { GraphQLScalarType, Kind } from 'graphql';
import { SchemaResolvers } from './models/schema';

export const resolvers: SchemaResolvers<WorkoutAppContext> = {
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
      return 'Hello World!';
    },

    users(_root, _args, { dataSources: { userAPI } }) {
      return userAPI.getAllUsers();
    },

    user(_root, { id }, { dataSources: { userAPI } }) {
      return userAPI.getUserById(id);
    },

    exercises(_root, _args, { dataSources: { exerciseAPI } }) {
      return exerciseAPI.getExercises();
    },

    exercise(_root, { id }, { dataSources: { exerciseAPI } }) {
      return exerciseAPI.getExerciseById(id);
    },

    muscles(_root, _args, { dataSources: { muscleAPI } }) {
      return muscleAPI.getMuscles();
    },

    muscle(_root, { id }, { dataSources: { muscleAPI } }) {
      return muscleAPI.getMuscleById(id);
    },

    allEquipment(_root, _args, { dataSources: { equipmentAPI } }) {
      return equipmentAPI.getAllEquipment();
    },

    equipment(_root, { id }, { dataSources: { equipmentAPI } }) {
      return equipmentAPI.getEquipmentById(id);
    },

    workouts(_root, _args, { dataSources: { workoutAPI } }) {
      return workoutAPI.getWorkoutsDetailed();
    },

    workout(_root, { id }, { dataSources: { workoutAPI } }) {
      return workoutAPI.getWorkoutDetailed(id);
    },

    me(_root, _args, { claims: { nameid }, dataSources: { userAPI } }) {
      return nameid ? userAPI.getUserById(parseInt(nameid)) : Promise.resolve(null);
    }
  },
  Mutation: {
    async registerUser(_root, { user }, { dataSources: { userAPI } }) {
      const newUser = await userAPI.registerUser(user);
      return {
        success: true,
        user: newUser
      };
    },

    async login(_root, { userCredentials }, { dataSources: { userAPI } }) {
      const res = await userAPI.login(userCredentials);
      return {
        success: true,
        ...res
      };
    },

    async refreshToken(_root, { input }, { dataSources: { userAPI } }) {
      const res = await userAPI.refreshToken(input);
      return {
        success: true,
        ...res
      };
    },

    async createScheduledWorkout(_root, { newWorkout }, { dataSources: { workoutAPI } }) {
      const createdWorkout = await workoutAPI.createScheduledWorkout(newWorkout);
      return {
        success: true,
        scheduledWorkout: createdWorkout
      };
    },

    async startScheduledWorkout(_root, { id }, { dataSources: { workoutAPI } }) {
      const workout = await workoutAPI.startScheduledWorkout(id);
      return {
        success: true,
        scheduledWorkout: workout
      };
    }
  },
  User: {
    scheduledWorkouts(user, _args, { dataSources: { userAPI } }) {
      return userAPI.getScheduledWorkoutsForUser(user.id);
    }
  },
  ExerciseGroup: {
    async exercise({ exercise: { id } }, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercise = exercises.find(ex => ex.id === id);

      if (!exercise) {
        throw new Error('No exercise found for exercise group');
      }

      return exercise;
    }
  },
  Muscle: {
    async primaryExercises({ id }, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForMuscle = exercises.filter(
        exercise => exercise.primaryMuscle && exercise.primaryMuscle.id === id
      );

      return exercisesForMuscle;
    },

    async secondaryExercises({ id }, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForMuscle = exercises.filter(
        exercise => exercise.secondaryMuscle && exercise.secondaryMuscle.id === id
      );

      return exercisesForMuscle;
    }
  },
  Equipment: {
    async exercises({ id }, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForEquipment = exercises.filter(exercise =>
        exercise.equipment.some(equipment => equipment.id === id)
      );

      return exercisesForEquipment;
    }
  },
  ExerciseCategory: {
    async exercises({ id }, _args, { dataSources: { exerciseAPI } }) {
      const exercises = await exerciseAPI.getExercises();

      const exercisesForCategory = exercises.filter(exercise =>
        exercise.exerciseCategorys.some(category => category.id === id)
      );

      return exercisesForCategory;
    }
  }
};
