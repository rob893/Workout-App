import { ApolloServer } from 'apollo-server';
import { typeDefs } from './schema';
import { resolvers } from './resolvers';
import { UserAPI } from './datasources/UserAPI';
import dotenv from 'dotenv';
import { ExerciseAPI } from './datasources/ExerciseAPI';
import { MuscleAPI } from './datasources/MuscleAPI';
import { EquipmentAPI } from './datasources/EquipmentAPI';

async function start(): Promise<void> {
    dotenv.config();
    
    const server = new ApolloServer({
        context: ({ req }) => {
            const token = req.headers && req.headers.authorization || '';

            return {
                token: token
            };
        },
        typeDefs,
        resolvers,
        dataSources: () => ({
            userAPI: new UserAPI(),
            exerciseAPI: new ExerciseAPI(),
            muscleAPI: new MuscleAPI(),
            equipmentAPI: new EquipmentAPI()
        })
    });

    const serverInfo = await server.listen();

    console.log(`Server ready at ${serverInfo.url}`);
}

start();