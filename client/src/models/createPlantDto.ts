// client/src/models/createPlantDto.ts
import { PlantHealthStatus } from './plantHealthStatus';

export interface CreatePlantDto {
  name: string;
  species: string;
  acquisitionDate: string;
  location: string;
  healthStatus: PlantHealthStatus;
  notes?: string;
  primaryImagePath?: string;
}