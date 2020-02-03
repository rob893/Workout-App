import { RESTDataSource, RequestOptions } from "apollo-datasource-rest";
import { Exercise, Muscle, Equipment } from "../entities/Exercise";

export class EquipmentAPI extends RESTDataSource {
    public constructor() {
        super();
        this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    }

    public willSendRequest(request: RequestOptions): void {
        if (this.context && this.context.token) {
            request.headers.set('authorization', this.context.token);
        }
    }

    public async getAllEquipment(): Promise<Equipment[]> {
        const equipment = await this.get<Equipment[]>('equipment');

        return equipment;
    }

    public async getEquipmentById(id: string): Promise<Equipment | null> {
        const equipment = await this.get<Equipment>(`equipment/${id}`);

        if (!equipment) {
            return null;
        }

        return equipment;
    }
}