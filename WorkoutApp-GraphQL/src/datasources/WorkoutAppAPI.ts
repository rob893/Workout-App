import querystring from 'querystring';
import { RESTDataSource, RequestOptions, Response, Request } from 'apollo-datasource-rest';
import { AuthenticationError } from 'apollo-server';
import { WorkoutAppContext } from '../models/WorkoutAppContext';
import { Indexable } from '../models/common';

export abstract class WorkoutAppAPI extends RESTDataSource<WorkoutAppContext> {
  protected requestMap = new Map<string, { request: Request; response: Response }>();

  public constructor() {
    super();
    this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
  }

  protected willSendRequest(request: RequestOptions): void {
    if (this.context.token) {
      request.headers.set('authorization', this.context.token);
    }
  }

  protected async didReceiveResponse<TResult>(response: Response, request: Request): Promise<TResult> {
    if (
      response.headers.has('Content-Type') &&
      response.headers.get('Content-Type')?.startsWith('application/problem+json')
    ) {
      response.headers.set('Content-Type', 'application/json');
    }

    if (response.status === 401 && response.headers.has('X-Token-Expired')) {
      throw new AuthenticationError('token-expired');
    }

    if (response.status === 404) {
      return null as any;
    }

    this.requestMap.set(request.url, { request, response });

    return super.didReceiveResponse<TResult>(response, request);
  }

  protected static buildQuery<TParams extends Indexable<string | number | boolean | null | undefined> = any>(
    queryParams: TParams
  ): string {
    Object.keys(queryParams).forEach(key => {
      if (queryParams[key] === undefined) {
        delete queryParams[key];
      }
    });

    return querystring.stringify(queryParams);
  }
}
