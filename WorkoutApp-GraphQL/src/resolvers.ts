import { IResolvers } from 'apollo-server';
import { UserToRegister, UserLogin, User, UserLoginResponse } from './entities/User';
import { Exercise, Muscle, Equipment, ExerciseCategory } from './entities/Exercise';
import { WorkoutAppContext } from './entities/WorkoutAppContext';
import { GraphQLScalarType, Kind } from 'graphql';

export const resolvers: IResolvers<any, WorkoutAppContext> = {
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

        users(root, args, { dataSources: { userAPI } }): Promise<User[]> {
            return userAPI.getAllUsers();
        },

        user(root, { id }, { dataSources: { userAPI } }): Promise<User | null> {
            return userAPI.getUserById(id);
        },

        exercises(root, args, { dataSources: { exerciseAPI } }): Promise<Exercise[]> {
            return exerciseAPI.getExercises();
        },

        exercise(root, { id }, { dataSources: { exerciseAPI } }): Promise<Exercise | null> {
            return exerciseAPI.getExerciseById(id);
        },

        muscles(root, args, { dataSources: { muscleAPI } }): Promise<Muscle[]> {
            return muscleAPI.getMuscles();
        },

        muscle(root, { id }, { dataSources: { muscleAPI } }): Promise<Muscle | null> {
            return muscleAPI.getMuscleById(id);
        },

        allEquipment(root, args, { dataSources: { equipmentAPI } }): Promise<Equipment[]> {
            return equipmentAPI.getAllEquipment();
        },

        equipment(root, { id }, { dataSources: { equipmentAPI } }): Promise<Equipment | null> {
            return equipmentAPI.getEquipmentById(id);
        },

        workouts(root, args, { dataSources: { workoutAPI } }): Promise<any[]> {
            return workoutAPI.getWorkoutsDetailed();
        },

        workout(root, { id }, { dataSources: { workoutAPI } }): Promise<any[]> {
            return workoutAPI.getWorkoutDetailed(id);
        },

        me(root, args, { claims: { nameid }, dataSources: { userAPI } }): Promise<User | null> {
            return nameid ? userAPI.getUserById(nameid) : Promise.resolve(null);
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

        login(
            root,
            { userCredentials }: { userCredentials: UserLogin },
            { dataSources: { userAPI } }
        ): Promise<UserLoginResponse> {
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
        scheduledWorkouts(user: User, args, { dataSources: { userAPI } }) {
            return userAPI.getScheduledWorkoutsForUser(user.id);
        }
    },
    ExerciseGroup: {
        async exercise(root, args, { dataSources: { exerciseAPI } }) {
            const exercises = await exerciseAPI.getExercises();

            return exercises.find(ex => ex.id === root.exercise.id);
        }
    },
    Muscle: {
        async primaryExercises(root: Muscle, args, { dataSources: { exerciseAPI } }): Promise<Exercise[]> {
            const exercises = await exerciseAPI.getExercises();

            const exercisesForMuscle = exercises.filter(
                exercise => exercise.primaryMuscle && exercise.primaryMuscle.id === root.id
            );

            return exercisesForMuscle;
        },

        async secondaryExercises(root: Muscle, args, { dataSources: { exerciseAPI } }): Promise<Exercise[]> {
            const exercises = await exerciseAPI.getExercises();

            const exercisesForMuscle = exercises.filter(
                exercise => exercise.secondaryMuscle && exercise.secondaryMuscle.id === root.id
            );

            return exercisesForMuscle;
        }
    },
    Equipment: {
        async exercises(root: Equipment, args, { dataSources: { exerciseAPI } }): Promise<Exercise[]> {
            const exercises = await exerciseAPI.getExercises();

            const exercisesForEquipment = exercises.filter(exercise =>
                exercise.equipment.some(equipment => equipment.id === root.id)
            );

            return exercisesForEquipment;
        }
    },
    ExerciseCategory: {
        async exercises(root: ExerciseCategory, args, { dataSources: { exerciseAPI } }): Promise<Exercise[]> {
            const exercises = await exerciseAPI.getExercises();

            const exercisesForCategory = exercises.filter(exercise =>
                exercise.exerciseCategorys.some(category => category.id === root.id)
            );

            return exercisesForCategory;
        }
    }
};
