import { IResolvers } from "apollo-server";
import { IUserAPI } from "./interfaces/IUserAPI";
import { UserToRegister, UserLogin, User, UserLoginResponse } from "./entities/User";
import { Exercise } from "./entities/Exercise";
import { ExerciseAPI } from "./datasources/ExerciseAPI";

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
    }
}