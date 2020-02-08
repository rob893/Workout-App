import { ApolloServer } from 'apollo-server';
import { typeDefs } from './schema';
import { resolvers } from './resolvers';
import { UserAPI } from './datasources/UserAPI';
import dotenv from 'dotenv';
import { ExerciseAPI } from './datasources/ExerciseAPI';
import { MuscleAPI } from './datasources/MuscleAPI';
import { EquipmentAPI } from './datasources/EquipmentAPI';
import jwt_decode from 'jwt-decode';
import { JwtClaims } from './entities/User';
import { WorkoutAPI } from './datasources/WorkoutAPI';

export interface WorkoutAppContext {
    token: string;
    claims: JwtClaims;
    dataSources: {
        userAPI: UserAPI;
        exerciseAPI: ExerciseAPI;
        muscleAPI: MuscleAPI;
        equipmentAPI: EquipmentAPI;
        workoutAPI: WorkoutAPI;
    }
}

async function start(): Promise<void> {
    dotenv.config();
    
    const server = new ApolloServer({
        context: ({ req }) => {
            const token = req.headers && req.headers.authorization || '';
            let claims;

            try {
                claims = jwt_decode<JwtClaims>(token);
            } catch {
                claims = {};
            }

            return {
                token,
                claims
            };
        },
        typeDefs,
        resolvers,
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