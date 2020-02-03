import { IResolvers } from "apollo-server";
import { IUserAPI } from "./interfaces/IUserAPI";
import { UserToRegister, UserLogin, User, UserLoginResponse } from "./entities/User";
import { Exercise, Muscle, Equipment, ExerciseCategory } from "./entities/Exercise";
import { ExerciseAPI } from "./datasources/ExerciseAPI";
import { MuscleAPI } from "./datasources/MuscleAPI";
import { EquipmentAPI } from "./datasources/EquipmentAPI";

export const resolvers: IResolvers = {
    Query: {
        test() {
            return 'Hello World and stuff!';
        },

        async users(root, args, { dataSources }): Promise<User[]> {
            const userAPI: IUserAPI = dataSources.userAPI;

            const allUsers = await userAPI.getAllUsers();

            return allUsers;
        },

        async user(root, { id }, { dataSources }): Promise<User | null> {
            const userAPI: IUserAPI = dataSources.userAPI;

            const user = await userAPI.getUserById(id);

            return user;
        },

        async exercises(root, args, { dataSources }): Promise<Exercise[]> {
            const exerciseAPI: ExerciseAPI = dataSources.exerciseAPI;

            const exercises = await exerciseAPI.getExercises();

            return exercises;
        },

        async exercise(root, { id }, { dataSources }): Promise<Exercise | null> {
            const exerciseAPI: ExerciseAPI = dataSources.exerciseAPI;

            const exercise = await exerciseAPI.getExerciseById(id);

            return exercise;
        },

        async muscles(root, args, { dataSources }): Promise<Muscle[]> {
            const muscleAPI: MuscleAPI = dataSources.muscleAPI;

            const muscles = await muscleAPI.getMuscles();

            return muscles;
        },

        async muscle(root, { id }, { dataSources }): Promise<Muscle | null> {
            const muscleAPI: MuscleAPI = dataSources.muscleAPI;

            const muscle = await muscleAPI.getMuscleById(id);

            return muscle;
        },

        async allEquipment(root, args, { dataSources }): Promise<Equipment[]> {
            const equipmentAPI: EquipmentAPI = dataSources.equipmentAPI;

            const equipment = await equipmentAPI.getAllEquipment();

            return equipment;
        },

        async equipment(root, { id }, { dataSources }): Promise<Equipment | null> {
            const equipmentAPI: EquipmentAPI = dataSources.equipmentAPI;

            const equipment = await equipmentAPI.getEquipmentById(id);

            return equipment;
        }
    },
    Mutation: {
        async registerUser(root, { user }: { user: UserToRegister }, { dataSources }): Promise<User> {
            const userAPI: IUserAPI = dataSources.userAPI;

            const createdUser = await userAPI.registerUser(user);

            return createdUser;
        },

        async login(root, { userCredentials }: { userCredentials: UserLogin }, { dataSources }): Promise<UserLoginResponse> {
            const userAPI: IUserAPI = dataSources.userAPI;

            const loginRes = await userAPI.login(userCredentials);

            return loginRes;
        }
    },
    Muscle: {
        async exercises(root: Muscle, args, { dataSources }): Promise<Exercise[]> {
            const exerciseAPI: ExerciseAPI = dataSources.exerciseAPI;

            const exercises = await exerciseAPI.getExercises();

            const exercisesForMuscle = exercises.filter(exercise => {
                if (exercise.secondaryMuscle) {
                    return exercise.primaryMuscle.id === root.id || exercise.secondaryMuscle.id === root.id;
                }

                return exercise.primaryMuscle.id === root.id;
            });

            return exercisesForMuscle;
        }
    },
    Equipment: {
        async exercises(root: Equipment, args, { dataSources }): Promise<Exercise[]> {
            const exerciseAPI: ExerciseAPI = dataSources.exerciseAPI;

            const exercises = await exerciseAPI.getExercises();

            const exercisesForEquipment = exercises.filter(exercise => exercise.equipment.some(equipment => equipment.id === root.id));

            return exercisesForEquipment;
        }
    },
    ExerciseCategory: {
        async exercises(root: ExerciseCategory, args, { dataSources }): Promise<Exercise[]> {
            const exerciseAPI: ExerciseAPI = dataSources.exerciseAPI;

            const exercises = await exerciseAPI.getExercises();

            const exercisesForCategory = exercises.filter(exercise => exercise.exerciseCategorys.some(category => category.id === root.id));

            return exercisesForCategory;
        }
    }
}