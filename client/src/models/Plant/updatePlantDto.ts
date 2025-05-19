import { PlantType } from './plantType';

export interface UpdatePlantDto {
  name: string;
  species: string;
  plantType: PlantType;
  acquisitionDate: string;
  location: string;
  nextHealthCheckDate?: string;
  notes?: string;
  photo?: File;
}