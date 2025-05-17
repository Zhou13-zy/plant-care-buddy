  // src/models/updatePlantDto.ts
  import { PlantHealthStatus } from './plantHealthStatus';

  export interface UpdatePlantDto {
    name: string;
    species: string;
    acquisitionDate: string;
    location: string;
    nextHealthCheckDate?: string;
    notes?: string;
    primaryImagePath?: string;
  }