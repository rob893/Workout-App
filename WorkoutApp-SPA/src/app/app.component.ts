import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { GraphQLQueries } from './queries';

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
            mutation: GraphQLQueries.login,
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
