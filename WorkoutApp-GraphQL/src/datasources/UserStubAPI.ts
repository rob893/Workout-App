import { DataSource } from "apollo-datasource";
import { User, UserToRegister, UserLogin, UserLoginResponse } from "../entities/User";
import { IUserAPI } from "../interfaces/IUserAPI";

const users: User[] = [
    {
        id: '1',
        firstName: 'John',
        lastName: 'Smith',
        userName: 'jSmith',
        email: 'test@gmail.com',
        created: '2019-08-02T00:00:00'
    },
    {
        id: '2',
        firstName: 'Joe',
        lastName: 'Black',
        userName: 'jBlack',
        email: 'test@gmail.com',
        created: '2019-08-02T00:00:00'
    },
    {
        id: '3',
        firstName: 'Jane',
        lastName: 'Doe',
        userName: 'jDoe',
        email: 'test@gmail.com',
        created: '2019-08-02T00:00:00'
    },
    {
        id: '4',
        firstName: 'Matt',
        lastName: 'Albege',
        userName: 'mAlbege',
        email: 'test@gmail.com',
        created: '2019-08-02T00:00:00'
    }
];

export class UserStubAPI extends DataSource implements IUserAPI {

    private readonly users: User[];


    public constructor() {
        super();
        this.users = users;
    }


    public getAllUsers(): Promise<User[]> {
        return Promise.resolve(this.users);
    }

    public getUserById(id: string): Promise<User | null> {
        const user = this.users.find(u => u.id === id);

        if (!user) {
            return Promise.resolve(null);
        }

        return Promise.resolve(user);
    }

    public registerUser(userToCreate: UserToRegister): Promise<User> {
        const nextId = Math.max(...this.users.map(u => Number.parseInt(u.id))) + 1;

        const user: User = {
            id: nextId.toString(),
            firstName: userToCreate.firstName,
            lastName: userToCreate.lastName,
            userName: userToCreate.username,
            email: userToCreate.email,
            created: Date.now.toString()
        }

        this.users.push(user);

        return Promise.resolve(user);
    }

    public login(userLogin: UserLogin): Promise<UserLoginResponse> {
        const user = this.users.find(u => u.userName === userLogin.username);

        if (!user) {
            throw new Error('Invalid username or password.');
        }

        return Promise.resolve({
            token: 'asdf',
            user: user
        });
    }
}