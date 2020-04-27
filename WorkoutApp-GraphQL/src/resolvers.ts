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

    exerciseCategory(_root, { id }, { dataSources: { exerciseCategoryAPI } }) {
      return exerciseCategoryAPI.getExerciseCategory(id);
    },

    exerciseCategories(_root, _args, { dataSources: { exerciseCategoryAPI } }) {
      return exerciseCategoryAPI.getExerciseCategories();
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
    scheduledWorkouts({ id }, _args, { dataSources: { userAPI } }) {
      return userAPI.getScheduledWorkoutsForUser(id);
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
    primaryExercises({ id }, _args, { dataSources: { muscleAPI } }) {
      return muscleAPI.getPrimaryExercisesForMuscle(id);
    },

    secondaryExercises({ id }, _args, { dataSources: { muscleAPI } }) {
      return muscleAPI.getSecondaryExercisesForMuscle(id);
    }
  },
  Equipment: {
    exercises({ id }, _args, { dataSources: { equipmentAPI } }) {
      return equipmentAPI.getExercisesForEquipment(id);
    }
  },
  ExerciseCategory: {
    exercises({ id }, _args, { dataSources: { exerciseCategoryAPI } }) {
      return exerciseCategoryAPI.getExercisesForExerciseCategory(id);
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

    attendees({ id }, _args, { dataSources: { workoutAPI } }) {
      return workoutAPI.getScheduledWorkoutAttendees(id);
    },

    async scheduledByUser({ scheduledByUserId }, _args, { dataSources: { userAPI } }) {
      const scheduledBy = await userAPI.getUserById(scheduledByUserId);

      if (scheduledBy === null) {
        throw new Error(`No user found for id ${scheduledByUserId}`);
      }

      return scheduledBy;
    },

    adHocExercises({ id }, _args, { dataSources: { workoutAPI } }) {
      return workoutAPI.getScheduledWorkoutAdHocExercises(id);
    }
  },
  MutationResponse: {
    __resolveType() {
      return null;
    }
  }
};
