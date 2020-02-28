import { User } from 'src/app/shared/models/user.model';

export interface LoginResponse {
    token: string;
    user: User;
}
