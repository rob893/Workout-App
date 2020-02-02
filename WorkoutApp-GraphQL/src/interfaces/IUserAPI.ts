import { User, UserToRegister, UserLogin, UserLoginResponse } from "../entities/User";

export interface IUserAPI {
    getAllUsers(): Promise<User[]>;
    getUserById(id: string): Promise<User | null>;
    registerUser(userToRegister: UserToRegister): Promise<User>;
    login(userLogin: UserLogin): Promise<UserLoginResponse>;
}