import { RESTDataSource, RequestOptions, Response, Request } from 'apollo-datasource-rest';
import { AuthenticationError } from 'apollo-server';
import { WorkoutAppContext } from '../models/WorkoutAppContext';

export abstract class WorkoutAppAPI extends RESTDataSource<WorkoutAppContext> {
  public constructor() {
    super();
    this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
  }

  protected willSendRequest(request: RequestOptions): void {
    if (this.context.token) {
      request.headers.set('authorization', this.context.token);
    }
  }

  protected didReceiveResponse<TResult = any>(response: Response, request: Request): Promise<TResult> {
    if (
      response.headers.has('Content-Type') &&
      response.headers.get('Content-Type')?.startsWith('application/problem+json')
    ) {
      response.headers.set('Content-Type', 'application/json');
    }

    if (response.status === 401 && response.headers.has('X-Token-Expired')) {
      throw new AuthenticationError('token-expired');
    }

    return super.didReceiveResponse(response, request);
  }
}
