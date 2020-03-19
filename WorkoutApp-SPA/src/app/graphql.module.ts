import { NgModule } from '@angular/core';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { HttpLinkModule, HttpLink, HttpLinkHandler } from 'apollo-angular-link-http';
import { onError } from 'apollo-link-error';
import { ServerError, ServerParseError } from 'apollo-link-http-common';
import { InMemoryCache } from 'apollo-cache-inmemory';
import { ApolloLink, execute, Observable, FetchResult } from 'apollo-link';
import { refreshToken } from './auth/auth.queries';

const uri = 'http://localhost:4000';

export function createApollo(httpLink: HttpLink) {
    const http = httpLink.create({ uri });

    const logoutLink = onError(({ graphQLErrors, operation, forward }): Observable<FetchResult> => {
        if (graphQLErrors) {
            for (const error of graphQLErrors) {
                if (error.extensions.code === 'UNAUTHENTICATED') {
                    if (error.message === 'token-expired') {
                        return execute(http, {
                            query: refreshToken,
                            variables: {
                                refreshTokenInput: {
                                    token: localStorage.getItem('access-token'),
                                    refreshToken: localStorage.getItem('refresh-token'),
                                    source: 'web'
                                }
                            }
                        }).flatMap(res => {
                            const token = res.data.refreshToken.token;
                            const refreshToken = res.data.refreshToken.refreshToken;
                            
                            localStorage.setItem('access-token', token);
                            localStorage.setItem('refresh-token', refreshToken);

                            const oldHeaders = operation.getContext().headers;

                            operation.setContext({
                                headers: {
                                    ...oldHeaders,
                                    authorization: `Bearer ${token}`
                                }
                            });

                            return forward(operation);
                        });
                    } else {
                        console.log('log the dude out');
                    }
                }
            }
        }
    });

    return {
        link: logoutLink.concat(http),
        cache: new InMemoryCache()
    };
}

@NgModule({
    exports: [ApolloModule, HttpLinkModule],
    providers: [
        {
            provide: APOLLO_OPTIONS,
            useFactory: createApollo,
            deps: [HttpLink]
        }
    ]
})
export class GraphQLModule {}
