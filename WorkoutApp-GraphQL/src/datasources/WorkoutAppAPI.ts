import querystring from 'querystring';
import { RESTDataSource, RequestOptions, Response, Request } from 'apollo-datasource-rest';
import { AuthenticationError } from 'apollo-server';
import { WorkoutAppContext } from '../models/common';
import { Indexable, Primitive } from '../models/common';

export abstract class WorkoutAppAPI extends RESTDataSource<WorkoutAppContext> {
  private readonly nullReturnRules: ((error: any, context: WorkoutAppContext) => boolean)[] = [];

  public constructor() {
    super();
    this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    this.addNullReturnRules(
      error => typeof error?.extensions?.response?.status === 'number' && error.extensions.response.status === 404
    );
  }

  protected static buildQuery<
    TParams extends Indexable<Primitive | string[] | number[] | boolean[] | null | undefined>
  >(queryParams: TParams): string {
    // Remove all values which could cause a 'foo=&' situation
    Object.keys(queryParams).forEach(key => {
      if (Array.isArray(queryParams[key])) {
        if ((queryParams[key] as any[]).length === 0) {
          delete queryParams[key];
        } else {
          (queryParams[key] as any[]) = (queryParams[key] as any[]).filter(WorkoutAppAPI.isValidQueryParam);
        }
      } else if (!WorkoutAppAPI.isValidQueryParam(queryParams[key])) {
        delete queryParams[key];
      }
    });

    return querystring.stringify(queryParams);
  }

  /**
   * Valid as defined here: https://nodejs.org/docs/latest-v12.x/api/querystring.html
   */
  private static isValidQueryParam(param: any): boolean {
    return (
      (typeof param === 'string' && param !== '') ||
      (typeof param === 'number' && !Number.isNaN(param)) ||
      typeof param === 'boolean'
    );
  }

  protected addNullReturnRules(
    rules:
      | ((error: any, context: WorkoutAppContext) => boolean)
      | ((error: any, context: WorkoutAppContext) => boolean)[]
  ): void {
    if (Array.isArray(rules)) {
      rules.forEach(rule => this.addNullReturnRules(rule));
    } else {
      this.nullReturnRules.push(rules);
    }
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

    try {
      return await super.didReceiveResponse<TResult>(response, request);
    } catch (error) {
      if (this.nullReturnRules.some(rule => rule(error, this.context))) {
        return null as any;
      }

      throw error;
    }
  }
}
