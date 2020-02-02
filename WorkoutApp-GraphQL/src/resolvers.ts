import { IResolvers } from "apollo-server";
import { UserAPI } from "./datasources/UserAPI";

export const resolvers: IResolvers = {
    Query: {
        test: () => 'Hello world!',
        users: (root, args, { dataSources }) => {
            const userAPI: UserAPI = dataSources.userAPI;

            const allUsers = userAPI.getAllUsers();

            return allUsers;
        },
        user: (root, { id }, { dataSources }) => {
            const userAPI: UserAPI = dataSources.userAPI;

            const user = userAPI.getUserById(id);

            console.log('ho');

            return user;
        }
    },
    Mutation: {
        registerUser: (root, userToRegister: { user: {firstName: string, lastName: string, age: number}}, { dataSources }) => {
            const userAPI: UserAPI = dataSources.userAPI;

            const createdUser = userAPI.createUser(userToRegister.user);

            return createdUser;
        }
    }
}