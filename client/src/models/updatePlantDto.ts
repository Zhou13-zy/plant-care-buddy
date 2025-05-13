  // src/models/updatePlantDto.ts
  import { PlantHealthStatus } from './plantHealthStatus';

  export interface UpdatePlantDto {
    id: number;
    name?: string;
    species?: string;
    acquisitionDate?: string;
    location?: string;
    healthStatus?: PlantHealthStatus;
    notes?: string;
    primaryImagePath?: string;
  }