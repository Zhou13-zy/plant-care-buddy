import { PlantHealthStatus } from './plantHealthStatus';

export interface CreatePlantDto {
  name: string;
  species: string;
  acquisitionDate: string;
  location: string;
  healthStatus: PlantHealthStatus;
  nextHealthCheckDate?: string;
  notes?: string;
  primaryImagePath?: string;
}