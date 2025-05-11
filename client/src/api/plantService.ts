import api from './axiosConfig';
import { Plant } from '../models/plant';
import { CreatePlantDto } from '../models/createPlantDto';

export const getAllPlants = async (): Promise<Plant[]> => {
  const response = await api.get<Plant[]>('/plants');
  return response.data;
};

export const addPlant = async (plant: CreatePlantDto) => {
    const response = await api.post('/plants', plant);
    return response.data;
};

// add more methods here later (updatePlant, deletePlant, etc.)