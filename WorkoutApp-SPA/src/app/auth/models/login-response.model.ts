import { User } from 'src/app/shared/models/user.model';

export interface LoginResponse {
  token: string;
  refreshToken: string;
  user: User;
}
