export interface User {
  scheduledWorkoutsUrl: string;
  favoriteExercisesUrl: string;
  workoutInvitationsUrl: string;
  sentWorkoutInvitationsUrl: string;
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  created: Date;
  id: number;
  url: string;
  detailedUrl: string;
}

export interface UserToRegister {
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface UserLogin {
  username: string;
  password: string;
  source: string;
}

export interface UserLoginResponse {
  token: string;
  refreshToken: string;
  user: User;
}
