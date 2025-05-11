// client/src/models/createPlantDto.ts
export interface CreatePlantDto {
  name: string;
  species: string;
  acquisitionDate: string;
  location?: string;
  healthStatus: number;
  notes?: string;
  primaryImagePath?: string;
}