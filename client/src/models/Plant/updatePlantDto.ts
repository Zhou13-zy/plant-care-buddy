  // src/models/updatePlantDto.ts
  import { PlantHealthStatus } from './plantHealthStatus';

  export interface UpdatePlantDto {
    name: string;
    species: string;
    acquisitionDate: string;
    location: string;
    healthStatus: PlantHealthStatus;
    nextHealthCheckDate?: string;
    notes?: string;
    primaryImagePath?: string;
  }