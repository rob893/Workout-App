import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

type LoginResponse = {
    login: {
        token?: string;
        user?: {
            username?: string;
            firstName?: string;
            lastName?: string;
            email?: string;
        } 
    }
};

const login = gql`
    mutation Login($userCredentials: UserLogin!) {
        login(userCredentials: $userCredentials) {
            token
            user {
                firstName
                email
            }
        }
    }
`;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'WorkoutApp-SPA';
    private readonly apollo: Apollo;

    public constructor(apollo: Apollo) {
        this.apollo = apollo;
    }

    public async ngOnInit() {
        const res = await this.apollo.mutate<LoginResponse>({
            mutation: login,
            variables: {
                userCredentials: {
                    username: 'robert',
                    password: 'password'
                }
            }
        }).toPromise();

        console.log(res.data.login.token);
    }
}
