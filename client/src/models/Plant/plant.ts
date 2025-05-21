import { PlantHealthStatus } from './plantHealthStatus';
import { PlantType } from './plantType';

export interface Plant {
    id: string;
    name: string;
    species: string;
    plantType: PlantType;
    acquisitionDate: string;
    location: string;
    healthStatus: PlantHealthStatus;
    healthStatusName?: string;
    nextHealthCheckDate: string;
    notes?: string;
    primaryImagePath?: string;
  }