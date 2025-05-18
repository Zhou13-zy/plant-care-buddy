import { PlantHealthStatus } from '../Plant/plantHealthStatus';

export interface UpdateHealthObservationDto {
  observationDate: string;
  healthStatus: PlantHealthStatus;
  notes: string;
  photo?: File;
}