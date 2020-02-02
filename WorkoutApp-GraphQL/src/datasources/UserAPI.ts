import { DataSource } from "apollo-datasource";
import { User } from "../entities/User";

const users: User[] = [
    {
        id: '1',
        firstName: 'John',
        lastName: 'Smith',
        age: 26
    },
    {
        id: '2',
        firstName: 'Joe',
        lastName: 'Black',
        age: 23
    },
    {
        id: '3',
        firstName: 'Jane',
        lastName: 'Doe',
        age: 28
    },
    {
        id: '4',
        firstName: 'Matt',
        lastName: 'Albege',
        age: 35
    }
];

export class UserAPI extends DataSource {

    private readonly users: User[];


    public constructor() {
        super();
        this.users = users;
    }


    public getAllUsers(): User[] {
        return this.users;
    }

    public getUserById(id: string): User | null {
        const user = this.users.find(u => u.id === id);

        if (!user) {
            return null;
        }

        return user;
    }

    public createUser(userToCreate: User | { firstName: string, lastName: string, age: number }): User {
        const nextId = Math.max(...this.users.map(u => Number.parseInt(u.id))) + 1;
        const user = {
            id: nextId.toString(),
            firstName: userToCreate.firstName,
            lastName: userToCreate.lastName,
            age: userToCreate.age
        };

        this.users.push(user);

        return user;
    }
}