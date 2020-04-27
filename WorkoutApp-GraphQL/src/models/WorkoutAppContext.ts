import { Request } from 'apollo-server';
import { Response } from 'apollo-datasource-rest';
import { UserAPI } from '../datasources/UserAPI';
import { ExerciseAPI } from '../datasources/ExerciseAPI';
import { MuscleAPI } from '../datasources/MuscleAPI';
import { EquipmentAPI } from '../datasources/EquipmentAPI';
import { WorkoutAPI } from '../datasources/WorkoutAPI';
import { ExerciseCategoryAPI } from '../datasources/ExerciseCategoryAPI';

export interface JwtClaims {
  nameid?: string;
  unique_name?: string;
  role?: string[];
  nbf?: number;
  exp?: number;
  iat?: number;
  iss?: string;
  aud?: string;
}

export interface WorkoutAppContext {
  token: string;
  claims: JwtClaims;
  request: Request;
  response: Response;
  dataSources: {
    userAPI: UserAPI;
    exerciseAPI: ExerciseAPI;
    muscleAPI: MuscleAPI;
    equipmentAPI: EquipmentAPI;
    workoutAPI: WorkoutAPI;
    exerciseCategoryAPI: ExerciseCategoryAPI;
  };
}
