import { ApolloServer } from 'apollo-server';
import { typeDefs } from './schema';
import { resolvers } from './resolvers';
import { UserAPI } from './datasources/UserAPI';
import dotenv from 'dotenv';
import { ExerciseAPI } from './datasources/ExerciseAPI';
import { MuscleAPI } from './datasources/MuscleAPI';
import { EquipmentAPI } from './datasources/EquipmentAPI';
import jwtDecode from 'jwt-decode';
import { WorkoutAPI } from './datasources/WorkoutAPI';
import { TypeGuards } from './helpers/TypeGuards';
import { DateFormatDirective } from './helpers/DateFormatDirective';
import { JwtClaims } from './models/common';
import { ExerciseCategoryAPI } from './datasources/ExerciseCategoryAPI';

async function start(): Promise<void> {
  dotenv.config();

  process.env.NODE_ENV = process.env.NODE_ENV ?? 'development';

  const debugEnabled = process.env.DEBUG === 'true';
  const tracingEnabled = process.env.TRACING === 'true';
  const enabledErrorExtensions = new Set<string>(process.env.ALLOWED_ERROR_EXTENSIONS?.split(',') || []);

  const server = new ApolloServer({
    context: ({ req, res }) => {
      const token = req.headers.authorization || '';
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
    schemaDirectives: {
      dateFormat: DateFormatDirective
    },
    typeDefs,
    resolvers,
    onHealthCheck: () => {
      return Promise.resolve(true);
    },
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

      if (!debugEnabled && error.extensions) {
        Object.keys(error.extensions).forEach(key => {
          if (!enabledErrorExtensions.has(key) && error.extensions) {
            delete error.extensions[key];
          }
        });
      }

      return error;
    },
    debug: debugEnabled,
    tracing: tracingEnabled,
    dataSources: () => ({
      userAPI: new UserAPI(),
      exerciseAPI: new ExerciseAPI(),
      muscleAPI: new MuscleAPI(),
      equipmentAPI: new EquipmentAPI(),
      workoutAPI: new WorkoutAPI(),
      exerciseCategoryAPI: new ExerciseCategoryAPI()
    }),
    cors: {
      origin: process.env.CORS_ALLOWED_ORIGINS ? process.env.CORS_ALLOWED_ORIGINS.split(',') : '*'
    }
  });

  const serverInfo = await server.listen();

  console.log(`Server ready at ${serverInfo.url}`);
}

start();
