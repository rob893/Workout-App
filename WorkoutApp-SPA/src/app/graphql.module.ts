import { NgModule } from '@angular/core';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http';
import { onError } from 'apollo-link-error';
import { ServerError, ServerParseError } from 'apollo-link-http-common';
import { InMemoryCache } from 'apollo-cache-inmemory';

const uri = 'http://localhost:4000';

function isServerError(error: Error | ServerError | ServerParseError): error is ServerError {
    return (error as ServerError).statusCode !== undefined && (error as ServerError).response !== undefined && (error as ServerError).result !== undefined;
}

export function createApollo(httpLink: HttpLink) {
    const http = httpLink.create({ uri });
    const logoutLink = onError(({ networkError, operation }) => {
        console.log(operation.getContext());
        console.log(networkError);
        if (networkError && isServerError(networkError) && networkError.statusCode === 401) {
            // authService.logout();
        }
    });
    
    return {
        link: logoutLink.concat(http),
        cache: new InMemoryCache(),
    };
}

@NgModule({
    exports: [ApolloModule, HttpLinkModule],
    providers: [
        {
            provide: APOLLO_OPTIONS,
            useFactory: createApollo,
            deps: [HttpLink],
        },
    ],
})
export class GraphQLModule { }
