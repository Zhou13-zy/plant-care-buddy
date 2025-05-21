import { PlantHealthStatus } from '../Plant/plantHealthStatus';

export interface HealthObservation {
  id: string;
  plantId: string;
  plantName: string;
  observationDate: string;
  healthStatus: PlantHealthStatus;
  notes: string;
  imagePath?: string;
}