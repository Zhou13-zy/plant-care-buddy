import { PlantHealthStatus } from '../Plant/plantHealthStatus';

export interface HealthObservation {
  id: number;
  plantId: number;
  plantName: string;
  observationDate: string;
  healthStatus: PlantHealthStatus;
  notes: string;
  imagePath?: string;
}