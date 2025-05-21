import { PlantHealthStatus } from '../Plant/plantHealthStatus';

export interface CreateHealthObservationDto {
  plantId: string;
  observationDate: string;
  healthStatus: PlantHealthStatus;
  notes: string;
  photo?: File;
}