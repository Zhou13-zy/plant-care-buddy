import { PlantHealthStatus } from './plantHealthStatus';

export interface Plant {
    id: number;
    name: string;
    species: string;
    acquisitionDate: string;
    location: string;
    healthStatus: PlantHealthStatus;
    healthStatusName?: string;
    notes?: string;
    primaryImagePath?: string;
  }