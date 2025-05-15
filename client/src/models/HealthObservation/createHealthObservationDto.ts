import { PlantHealthStatus } from '../Plant/plantHealthStatus';

export interface CreateHealthObservationDto {
  plantId: number;
  observationDate: string;
  healthStatus: PlantHealthStatus;
  notes: string;
  imagePath?: string;
}