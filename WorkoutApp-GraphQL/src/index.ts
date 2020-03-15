import { ApolloServer } from 'apollo-server';
import { typeDefs } from './schema';
import { resolvers } from './resolvers';
import { UserAPI } from './datasources/UserAPI';
import dotenv from 'dotenv';
import { ExerciseAPI } from './datasources/ExerciseAPI';
import { MuscleAPI } from './datasources/MuscleAPI';
import { EquipmentAPI } from './datasources/EquipmentAPI';
import jwtDecode from 'jwt-decode';
import { JwtClaims } from './entities/User';
import { WorkoutAPI } from './datasources/WorkoutAPI';
import { Request, Response } from 'express';

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
    };
}

async function start(): Promise<void> {
    dotenv.config();

    const server = new ApolloServer({
        context: ({ req, res }) => {
            const token = (req.headers && req.headers.authorization) || '';
            let claims;

            try {
                claims = jwtDecode<JwtClaims>(token);
            } catch {
                claims = {};
            }

            return {
                token,
                claims,
                request: req,
                response: res
            };
        },
        typeDefs,
        resolvers,
        cors: {
            exposedHeaders: ['token-expired']
        },
        formatError: error => {
            if (error.extensions && error.originalError) {
                delete error.extensions.response;
                error.extensions.errorType = error.originalError.constructor.name;
            }

            return error;
        },
        debug: true,
        dataSources: () => ({
            userAPI: new UserAPI(),
            exerciseAPI: new ExerciseAPI(),
            muscleAPI: new MuscleAPI(),
            equipmentAPI: new EquipmentAPI(),
            workoutAPI: new WorkoutAPI()
        })
    });

    const serverInfo = await server.listen();

    console.log(`Server ready at ${serverInfo.url}`);
}

start();
