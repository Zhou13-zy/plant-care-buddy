  // src/models/updatePlantDto.ts
  export interface UpdatePlantDto {
    id: number;
    name?: string;
    species?: string;
    acquisitionDate?: string;
    location?: string;
    healthStatus?: number;
    notes?: string;
    primaryImagePath?: string;
  }