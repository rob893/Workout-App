import { Equipment } from '../entities/Exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class EquipmentAPI extends WorkoutAppAPI {
    public getAllEquipment(): Promise<Equipment[]> {
        return this.get<Equipment[]>('equipment');
    }

    public async getEquipmentById(id: string): Promise<Equipment | null> {
        const equipment = await this.get<Equipment>(`equipment/${id}`);

        if (!equipment) {
            return null;
        }

        return equipment;
    }
}
