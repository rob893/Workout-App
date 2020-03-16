export interface WorkoutAppAPIError {
    errors: string[];
    type: string;
    title: string;
    status: number;
    detail: string;
    instance: string;
    extensions: any;
}
