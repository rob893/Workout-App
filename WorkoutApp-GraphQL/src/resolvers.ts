import { WorkoutAppContext } from './models/common';
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
    users(_root, { pagination }, { dataSources: { userAPI } }) {
      const queryParams = { ...pagination };
      return userAPI.getAllUsers(queryParams);
    },

    user(_root, { id }, { dataSources: { userAPI } }) {
      return userAPI.getUserById(id);
    },

    exercises(_root, { pagination }, { dataSources: { exerciseAPI } }) {
      const queryParams = { ...pagination };
      return exerciseAPI.getExercises(queryParams);
    },

    exercise(_root, { id }, { dataSources: { exerciseAPI } }) {
      return exerciseAPI.getExerciseById(id);
    },

    exerciseCategory(_root, { id }, { dataSources: { exerciseCategoryAPI } }) {
      return exerciseCategoryAPI.getExerciseCategory(id);
    },

    exerciseCategories(_root, { pagination }, { dataSources: { exerciseCategoryAPI } }) {
      const queryParams = { ...pagination };
      return exerciseCategoryAPI.getExerciseCategories(queryParams);
    },

    muscles(_root, { pagination }, { dataSources: { muscleAPI } }) {
      const queryParams = { ...pagination };
      return muscleAPI.getMuscles(queryParams);
    },

    muscle(_root, { id }, { dataSources: { muscleAPI } }) {
      return muscleAPI.getMuscleById(id);
    },

    allEquipment(_root, { pagination }, { dataSources: { equipmentAPI } }) {
      const queryParams = { ...pagination };
      return equipmentAPI.getAllEquipment(queryParams);
    },

    equipment(_root, { id }, { dataSources: { equipmentAPI } }) {
      return equipmentAPI.getEquipmentById(id);
    },

    workouts(_root, { pagination }, { dataSources: { workoutAPI } }) {
      const queryParams = { ...pagination };
      return workoutAPI.getWorkoutsDetailed(queryParams);
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
    scheduledWorkouts({ id }, { pagination }, { dataSources: { userAPI } }) {
      const queryParams = { ...pagination };
      return userAPI.getScheduledWorkoutsForUser(id, queryParams);
    },

    sentWorkoutInvitations({ id }, { filter, pagination }, { dataSources: { userAPI } }) {
      const params = {
        ...filter,
        ...pagination
      };
      return userAPI.getSentWorkoutInvitationsForUser(id, params);
    },

    receivedWorkoutInvitations({ id }, { filter }, { dataSources: { userAPI } }) {
      return userAPI.getReceivedWorkoutInvitationsForUser(id, filter || {});
    }
  },
  Exercise: {
    equipment({ id }, { pagination }, { dataSources: { exerciseAPI } }) {
      const queryParams = { ...pagination };
      return exerciseAPI.getEquipmentForExercise(id, queryParams);
    },
    exerciseCategorys({ id }, { pagination }, { dataSources: { exerciseAPI } }) {
      const queryParams = { ...pagination };
      return exerciseAPI.getExerciseCategoriesForExercise(id, queryParams);
    },
    primaryMuscle({ primaryMuscleId }, _args, { dataSources: { muscleAPI } }) {
      if (!primaryMuscleId) {
        return null;
      }

      return muscleAPI.getMuscleById(primaryMuscleId);
    },
    secondaryMuscle({ secondaryMuscleId }, _args, { dataSources: { muscleAPI } }) {
      if (!secondaryMuscleId) {
        return null;
      }

      return muscleAPI.getMuscleById(secondaryMuscleId);
    }
  },
  ExerciseGroup: {
    async exercise({ exercise: { id } }, _args, { dataSources: { exerciseAPI } }) {
      const exercise = await exerciseAPI.getExerciseById(id);

      if (!exercise) {
        throw new Error(`No exercise found with id ${id}`);
      }

      return exercise;
    }
  },
  Muscle: {
    primaryExercises({ id }, { pagination }, { dataSources: { muscleAPI } }) {
      const queryParams = { ...pagination };
      return muscleAPI.getPrimaryExercisesForMuscle(id, queryParams);
    },

    secondaryExercises({ id }, { pagination }, { dataSources: { muscleAPI } }) {
      const queryParams = { ...pagination };
      return muscleAPI.getSecondaryExercisesForMuscle(id, queryParams);
    }
  },
  Equipment: {
    exercises({ id }, { pagination }, { dataSources: { equipmentAPI } }) {
      const queryParams = { ...pagination };
      return equipmentAPI.getExercisesForEquipment(id, queryParams);
    }
  },
  ExerciseCategory: {
    exercises({ id }, { pagination }, { dataSources: { exerciseCategoryAPI } }) {
      const queryParams = { ...pagination };
      return exerciseCategoryAPI.getExercisesForExerciseCategory(id, queryParams);
    }
  },
  WorkoutInvitation: {
    async invitee({ inviteeId }, _args, { dataSources: { userAPI } }) {
      const invitee = await userAPI.getUserById(inviteeId);

      if (invitee === null) {
        throw new Error(`No user found for id ${inviteeId}`);
      }

      return invitee;
    },

    async inviter({ inviterId }, _args, { dataSources: { userAPI } }) {
      const inviter = await userAPI.getUserById(inviterId);

      if (inviter === null) {
        throw new Error(`No user found for id ${inviterId}`);
      }

      return inviter;
    },

    async scheduledWorkout({ scheduledWorkoutId }, _args, { dataSources: { workoutAPI } }) {
      const workout = await workoutAPI.getScheduledWorkout(scheduledWorkoutId);

      if (workout === null) {
        throw new Error(`No scheduled workout found for id ${scheduledWorkoutId}`);
      }

      return workout;
    }
  },
  ScheduledWorkout: {
    async workout({ workoutId }, _args, { dataSources: { workoutAPI } }) {
      const workout = await workoutAPI.getWorkoutDetailed(workoutId);

      if (workout === null) {
        throw new Error(`No workout found for id ${workoutId}`);
      }

      return workout;
    },

    attendees({ id }, { pagination }, { dataSources: { workoutAPI } }) {
      const queryParams = { ...pagination };
      return workoutAPI.getScheduledWorkoutAttendees(id, queryParams);
    },

    async scheduledByUser({ scheduledByUserId }, _args, { dataSources: { userAPI } }) {
      const scheduledBy = await userAPI.getUserById(scheduledByUserId);

      if (scheduledBy === null) {
        throw new Error(`No user found for id ${scheduledByUserId}`);
      }

      return scheduledBy;
    },

    adHocExercises({ id }, { pagination }, { dataSources: { workoutAPI } }) {
      const queryParams = { ...pagination };
      return workoutAPI.getScheduledWorkoutAdHocExercises(id, queryParams);
    }
  },
  MutationResponse: {
    __resolveType() {
      return null;
    }
  },
  Node: {
    __resolveType() {
      return null;
    }
  },
  Edge: {
    __resolveType() {
      return null;
    }
  },
  CursorPaginatedResponse: {
    __resolveType() {
      return null;
    }
  }
};
