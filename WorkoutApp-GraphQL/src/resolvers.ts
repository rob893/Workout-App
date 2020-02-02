import { IResolvers } from "apollo-server";
import { IUserAPI } from "./interfaces/IUserAPI";
import { UserToRegister, UserLogin, User, UserLoginResponse } from "./entities/User";

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