import { PlantHealthStatus } from './plantHealthStatus';
import { PlantType } from './plantType';

export interface CreatePlantDto {
  name: string;
  species: string;
  plantType: PlantType;
  acquisitionDate: string;
  location: string;
  healthStatus: PlantHealthStatus;
  nextHealthCheckDate?: string;
  notes?: string;
  photo?: File;
}