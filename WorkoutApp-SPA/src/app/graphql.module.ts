import { NgModule } from '@angular/core';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLinkModule, HttpLink } from 'apollo-angular-link-http';
import { onError } from 'apollo-link-error';
import { ServerError, ServerParseError } from 'apollo-link-http-common';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { ApolloLink } from 'apollo-link';

const uri = 'http://localhost:4000';

function isServerError(error: Error | ServerError | ServerParseError): error is ServerError {
    return (error as ServerError).statusCode !== undefined && (error as ServerError).response !== undefined && (error as ServerError).result !== undefined;
}

export function createApollo(httpLink: HttpLink) {
    const http = httpLink.create({ uri });

    const afterwareLink = new ApolloLink((operation, forward) => {
        return forward(operation).map(response => {
            const context = operation.getContext();
            const {
                response: { headers }
            } = context;

            if (headers) {
                console.log(headers.get('token-expired'));
            }

            return response;
        })
    });

    const logoutLink = onError(({ graphQLErrors, networkError, operation, forward }) => {
        const context = operation.getContext();

        if (graphQLErrors) {
            for (const error of graphQLErrors) {
                if (error.extensions.code === 'UNAUTHENTICATED') {
                    const {
                        response: { headers }
                    } = context;

                    if (headers && headers.has('token-expired')) {
                        console.log('refreshToken');
                    } else {
                        console.log('log the dude out');
                    }
                }
            }
        }
    });

    return {
        link: afterwareLink.concat(logoutLink).concat(http),
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
