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
import { TypeGuards } from './helpers/TypeGuards';

async function start(): Promise<void> {
    dotenv.config();

    const debugEnabled = process.env.DEBUG === 'true';
    const enabledErrorExtensions = new Set<string>(process.env.ALLOWED_ERROR_EXTENSIONS?.split(',') || []);

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
        formatError: error => {
            if (error.extensions && error.originalError) {
                error.extensions.originalErrorType = error.originalError.constructor.name;
            }

            if (error.extensions?.response?.body) {
                const { body } = error.extensions.response;

                if (TypeGuards.isWorkoutAppAPIError(body)) {
                    error.message = body.errors.reduce((accumulator, curr) => `${accumulator} ${curr}`);
                }
            }

            if (!debugEnabled) {
                for (const key in error.extensions) {
                    if (!enabledErrorExtensions.has(key)) {
                        delete error.extensions[key];
                    }
                }
            }

            return error;
        },
        debug: debugEnabled,
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
